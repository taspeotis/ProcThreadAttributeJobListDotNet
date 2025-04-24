using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
public sealed class JobObjectExtendedLimitInformation
{
    public JobObjectBasicLimitInformation BasicLimitInformation;

    public IoCounters IoInfo;

    public nuint ProcessMemoryLimit;

    public nuint JobMemoryLimit;

    public nuint PeakProcessMemoryUsed;

    public nuint PeakJobMemoryUsed;
}