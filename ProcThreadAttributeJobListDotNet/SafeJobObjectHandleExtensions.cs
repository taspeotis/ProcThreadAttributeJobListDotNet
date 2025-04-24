using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ProcThreadAttributeJobListDotNet;

public static class SafeJobObjectHandleExtensions
{
    public static Process CreateAssociatedProcess(this SafeJobObjectHandle safeHandle,
        string fileName, string? argument = null, ConfigureStartupInfo? configureStartupInfo = null)
    {
        nuint size = 0;

        // https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-initializeprocthreadattributelist#remarks
        // "First, call this function with the dwAttributeCount parameter set to the maximum number of attributes
        // you will be using and the lpAttributeList to NULL ... This initial call will return an error by design."
        InitializeProcThreadAttributeList(IntPtr.Zero, 1, 0, ref size);

        var attributeList = Marshal.AllocHGlobal((int)size);

        try
        {
            try
            {
                if (!InitializeProcThreadAttributeList(attributeList, 1, 0, ref size))
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

                var handle = safeHandle.DangerousGetHandle();

                var updateProcThreadAttributeResult = UpdateProcThreadAttribute(
                    attributeList, 0, (nuint)ProcThreadAttribute.JobList,
                    ref handle, (nuint)IntPtr.Size, IntPtr.Zero, IntPtr.Zero);

                if (!updateProcThreadAttributeResult)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

                const ProcessCreationFlags processCreationFlags = ProcessCreationFlags.ExtendedStartupInfoPresent;
                var startupInfoEx = new StartupInfoEx { AttributeList = attributeList };

                configureStartupInfo?.Invoke(ref startupInfoEx.StartupInfo);

                // https://learn.microsoft.com/en-us/windows/win32/api/winbase/ns-winbase-startupinfoexa#remarks
                // "Be sure to set the cb member of the STARTUPINFO structure to sizeof(STARTUPINFOEX)."
                startupInfoEx.StartupInfo.cb = (uint)Marshal.SizeOf<StartupInfoEx>();

                var processInformation = new ProcessInformation();

                var createProcessResult = CreateProcess(fileName, argument, IntPtr.Zero, IntPtr.Zero,
                    false, processCreationFlags, IntPtr.Zero, null, startupInfoEx, processInformation);

                if (!createProcessResult)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

                using (new SafeCloseHandle(processInformation.ProcessHandle))
                using (new SafeCloseHandle(processInformation.ThreadHandle))
                    return Process.GetProcessById((int)processInformation.ProcessId);
            }
            finally
            {
                DeleteProcThreadAttributeList(attributeList);
            }
        }
        finally
        {
            Marshal.FreeHGlobal(attributeList);
        }
    }

    public delegate void ConfigureStartupInfo(ref StartupInfo startupInfo);

    [DllImport(Constants.Kernel32DllName, CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool CreateProcess(
        string? lpApplicationName,
        string? lpCommandLine,
        IntPtr lpProcessAttributes,
        IntPtr lpThreadAttributes,
        bool bInheritHandles,
        ProcessCreationFlags dwCreationFlags,
        IntPtr lpEnvironment,
        string? lpCurrentDirectory,
        StartupInfoEx lpStartupInfo,
        ProcessInformation lpProcessInformation
    );

    [DllImport(Constants.Kernel32DllName)]
    private static extern void DeleteProcThreadAttributeList(IntPtr attributeList);

    [DllImport(Constants.Kernel32DllName, SetLastError = true)]
    private static extern bool InitializeProcThreadAttributeList(
        IntPtr attributeList, uint attributeCount, uint flags, ref nuint lpSize);

    [DllImport(Constants.Kernel32DllName, SetLastError = true)]
    private static extern bool UpdateProcThreadAttribute(IntPtr attributeList, uint flags,
        nuint attribute, ref IntPtr value, nuint cbSize, IntPtr previousValue, IntPtr returnSize);
}