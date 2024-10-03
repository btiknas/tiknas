using System.Text.Json;
using Shouldly;
using Tiknas.Json.SystemTextJson.JsonConverters;
using Xunit;

namespace Tiknas.Json;

public class TiknasStringToBoolean_Tests
{
    [Fact]
    public void Test_Read()
    {
            var options = new JsonSerializerOptions()
            {
                Converters =
                {
                    new TiknasStringToBooleanConverter()
                }
            };

            var testClass = JsonSerializer.Deserialize<TestClass>("{\"Enabled\": \"TrUe\"}", options);
            testClass.ShouldNotBeNull();
            testClass.Enabled.ShouldBe(true);

            testClass = JsonSerializer.Deserialize<TestClass>("{\"Enabled\": true}", options);
            testClass.ShouldNotBeNull();
            testClass.Enabled.ShouldBe(true);
        }

    [Fact]
    public void Test_Write()
    {
            var options = new JsonSerializerOptions()
            {
                Converters =
                {
                    new TiknasStringToBooleanConverter()
                }
            };

            var testClassJson = JsonSerializer.Serialize(new TestClass()
            {
                Enabled = true
            });

            testClassJson.ShouldBe("{\"Enabled\":true}");
        }

    class TestClass
    {
        public bool Enabled { get; set; }
    }
}