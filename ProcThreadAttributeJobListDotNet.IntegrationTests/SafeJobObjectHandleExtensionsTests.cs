using System.Drawing;
using System.Runtime.InteropServices;

namespace ProcThreadAttributeJobListDotNet.IntegrationTests;

public sealed class SafeJobObjectHandleExtensionsTests
{
    [Fact(Timeout = 5000)]
    public async Task CreateAssociatedProcess_Configures_StartupInfo()
    {
        var extendedLimitInformation = new JobObjectExtendedLimitInformation
        {
            BasicLimitInformation = { LimitFlags = JobObjectLimit.KillOnJobClose }
        };

        using var safeJobObjectHandle = extendedLimitInformation.CreateJobObject();

        using var process = safeJobObjectHandle.CreateAssociatedProcess(
            @"C:\Windows\System32\notepad.exe", configureStartupInfo: (ref StartupInfo startupInfo) =>
            {
                startupInfo.Flags = StartupInfoFlags.UseShowWindow;
                startupInfo.ShowWindowCommand = ShowWindowCommand.Maximize;
            });

        while (process.MainWindowHandle == IntPtr.Zero)
            await Task.Delay(1, TestContext.Current.CancellationToken);

        var windowPlacement = new WindowPlacement
        {
            Length = (uint)Marshal.SizeOf<WindowPlacement>()
        };

        Assert.True(GetWindowPlacement(process.MainWindowHandle, ref windowPlacement));
        Assert.Equal(ShowWindowCommand.Maximize, (ShowWindowCommand)windowPlacement.ShowCommand);
    }

    [Fact]
    public void CreateAssociatedProcess_Creates_AssociatedProcess()
    {
        var extendedLimitInformation = new JobObjectExtendedLimitInformation
        {
            BasicLimitInformation = { LimitFlags = JobObjectLimit.KillOnJobClose }
        };

        using var safeJobObjectHandle = extendedLimitInformation.CreateJobObject();
        using var process = safeJobObjectHandle.CreateAssociatedProcess(@"C:\Windows\System32\notepad.exe");
        var result = false;

        Assert.True(IsProcessInJob(process.SafeHandle, safeJobObjectHandle, ref result));
        Assert.True(result);
    }

    [DllImport("User32.dll", SetLastError = true)]
    private static extern bool GetWindowPlacement(IntPtr windowHandle, ref WindowPlacement wionWindowPlacement);

    [DllImport(Constants.Kernel32DllName, SetLastError = true)]
    private static extern bool IsProcessInJob(SafeHandle processHandle, SafeHandle jobHandle, ref bool result);

    [StructLayout(LayoutKind.Sequential)]
    private struct WindowPlacement
    {
        public uint Length;

        public uint Flags;

        /// <remarks>
        ///     This is <see cref="uint" /> rather than <see cref="ShowWindowCommand" />
        ///     because the latter is <see cref="ushort" /> for <see cref="StartupInfo" />.
        /// </remarks>
        public uint ShowCommand;

        public Point MinimumPosition;

        public Point MaximumPosition;

        public Rectangle NormalPosition;
    }
}