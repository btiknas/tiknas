﻿using Shouldly;
using Tiknas.Data;
using Tiknas.ObjectExtending.TestObjects;
using Xunit;

namespace Tiknas.ObjectExtending;

public class ExtensibleObject_Tests : TiknasObjectExtendingTestBase
{
    [Fact]
    public void Should_Set_Default_Values_For_Defined_Properties_On_Create()
    {
        var person = new ExtensibleTestPerson();

        person.HasProperty("Name").ShouldBeTrue();
        person.HasProperty("Age").ShouldBeTrue();
        person.HasProperty("NoPairCheck").ShouldBeTrue();
        person.HasProperty("CityName").ShouldBeTrue();
        person.HasProperty("EnumProperty").ShouldBeTrue();

        person.GetProperty<string>("Name").ShouldBeNull();
        person.GetProperty<int>("Age").ShouldBe(0);
        person.GetProperty<string>("NoPairCheck").ShouldBeNull();
        person.GetProperty<string>("CityName").ShouldBeNull();
        person.GetProperty<ExtensibleTestEnumProperty>("EnumProperty").ShouldBe(default);
    }

    [Fact]
    public void Should_Not_Set_Default_Values_For_Defined_Properties_If_Requested()
    {
        var person = new ExtensibleTestPerson(false);

        person.HasProperty("Name").ShouldBeFalse();
        person.HasProperty("Age").ShouldBeFalse();
        person.HasProperty("NoPairCheck").ShouldBeFalse();
        person.HasProperty("CityName").ShouldBeFalse();
        person.HasProperty("EnumProperty").ShouldBeFalse();
    }
}
