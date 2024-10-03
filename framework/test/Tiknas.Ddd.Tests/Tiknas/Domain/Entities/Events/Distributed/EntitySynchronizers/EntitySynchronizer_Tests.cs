using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Autofac;
using Tiknas.AutoMapper;
using Tiknas.Data;
using Tiknas.Domain.Entities.Events.Distributed.EntitySynchronizers.WithEntityVersion;
using Tiknas.Domain.Entities.Events.Distributed.EntitySynchronizers.WithoutEntityVersion;
using Tiknas.Domain.Repositories;
using Tiknas.MemoryDb;
using Tiknas.Modularity;
using Tiknas.TestBase;
using Tiknas.Uow;
using Xunit;

namespace Tiknas.Domain.Entities.Events.Distributed.EntitySynchronizers;

public class EntitySynchronizer_Tests : TiknasIntegratedTest<EntitySynchronizer_Tests.TestModule>
{
    [Fact]
    public async Task Should_Handle_Entity_Created_Event()
    {
        var authorId = Guid.NewGuid();

        var uowManager = GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();

        var authorSynchronizer = GetRequiredService<AuthorSynchronizer>();
        var repository = GetRequiredService<IRepository<Author, Guid>>();

        (await repository.FindAsync(authorId)).ShouldBeNull();

        var remoteAuthorEto = new RemoteAuthorEto { Id = authorId, Name = "New" };

        await authorSynchronizer.HandleEventAsync(new EntityCreatedEto<RemoteAuthorEto>(remoteAuthorEto));

        var author = await repository.FindAsync(authorId);
        author.ShouldNotBeNull();
        author.Name.ShouldBe("New");
    }

    [Fact]
    public async Task Should_Handle_Entity_Update_Event()
    {
        var authorId = Guid.NewGuid();

        var uowManager = GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();

        var authorSynchronizer = GetRequiredService<AuthorSynchronizer>();
        var repository = GetRequiredService<IRepository<Author, Guid>>();

        await repository.InsertAsync(new Author(authorId, "Old"), true);

        var author = await repository.FindAsync(authorId);
        author.ShouldNotBeNull();
        author.Id.ShouldBe(authorId);
        author.Name.ShouldBe("Old");

        var remoteAuthorEto = new RemoteAuthorEto { Id = authorId, Name = "New" };

        await authorSynchronizer.HandleEventAsync(new EntityUpdatedEto<RemoteAuthorEto>(remoteAuthorEto));

        author = await repository.FindAsync(authorId);
        author.ShouldNotBeNull();
        author.Id.ShouldBe(authorId);
        author.Name.ShouldBe("New");
    }

    [Fact]
    public async Task Should_Handle_Entity_Deleted_Event()
    {
        var authorId = Guid.NewGuid();

        var uowManager = GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();

        var authorSynchronizer = GetRequiredService<AuthorSynchronizer>();
        var repository = GetRequiredService<IRepository<Author, Guid>>();

        await repository.InsertAsync(new Author(authorId, "Old"), true);

        var author = await repository.FindAsync(authorId);
        author.ShouldNotBeNull();
        author.Id.ShouldBe(authorId);
        author.Name.ShouldBe("Old");

        var remoteAuthorEto = new RemoteAuthorEto { Id = authorId, Name = "New" };

        await authorSynchronizer.HandleEventAsync(new EntityDeletedEto<RemoteAuthorEto>(remoteAuthorEto));

        (await repository.FindAsync(authorId)).ShouldBeNull();

        await Should.NotThrowAsync(() =>
            authorSynchronizer.HandleEventAsync(new EntityDeletedEto<RemoteAuthorEto>(remoteAuthorEto)));
    }

    [Fact]
    public async Task Should_Handle_Versioned_Entity_Created_Event()
    {
        var bookId = Guid.NewGuid();

        var uowManager = GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();

        var bookSynchronizer = GetRequiredService<BookSynchronizer>();
        var repository = GetRequiredService<IRepository<Book, Guid>>();

        (await repository.FindAsync(bookId)).ShouldBeNull();

        var remoteBookEto = new RemoteBookEto { Id = bookId, EntityVersion = 0, Sold = 1 };

        await bookSynchronizer.HandleEventAsync(new EntityCreatedEto<RemoteBookEto>(remoteBookEto));

        var book = await repository.FindAsync(bookId);
        book.ShouldNotBeNull();
        book.EntityVersion.ShouldBe(remoteBookEto.EntityVersion);
        book.Sold.ShouldBe(1);
    }

