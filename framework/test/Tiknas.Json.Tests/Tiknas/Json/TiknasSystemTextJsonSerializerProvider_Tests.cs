using System;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Data;
using Tiknas.Json.SystemTextJson;
using Tiknas.ObjectExtending;
using Tiknas.Timing;
using Xunit;

namespace Tiknas.Json;

public abstract class TiknasSystemTextJsonSerializerProviderTestBase : TiknasJsonSystemTextJsonTestBase
{
    protected TiknasSystemTextJsonSerializer JsonSerializer;

    public TiknasSystemTextJsonSerializerProviderTestBase()
    {
        JsonSerializer = GetRequiredService<TiknasSystemTextJsonSerializer>();
    }

    public class TestExtensibleObjectClass : ExtensibleObject
    {
        public string Name { get; set; }
    }

    public class FileWithBoolean
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class FileWithNullableBoolean
    {
        public string Name { get; set; }

        public bool? IsDeleted { get; set; }
    }

    public class FileWithEnum
    {
        public string Name { get; set; }

        public FileType Type { get; set; }
    }

    public class FileWithNullableEnum
    {
        public string Name { get; set; }

        public FileType? Type { get; set; }
    }

    public enum FileType
    {
        Zip = 0,
        Exe = 2
    }

    public class FileWithDatetime
    {
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }
    }

    public class FileWithNullableDatetime
    {
        public string Name { get; set; }

        public DateTime? CreationTime { get; set; }
    }
}

public class TiknasSystemTextJsonSerializerProviderTests : TiknasSystemTextJsonSerializerProviderTestBase
{
    [Fact]
    public void Serialize_Deserialize_With_Boolean()
    {
        var json = "{\"name\":\"tiknas\",\"IsDeleted\":\"fAlSe\"}";
        var file = JsonSerializer.Deserialize<FileWithBoolean>(json);
        file.Name.ShouldBe("tiknas");
        file.IsDeleted.ShouldBeFalse();

        file.IsDeleted = false;
        var newJson = JsonSerializer.Serialize(file);
        newJson.ShouldBe("{\"name\":\"tiknas\",\"isDeleted\":false}");
    }

    [Fact]
    public void Serialize_Deserialize_With_Nullable_Boolean()
    {
        var json = "{\"name\":\"tiknas\",\"IsDeleted\":null}";
        var file = JsonSerializer.Deserialize<FileWithNullableBoolean>(json);
        file.Name.ShouldBe("tiknas");
        file.IsDeleted.ShouldBeNull();

        var newJson = JsonSerializer.Serialize(file);
        newJson.ShouldBe("{\"name\":\"tiknas\",\"isDeleted\":null}");

        json = "{\"name\":\"tiknas\",\"IsDeleted\":\"true\"}";
        file = JsonSerializer.Deserialize<FileWithNullableBoolean>(json);
        file.IsDeleted.ShouldNotBeNull();
        file.IsDeleted.Value.ShouldBeTrue();

        newJson = JsonSerializer.Serialize(file);
        newJson.ShouldBe("{\"name\":\"tiknas\",\"isDeleted\":true}");
    }

    [Fact]
    public void Serialize_Deserialize_With_Enum()
    {
        var json = "{\"name\":\"tiknas\",\"type\":\"Exe\"}";
        var file = JsonSerializer.Deserialize<FileWithEnum>(json);
        file.Name.ShouldBe("tiknas");
        file.Type.ShouldBe(FileType.Exe);

        var newJson = JsonSerializer.Serialize(file);
        newJson.ShouldBe("{\"name\":\"tiknas\",\"type\":2}");
    }

    [Fact]
    public void Serialize_Deserialize_With_Nullable_Enum()
    {
        var json = "{\"name\":\"tiknas\",\"type\":null}";
        var file = JsonSerializer.Deserialize<FileWithNullableEnum>(json);
        file.Name.ShouldBe("tiknas");
        file.Type.ShouldBeNull();

        var newJson = JsonSerializer.Serialize(file);
        newJson.ShouldBe("{\"name\":\"tiknas\",\"type\":null}");

        json = "{\"name\":\"tiknas\",\"type\":\"Exe\"}";
        file = JsonSerializer.Deserialize<FileWithNullableEnum>(json);
        file.Type.ShouldNotBeNull();
        file.Type.ShouldBe(FileType.Exe);

        newJson = JsonSerializer.Serialize(file);
        newJson.ShouldBe("{\"name\":\"tiknas\",\"type\":2}");
    }

    [Fact]
    public void Serialize_Deserialize_ExtensibleObject()
    {
        var json = "{\"name\":\"test\",\"extraProperties\":{\"One\":\"123\",\"Two\":456}}";
        var extensibleObject = JsonSerializer.Deserialize<TestExtensibleObjectClass>(json);
        extensibleObject.GetProperty("One").ShouldBe("123");
        extensibleObject.GetProperty("Two").ShouldBe(456);

        var newJson = JsonSerializer.Serialize(extensibleObject);
        newJson.ShouldBe(json);
    }

    [Fact]
    public void Serialize_Deserialize_ExtensibleObject_Without_String()
    {
        var json = "{\"name\":\"test\"}";
        var extensibleObject = JsonSerializer.Deserialize<TestExtensibleObjectClass>(json);
        extensibleObject.ExtraProperties.ShouldNotBeNull();
        extensibleObject.ExtraProperties.ShouldBeEmpty();
    }

    [Fact]
    public void Serialize_Deserialize_ExtensibleObject_Without_Empty()
    {
        var json = "{\"name\":\"test\",\"extraProperties\":{}}";
        var extensibleObject = JsonSerializer.Deserialize<TestExtensibleObjectClass>(json);
        extensibleObject.ExtraProperties.ShouldNotBeNull();
        extensibleObject.ExtraProperties.ShouldBeEmpty();
    }

