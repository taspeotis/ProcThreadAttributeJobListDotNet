using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

[PublicAPI]
public enum ShowWindowCommand : ushort
{
    Hide = 0,

    ShowNormal = 1,

    Normal = 1,

    ShowMinimized = 2,

    ShowMaximized = 3,

    Maximize = 3,

    ShowNormalNoActivate = 4,

    Show = 5,

    Minimize = 6,

    ShowMinimizedNoActivate = 7,

    ShowNoActivate = 8,

    Restore = 9,

    ShowDefault = 10,

    ForceMinimize = 11
}