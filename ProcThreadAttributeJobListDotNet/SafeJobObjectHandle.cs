using JetBrains.Annotations;

namespace ProcThreadAttributeJobListDotNet;

[PublicAPI]
public sealed class SafeJobObjectHandle(IntPtr existingHandle) : SafeCloseHandle(existingHandle);