    [Fact]
    public void Serialize_Deserialize_ExtensibleObject_Without_Null()
    {
        var json = "{\"name\":\"test\",\"extraProperties\":null}";
        var extensibleObject = JsonSerializer.Deserialize<TestExtensibleObjectClass>(json);
        extensibleObject.ExtraProperties.ShouldBeNull();
    }

    [Fact]
    public void Serialize_Deserialize_With_Datetime()
    {
        var json = "{\"name\":\"tiknas\",\"creationTime\":\"2020-11-20T00:00:00\"}";
        var file = JsonSerializer.Deserialize<FileWithDatetime>(json);
        file.CreationTime.Year.ShouldBe(2020);
        file.CreationTime.Month.ShouldBe(11);
        file.CreationTime.Day.ShouldBe(20);

        var newJson = JsonSerializer.Serialize(file);
        newJson.ShouldBe(json);
    }

    [Fact]
    public void Serialize_Deserialize_With_Nullable_Datetime()
    {
        var json = "{\"name\":\"tiknas\",\"creationTime\":null}";
        var file = JsonSerializer.Deserialize<FileWithNullableDatetime>(json);
        file.CreationTime.ShouldBeNull();

        json = "{\"name\":\"tiknas\"}";
        file = JsonSerializer.Deserialize<FileWithNullableDatetime>(json);
        file.CreationTime.ShouldBeNull();

        json = "{\"name\":\"tiknas\",\"creationTime\":\"2020-11-20T00:00:00\"}";
        file = JsonSerializer.Deserialize<FileWithNullableDatetime>(json);
        file.CreationTime.ShouldNotBeNull();

        file.CreationTime.Value.Year.ShouldBe(2020);
        file.CreationTime.Value.Month.ShouldBe(11);
        file.CreationTime.Value.Day.ShouldBe(20);

        var newJson = JsonSerializer.Serialize(file);
        newJson.ShouldBe(json);
    }
}

public class TiknasSystemTextJsonSerializerProviderDateTimeFormatTests : TiknasSystemTextJsonSerializerProviderTestBase
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<TiknasJsonOptions>(options =>
        {
            options.InputDateTimeFormats.Add("yyyy*MM*dd");
            options.OutputDateTimeFormat = "yyyy*MM*dd HH*mm*ss";
        });
    }

    [Fact]
    public void Serialize_Deserialize_With_Format_Datetime()
    {
        var json = "{\"name\":\"tiknas\",\"creationTime\":\"2020*11*20\"}";
        var file = JsonSerializer.Deserialize<FileWithDatetime>(json);
        file.CreationTime.Year.ShouldBe(2020);
        file.CreationTime.Month.ShouldBe(11);
        file.CreationTime.Day.ShouldBe(20);

        json = JsonSerializer.Serialize(new FileWithDatetime()
        {
            Name = "tiknas",
            CreationTime = new DateTime(2020, 11, 20, 12, 34, 56)
        });
        json.ShouldContain("\"2020*11*20 12*34*56\"");
    }

    [Fact]
    public void Serialize_Deserialize_With_Nullable_Format_Datetime()
    {
        var json = "{\"name\":\"tiknas\",\"creationTime\":null}";
        var file = JsonSerializer.Deserialize<FileWithNullableDatetime>(json);
        file.CreationTime.ShouldBeNull();

        json = "{\"name\":\"tiknas\"}";
        file = JsonSerializer.Deserialize<FileWithNullableDatetime>(json);
        file.CreationTime.ShouldBeNull();

        json = "{\"name\":\"tiknas\",\"creationTime\":\"2020*11*20\"}";
        file = JsonSerializer.Deserialize<FileWithNullableDatetime>(json);
        file.CreationTime.ShouldNotBeNull();

        file.CreationTime.Value.Year.ShouldBe(2020);
        file.CreationTime.Value.Month.ShouldBe(11);
        file.CreationTime.Value.Day.ShouldBe(20);

        json = JsonSerializer.Serialize(new FileWithDatetime()
        {
            Name = "tiknas",
            CreationTime = new DateTime(2020, 11, 20, 12, 34, 56)
        });
        json.ShouldContain("\"2020*11*20 12*34*56\"");
    }
}

public abstract class TiknasSystemTextJsonSerializerProviderDatetimeKindTests : TiknasSystemTextJsonSerializerProviderTestBase
{
    protected DateTimeKind Kind { get; set; } = DateTimeKind.Unspecified;

    [Fact]
    public void Serialize_Deserialize()
    {
        var json = "{\"name\":\"tiknas\",\"creationTime\":\"2020-11-20T00:00:00\"}";
        var file = JsonSerializer.Deserialize<FileWithDatetime>(json);
        file.CreationTime.Kind.ShouldBe(Kind);
    }
}

public class TiknasSystemTextJsonSerializerProviderDatetimeKindUtcTests : TiknasSystemTextJsonSerializerProviderDatetimeKindTests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        Kind = DateTimeKind.Utc;
        services.Configure<TiknasClockOptions>(x => x.Kind = Kind);
    }
}

public class TiknasSystemTextJsonSerializerProviderDatetimeKindLocalTests : TiknasSystemTextJsonSerializerProviderDatetimeKindTests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        Kind = DateTimeKind.Local;
        services.Configure<TiknasClockOptions>(x => x.Kind = Kind);
    }
}

public class TiknasSystemTextJsonSerializerProviderDatetimeKindUnspecifiedTests : TiknasSystemTextJsonSerializerProviderDatetimeKindTests
{

}
