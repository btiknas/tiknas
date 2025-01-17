﻿using Shouldly;
using Tiknas.Serialization.Objects;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Serialization;

public class ObjectSerializer_Tests : TiknasIntegratedTest<TiknasSerializationTestModule>
{
    private readonly IObjectSerializer _serializer;

    public ObjectSerializer_Tests()
    {
        _serializer = GetRequiredService<IObjectSerializer>();
    }

    [Fact]
    public void Should_Return_Null_For_Null_Object()
    {
        _serializer.Serialize<Person>(null).ShouldBeNull();
        _serializer.Deserialize<Person>((byte[])null).ShouldBeNull();
    }

    [Fact]
    public void Simple_Serialize_Deserialize()
    {
        _serializer.Deserialize<Person>(_serializer.Serialize(new Person("john"))).Name.ShouldBe("john");
    }

    [Fact]
    public void Should_Use_Specific_Serializer_If_Available()
    {
        _serializer.Deserialize<Car>(_serializer.Serialize(new Car("xc40"))).Name.ShouldBe("xc40-serialized-deserialized");
    }

    //TODO: Other methods
}
