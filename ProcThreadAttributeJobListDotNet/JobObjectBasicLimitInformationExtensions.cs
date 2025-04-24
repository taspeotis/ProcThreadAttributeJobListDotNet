using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

public static class JobObjectBasicLimitInformationExtensions
{
    [PublicAPI]
    public static SafeJobObjectHandle CreateJobObject(this JobObjectExtendedLimitInformation extendedLimitInformation)
    {
        var handle = CreateJobObject(IntPtr.Zero, null);

        if (handle == IntPtr.Zero)
            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

        const JobObjectInfoClass jobObjectInfoClass = JobObjectInfoClass.JobObjectExtendedLimitInformation;
        var length = Marshal.SizeOf<JobObjectExtendedLimitInformation>();

        if (!SetInformationJobObject(handle, jobObjectInfoClass, extendedLimitInformation, length))
            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

        return new SafeJobObjectHandle(handle);
    }

    [DllImport(Constants.Kernel32DllName, CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CreateJobObject(IntPtr securityAttributes, string? name);

    [DllImport(Constants.Kernel32DllName, CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetInformationJobObject(
        IntPtr handle,
        JobObjectInfoClass jobObjectInfoClass,
        JobObjectExtendedLimitInformation extendedLimitInformation,
        int extendedLimitInformationLength);
}