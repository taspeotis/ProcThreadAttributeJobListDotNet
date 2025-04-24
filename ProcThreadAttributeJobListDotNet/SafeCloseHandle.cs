using System.Runtime.InteropServices;

namespace ProcThreadAttributeJobListDotNet;

public class SafeCloseHandle : SafeHandle
{
    internal SafeCloseHandle(IntPtr existingHandle) : base(IntPtr.Zero, true)
    {
        SetHandle(existingHandle);
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    protected override bool ReleaseHandle()
    {
        return CloseHandle(handle);
    }

    [DllImport(Constants.Kernel32DllName, SetLastError = true)]
    private static extern bool CloseHandle(IntPtr handle);
}