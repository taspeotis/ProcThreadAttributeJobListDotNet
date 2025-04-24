using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
public struct StartupInfo
{
    public uint cb;

    public string? Reserved1;

    public string? Desktop;

    public string? Title;

    public uint PositionX;

    public uint PositionY;

    public uint SizeWidth;

    public uint SizeHeight;

    public uint ConsoleCharactersWidth;

    public uint ConsoleCharactersHeight;

    public FillAttributes FillAttributes;

    public StartupInfoFlags Flags;

    public ShowWindowCommand ShowWindowCommand;

    public ushort Reserved2;

    public IntPtr Reserved3;

    public IntPtr StandardInputHandle;

    public IntPtr StandardOutputHandle;

    public IntPtr StandardErrorHandle;
}