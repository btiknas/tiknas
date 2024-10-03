using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Shouldly;
using Tiknas.Json.SystemTextJson.JsonConverters;
using Tiknas.Json.SystemTextJson.Modifiers;
using Tiknas.Localization;
using Xunit;

namespace Tiknas.Json;

public class TiknasDatetimeToEnum_Tests : TiknasJsonSystemTextJsonTestBase
{
    [Theory]
    [InlineData("tr", "14.02.2024")]
    [InlineData("en-US", "2/14/2024")]
    [InlineData("en-GB", "14/02/2024")]
    public void Test_Read(string culture, string datetime)
    {
        var options = new JsonSerializerOptions()
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            {
                Modifiers =
                {
                    new TiknasDateTimeConverterModifier(
                            GetRequiredService<TiknasDateTimeConverter>(),
                            GetRequiredService<TiknasNullableDateTimeConverter>())
                        .CreateModifyAction()
                }
            }
        };

        using(CultureHelper.Use(culture))
        {
            var testClass = JsonSerializer.Deserialize<TestClass>($"{{\"DateTime\": \"{datetime}\", \"NullableDateTime\": \"{datetime}\"}}", options);
            testClass.ShouldNotBeNull();
            testClass.DateTime.ToString(CultureInfo.CurrentCulture).ShouldStartWith(datetime);
            testClass.NullableDateTime.ShouldNotBeNull();
            testClass.NullableDateTime.Value.ToString(CultureInfo.CurrentCulture).ShouldStartWith(datetime);
        }

        using(CultureHelper.Use(culture))
        {
            var testClass = JsonSerializer.Deserialize<TestClass>($"{{\"DateTime\": \"{datetime}\", \"NullableDateTime\": null}}", options);
            testClass.ShouldNotBeNull();
            testClass.DateTime.ToString(CultureInfo.CurrentCulture).ShouldStartWith(datetime);
            testClass.NullableDateTime.ShouldBeNull();
        }
    }

    class TestClass
    {
        public DateTime DateTime { get; set; }

        public DateTime? NullableDateTime { get; set; }
    }
}