    [Fact]
    public async Task Should_Handle_Versioned_Entity_Update_Event()
    {
        var bookId = Guid.NewGuid();

        var uowManager = GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();

        var bookSynchronizer = GetRequiredService<BookSynchronizer>();
        var repository = GetRequiredService<IRepository<Book, Guid>>();

        await repository.InsertAsync(new Book(bookId, 1), true);

        var book = await repository.FindAsync(bookId);
        book.ShouldNotBeNull();
        book.Id.ShouldBe(bookId);
        book.EntityVersion.ShouldBe(0);

        var remoteBookEto = new RemoteBookEto { Id = bookId, EntityVersion = 0, Sold = 1 };

        await bookSynchronizer.HandleEventAsync(new EntityUpdatedEto<RemoteBookEto>(remoteBookEto));

        book = await repository.FindAsync(bookId);
        book.ShouldNotBeNull();
        book.EntityVersion.ShouldBe(0);
        book.Sold.ShouldBe(1);

        remoteBookEto.EntityVersion = 1;
        remoteBookEto.Sold = 2;

        await bookSynchronizer.HandleEventAsync(new EntityUpdatedEto<RemoteBookEto>(remoteBookEto));

        book = await repository.FindAsync(bookId);
        book.ShouldNotBeNull();
        book.EntityVersion.ShouldBe(1);
        book.Sold.ShouldBe(2);

        remoteBookEto.EntityVersion = 0;
        remoteBookEto.Sold = 3;

        await bookSynchronizer.HandleEventAsync(new EntityUpdatedEto<RemoteBookEto>(remoteBookEto));

        // Should skip synchronizing older remote entities.
        book = await repository.FindAsync(bookId);
        book.ShouldNotBeNull();
        book.EntityVersion.ShouldBe(1);
        book.Sold.ShouldBe(2);
    }

    [Fact]
    public async Task Should_Handle_Versioned_Entity_Deleted_Event()
    {
        var bookId = Guid.NewGuid();

        var uowManager = GetRequiredService<IUnitOfWorkManager>();
        using var uow = uowManager.Begin();

        var bookSynchronizer = GetRequiredService<BookSynchronizer>();
        var repository = GetRequiredService<IRepository<Book, Guid>>();

        await repository.InsertAsync(new Book(bookId, 1), true);

        var book = await repository.FindAsync(bookId);
        book.ShouldNotBeNull();
        book.Id.ShouldBe(bookId);
        book.EntityVersion.ShouldBe(0);

        var remoteBookEto = new RemoteBookEto { Id = bookId, EntityVersion = 0, Sold = 1 };

        await bookSynchronizer.HandleEventAsync(new EntityDeletedEto<RemoteBookEto>(remoteBookEto));

        (await repository.FindAsync(bookId)).ShouldBeNull();

        await Should.NotThrowAsync(() =>
            bookSynchronizer.HandleEventAsync(new EntityDeletedEto<RemoteBookEto>(remoteBookEto)));
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [DependsOn(
        typeof(TiknasAutofacModule),
        typeof(TiknasMemoryDbModule),
        typeof(TiknasDddDomainModule),
        typeof(TiknasAutoMapperModule)
    )]
    public class TestModule : TiknasModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connStr = Guid.NewGuid().ToString();

            Configure<TiknasDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connStr;
            });

            context.Services.AddMemoryDbContext<MyMemoryDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            context.Services.AddAutoMapperObjectMapper<TestModule>();
            Configure<TiknasAutoMapperOptions>(options =>
            {
                options.AddMaps<TestModule>(validate: true);
            });
        }
    }

    public class MyMemoryDbContext : MemoryDbContext
    {
        public override IReadOnlyList<Type> GetEntityTypes()
        {
            return new List<Type> { typeof(Book), typeof(Author) };
        }
    }

    public class MyAutoMapperProfile : Profile
    {
        public MyAutoMapperProfile()
        {
            CreateMap<RemoteBookEto, Book>(MemberList.None);
            CreateMap<RemoteAuthorEto, Author>(MemberList.None);
        }
    }
}