using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Tiknas.BackgroundJobs;

public class BackgroundJobExecuter_Tests : BackgroundJobsTestBase
{
    private readonly IBackgroundJobExecuter _backgroundJobExecuter;

    public BackgroundJobExecuter_Tests()
    {
        _backgroundJobExecuter = GetRequiredService<IBackgroundJobExecuter>();
    }

    [Fact]
    public async Task Should_Execute_Tasks()
    {
        //Arrange

        var jobObject = GetRequiredService<MyJob>();
        jobObject.ExecutedValues.ShouldBeEmpty();

        //Act

        await _backgroundJobExecuter.ExecuteAsync(
            new JobExecutionContext(
                ServiceProvider,
                typeof(MyJob),
                new MyJobArgs("42")
            )
        );

        //Assert

        jobObject.ExecutedValues.ShouldContain("42");
    }

    [Fact]
    public async Task Should_Execute_Async_Tasks()
    {
        //Arrange

        var jobObject = GetRequiredService<MyAsyncJob>();
        jobObject.ExecutedValues.ShouldBeEmpty();

        //Act

        await _backgroundJobExecuter.ExecuteAsync(
            new JobExecutionContext(
                ServiceProvider,
                typeof(MyAsyncJob),
                new MyAsyncJobArgs("42")
            )
        );

        //Assert

        jobObject.ExecutedValues.ShouldContain("42");
    }

    [Fact]
    public async Task Should_Change_TenantId_If_EventData_Is_MultiTenant()
    {
        //Arrange
        var tenantId = Guid.NewGuid();
        var jobObject = GetRequiredService<MyJob>();
        var asyncJobObject = GetRequiredService<MyAsyncJob>();

        //Act

        await _backgroundJobExecuter.ExecuteAsync(
            new JobExecutionContext(
                ServiceProvider,
                typeof(MyJob),
                new MyJobArgs("42", tenantId)
            )
        );

        await _backgroundJobExecuter.ExecuteAsync(
            new JobExecutionContext(
                ServiceProvider,
                typeof(MyAsyncJob),
                new MyAsyncJobArgs("42", tenantId)
            )
        );

        //Assert

        jobObject.TenantId.ShouldBe(tenantId);
        asyncJobObject.TenantId.ShouldBe(tenantId);
    }

    [Fact]
    public async Task Should_Cancel_Job()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        cts.Cancel();

        var jobObject = GetRequiredService<MyJob>();
        jobObject.ExecutedValues.ShouldBeEmpty();

        //Act
        await _backgroundJobExecuter.ExecuteAsync(
            new JobExecutionContext(
                ServiceProvider,
                typeof(MyJob),
                new MyJobArgs("42"),
                cts.Token
            )
        );

        //Assert
        jobObject.Canceled.ShouldBeTrue();

        //Arrange
        var asyncCts = new CancellationTokenSource();
        asyncCts.Cancel();

        var asyncJobObject = GetRequiredService<MyAsyncJob>();
        asyncJobObject.ExecutedValues.ShouldBeEmpty();

        //Act
        await _backgroundJobExecuter.ExecuteAsync(
            new JobExecutionContext(
                ServiceProvider,
                typeof(MyAsyncJob),
                new MyAsyncJobArgs("42"),
                asyncCts.Token
            )
        );

        //Assert
        asyncJobObject.Canceled.ShouldBeTrue();
    }
}
