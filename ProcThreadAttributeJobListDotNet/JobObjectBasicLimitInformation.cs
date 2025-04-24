using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
public struct JobObjectBasicLimitInformation
{
    public long PerProcessUserTimeLimit;

    public long PerJobUserTimeLimit;

    public JobObjectLimit LimitFlags;

    public nuint MinimumWorkingSetSize;

    public nuint MaximumWorkingSetSize;

    public uint ActiveProcessLimit;

    public nuint Affinity;

    public uint PriorityClass;

    public uint SchedulingClass;
}