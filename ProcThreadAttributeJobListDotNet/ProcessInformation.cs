using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
[StructLayout(LayoutKind.Sequential)]
internal sealed class ProcessInformation
{
    public IntPtr ProcessHandle;

    public IntPtr ThreadHandle;

    public uint ProcessId;

    public uint ThreadId;
}