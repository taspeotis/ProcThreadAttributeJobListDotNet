# ProcThreadAttributeJobListDotNet

A managed wrapper around `PROC_THREAD_ATTRIBUTE_JOB_LIST`.

> [!WARNING]
> 
> This class library only works on Windows 10 and newer, or Windows Server 2016 and newer.
> If you need a solution for Linux or macOS, there is a discussion [here](https://github.com/dotnet/runtime/issues/101985).

## License

_ProcThreadAttributeJobListDotNet_ is licensed under [The MIT License](LICENSE).

## Getting Started

[Install the _ProcThreadAttributeJobListDotNet_ package from NuGet](https://www.nuget.org/packages/ProcThreadAttributeJobListDotNet).

### Atomically Create Process In Job

```csharp
var extendedLimitInformation = new JobObjectExtendedLimitInformation
{
    BasicLimitInformation = { LimitFlags = JobObjectLimit.KillOnJobClose }
};

using var safeJobObjectHandle = extendedLimitInformation.CreateJobObject();

using var process = safeJobObjectHandle.CreateAssociatedProcess(
    @"C:\Windows\System32\notepad.exe", @"C:\Windows\System32\drivers\etc\hosts");

// ...
```

### Configure `StartupInfo`

`CreateAssociatedProcess` necessarily creates `StartupInfoEx` for its `AttributeList`. The contents of the concomitant
`StartupInfo` (except for `cb`) are irrelevant to creating an associated process, so you can use `configureStartupInfo`
to control the created process further:

```csharp
var extendedLimitInformation = new JobObjectExtendedLimitInformation
{
    BasicLimitInformation = { LimitFlags = JobObjectLimit.KillOnJobClose }
};

using var safeJobObjectHandle = extendedLimitInformation.CreateJobObject();

using var process = safeJobObjectHandle.CreateAssociatedProcess(
    @"C:\Windows\System32\notepad.exe", configureStartupInfo: MyConfiguration);

// ...

void MyConfiguration(ref StartupInfo startupInfo)
{
    startupInfo.Flags = StartupInfoFlags.UseShowWindow;
    startupInfo.ShowWindowCommand = ShowWindowCommand.Maximize;
}
```

### Acknowledgements

Raymond Chen for bringing `PROC_THREAD_ATTRIBUTE_JOB_LIST ` to my attention in [_A more direct and mistake-free way of creating a process in a job object_](https://devblogs.microsoft.com/oldnewthing/20230209-00/?p=107812).

[_CsWin32_](https://github.com/microsoft/CsWin32) for generating several of the structures and enums required.

[_PInvoke.net_](https://www.pinvoke.net/) for the rest. 