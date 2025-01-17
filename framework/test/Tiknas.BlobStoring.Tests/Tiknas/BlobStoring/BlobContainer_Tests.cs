﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.BlobStoring.TestObjects;
using Tiknas.Clients;
using Tiknas.Modularity;
using Tiknas.MultiTenancy;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.BlobStoring;

public abstract class BlobContainer_Tests<TStartupModule> : TiknasIntegratedTest<TStartupModule>
    where TStartupModule : ITiknasModule
{
    protected IBlobContainer<TestContainer1> Container { get; }

    protected ICurrentTenant CurrentTenant { get; }

    protected BlobContainer_Tests()
    {
        Container = GetRequiredService<IBlobContainer<TestContainer1>>();
        CurrentTenant = GetRequiredService<ICurrentTenant>();
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Theory]
    [InlineData("test-blob-1")]
    [InlineData("test-blob-1.txt")]
    [InlineData("test-folder/test-blob-1")]
    public async Task Should_Save_And_Get_Blobs(string blobName)
    {
        var testContent = "test content".GetBytes();
        await Container.SaveAsync(blobName, testContent);

        var result = await Container.GetAllBytesAsync(blobName);
        result.SequenceEqual(testContent).ShouldBeTrue();
    }

    [Fact]
    public async Task Should_Save_And_Get_Blobs_In_Different_Tenant()
    {
        var blobName = "test-blob-1";
        var testContent = "test content".GetBytes();

        using (CurrentTenant.Change(Guid.NewGuid()))
        {
            await Container.SaveAsync(blobName, testContent);
            (await Container.GetAllBytesAsync(blobName)).SequenceEqual(testContent).ShouldBeTrue();
        }

        using (CurrentTenant.Change(Guid.NewGuid()))
        {
            await Container.SaveAsync(blobName, testContent);
            (await Container.GetAllBytesAsync(blobName)).SequenceEqual(testContent).ShouldBeTrue();

            using (CurrentTenant.Change(null))
            {
                // Could not find the requested BLOB...
                await Assert.ThrowsAsync<TiknasException>(async () =>
                    await Container.GetAllBytesAsync(blobName)
                );
            }
        }

        using (CurrentTenant.Change(null))
        {
            await Container.SaveAsync(blobName, testContent);
            (await Container.GetAllBytesAsync(blobName)).SequenceEqual(testContent).ShouldBeTrue();
        }
    }

    [Fact]
    public async Task Should_Overwrite_Pre_Saved_Blob_If_Requested()
    {
        var blobName = "test-blob-1";

        var testContent = "test content".GetBytes();
        await Container.SaveAsync(blobName, testContent);

        var testContentOverwritten = "test content overwritten".GetBytes();
        await Container.SaveAsync(blobName, testContentOverwritten, true);

        var result = await Container.GetAllBytesAsync(blobName);
        result.SequenceEqual(testContentOverwritten).ShouldBeTrue();
    }

    [Fact]
    public async Task Should_Not_Allow_To_Overwrite_Pre_Saved_Blob_By_Default()
    {
        var blobName = "test-blob-1";

        var testContent = "test content".GetBytes();
        await Container.SaveAsync(blobName, testContent);

        var testContentOverwritten = "test content overwritten".GetBytes();
        await Assert.ThrowsAsync<BlobAlreadyExistsException>(() =>
            Container.SaveAsync(blobName, testContentOverwritten)
        );
    }

    [Theory]
    [InlineData("test-blob-1")]
    [InlineData("test-blob-1.txt")]
    [InlineData("test-folder/test-blob-1")]
    public async Task Should_Delete_Saved_Blobs(string blobName)
    {
        await Container.SaveAsync(blobName, "test content".GetBytes());
        (await Container.GetAllBytesAsync(blobName)).ShouldNotBeNull();

        await Container.DeleteAsync(blobName);
        (await Container.GetAllBytesOrNullAsync(blobName)).ShouldBeNull();
    }

    [Theory]
    [InlineData("test-blob-1")]
    [InlineData("test-blob-1.txt")]
    [InlineData("test-folder/test-blob-1")]
    public async Task Saved_Blobs_Should_Exists(string blobName)
    {
        await Container.SaveAsync(blobName, "test content".GetBytes());
        (await Container.ExistsAsync(blobName)).ShouldBeTrue();

        await Container.DeleteAsync(blobName);
        (await Container.ExistsAsync(blobName)).ShouldBeFalse();
    }

    [Theory]
    [InlineData("test-blob-1")]
    [InlineData("test-blob-1.txt")]
    [InlineData("test-folder/test-blob-1")]
    public async Task Unknown_Blobs_Should_Not_Exists(string blobName)
    {
        await Container.DeleteAsync(blobName);
        (await Container.ExistsAsync(blobName)).ShouldBeFalse();
    }
}
