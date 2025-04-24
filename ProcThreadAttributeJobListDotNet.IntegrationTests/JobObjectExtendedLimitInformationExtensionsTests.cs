namespace ProcThreadAttributeJobListDotNet.IntegrationTests;

public sealed class JobObjectExtendedLimitInformationExtensionsTests
{
    [Fact]
    public void CreateJobObject_Creates_JobObject()
    {
        var extendedLimitInformation = new JobObjectExtendedLimitInformation
        {
            BasicLimitInformation = { LimitFlags = JobObjectLimit.KillOnJobClose }
        };

        var safeJobObjectHandle = extendedLimitInformation.CreateJobObject();

        using (safeJobObjectHandle)
        {
            Assert.False(safeJobObjectHandle.IsClosed);
            Assert.False(safeJobObjectHandle.IsInvalid);
        }

        Assert.True(safeJobObjectHandle.IsClosed);
    }
}