using System.Runtime.InteropServices;

namespace ProcThreadAttributeJobListDotNet;

[StructLayout(LayoutKind.Sequential)]
internal sealed class StartupInfoEx
{
    public StartupInfo StartupInfo;

    public IntPtr AttributeList;
}