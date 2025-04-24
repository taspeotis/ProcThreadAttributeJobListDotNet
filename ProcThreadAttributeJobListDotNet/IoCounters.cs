using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

[PublicAPI]
public struct IoCounters
{
    public ulong ReadOperationCount;

    public ulong WriteOperationCount;

    public ulong OtherOperationCount;

    public ulong ReadTransferCount;

    public ulong WriteTransferCount;

    public ulong OtherTransferCount;
}