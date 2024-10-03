using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Shouldly;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;
using Tiknas.Timing;
using Xunit;

namespace Tiknas.MongoDB.Serializer;

[Collection(MongoTestCollection.Name)]
public abstract class MongoDB_DateTimeKind_Tests : DateTimeKind_Tests<TiknasMongoDbTestModule>
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        // MongoDB uses static properties to store the mapping information,
        // We must reconfigure it in the new unit test.
        foreach (var registeredClassMap in BsonClassMap.GetRegisteredClassMaps())
        {
            foreach (var declaredMemberMap in registeredClassMap.DeclaredMemberMaps)
            {
                var serializer = declaredMemberMap.GetSerializer();
                switch (serializer)
                {
                    case TiknasMongoDbDateTimeSerializer dateTimeSerializer:
                        dateTimeSerializer.SetDateTimeKind(Kind);
                        break;
                    case NullableSerializer<DateTime> nullableSerializer:
                        {
                            var lazySerializer = nullableSerializer.GetType()
                                ?.GetField("_lazySerializer", BindingFlags.NonPublic | BindingFlags.Instance)
                                ?.GetValue(serializer)?.As<Lazy<IBsonSerializer<DateTime>>>();

                            if (lazySerializer?.Value is TiknasMongoDbDateTimeSerializer dateTimeSerializer)
                            {
                                dateTimeSerializer.SetDateTimeKind(Kind);
                            }
                            break;
                        }
                }
            }
        }
    }
}

public class DateTimeKindTests_Unspecified : MongoDB_DateTimeKind_Tests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        Kind = DateTimeKind.Unspecified;
        services.Configure<TiknasClockOptions>(x => x.Kind = Kind);
        base.AfterAddApplication(services);
    }
}

[Collection(MongoTestCollection.Name)]
public class DateTimeKindTests_Local : MongoDB_DateTimeKind_Tests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        Kind = DateTimeKind.Local;
        services.Configure<TiknasClockOptions>(x => x.Kind = Kind);
        base.AfterAddApplication(services);
    }
}

[Collection(MongoTestCollection.Name)]
public class DateTimeKindTests_Utc : MongoDB_DateTimeKind_Tests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        Kind = DateTimeKind.Utc;
        services.Configure<TiknasClockOptions>(x => x.Kind = Kind);
        base.AfterAddApplication(services);
    }
}

[Collection(MongoTestCollection.Name)]
public class DisableDateTimeKindTests : TestAppTestBase<TiknasMongoDbTestModule>
{
    protected IPersonRepository PersonRepository { get; }

    public DisableDateTimeKindTests()
    {
        PersonRepository = GetRequiredService<IPersonRepository>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<TiknasMongoDbOptions>(x => x.UseTiknasClockHandleDateTime = false);
        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task DateTime_Kind_Should_Be_Normalized_By_MongoDb_Test()
    {
        var personId = Guid.NewGuid();
        await PersonRepository.InsertAsync(new Person(personId, "bob lee", 18)
        {
            Birthday = DateTime.Parse("2020-01-01 00:00:00"),
            LastActive = DateTime.Parse("2020-01-01 00:00:00"),
        }, true);

        var person = await PersonRepository.GetAsync(personId);

        person.ShouldNotBeNull();
        person.CreationTime.Kind.ShouldBe(DateTimeKind.Utc);

        person.Birthday.ShouldNotBeNull();
        person.Birthday.Value.Kind.ShouldBe(DateTimeKind.Utc);
    }
}
