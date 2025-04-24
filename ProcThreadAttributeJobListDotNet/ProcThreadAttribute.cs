using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal enum ProcThreadAttribute : uint
{
    ParentProcess = 0x20000U,
    HandleList = 0x20002U,
    GroupAffinity = 0x30003U,
    PreferredNode = 0x20004U,
    IdealProcessor = 0x30005U,
    UmsThread = 0x30006U,
    MitigationPolicy = 0x20007U,
    SecurityCapabilities = 0x20009U,
    ProtectionLevel = 0x2000BU,
    MachineType = 0x20019U,
    EnableOptionalXstateFeatures = 0x3001BU,
    JobList = 0x2000DU,
    ChildProcessPolicy = 0x2000EU,
    DesktopAppPolicy = 0x20012U
}