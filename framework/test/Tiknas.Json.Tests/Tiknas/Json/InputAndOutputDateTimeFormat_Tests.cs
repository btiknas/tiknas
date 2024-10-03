using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Timing;
using Xunit;

namespace Tiknas.Json;

public class InputAndOutputDateTimeFormatSystemTextJsonTests : TiknasJsonSystemTextJsonTestBase
{
    private readonly IJsonSerializer _jsonSerializer;

    public InputAndOutputDateTimeFormatSystemTextJsonTests()
    {
        _jsonSerializer = GetRequiredService<IJsonSerializer>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<TiknasJsonOptions>(options =>
        {
            options.InputDateTimeFormats = new List<string>()
            {
                "yyyy*MM*dd",
                "yyyy-MM-dd HH:mm:ss"
            };
            options.OutputDateTimeFormat = "yyyy*MM-dd HH:mm:ss";
        });
        services.Configure<TiknasClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }

    [Fact]
    public void InputAndOutputDateTimeFormat_Test()
    {
        var resultDate = new DateTime(2016, 04, 13, 08, 58, 10, DateTimeKind.Utc);
        var json = _jsonSerializer.Serialize(new DateTimeDto()
        {
            DateTime1 = resultDate,
            DateTime2 = resultDate
        });
        json.ShouldContain("\"DateTime1\":\"2016*04-13 08:58:10\"");
        json.ShouldContain("\"DateTime2\":\"2016*04-13 08:58:10\"");

        var dto = _jsonSerializer.Deserialize<DateTimeDto>("{\"DateTime1\":\"" + resultDate.ToString("yyyy*MM*dd") + "\",\"DateTime2\":\"" + resultDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"}");
        dto.DateTime1.ShouldBe(resultDate.Date);
        dto.DateTime1.Kind.ShouldBe(DateTimeKind.Utc);
        dto.DateTime2.ShouldBe(resultDate);
        dto.DateTime2.Value.Kind.ShouldBe(DateTimeKind.Utc);
    }

    public class DateTimeDto
    {
        public DateTime DateTime1 { get; set; }
        public DateTime? DateTime2 { get; set; }
    }
}

public class InputAndOutputDateTimeFormatNewtonsoftTests : TiknasJsonNewtonsoftJsonTestBase
{
    private readonly IJsonSerializer _jsonSerializer;

    public InputAndOutputDateTimeFormatNewtonsoftTests()
    {
        _jsonSerializer = GetRequiredService<IJsonSerializer>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<TiknasJsonOptions>(options =>
        {
            options.InputDateTimeFormats = new List<string>()
            {
                "yyyy*MM*dd",
                "yyyy-MM-dd HH:mm:ss"
            };
            options.OutputDateTimeFormat = "yyyy*MM-dd HH:mm:ss";
        });
        services.Configure<TiknasClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }

    [Fact]
    public void InputAndOutputDateTimeFormat_Test()
    {
        var resultDate = new DateTime(2016, 04, 13, 08, 58, 10, DateTimeKind.Utc);
        var json = _jsonSerializer.Serialize(new DateTimeDto()
        {
            DateTime1 = resultDate,
            DateTime2 = resultDate
        });
        json.ShouldContain("\"DateTime1\":\"2016*04-13 08:58:10\"");
        json.ShouldContain("\"DateTime2\":\"2016*04-13 08:58:10\"");

        var dto = _jsonSerializer.Deserialize<DateTimeDto>("{\"DateTime1\":\"" + resultDate.ToString("yyyy*MM*dd") + "\",\"DateTime2\":\"" + resultDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"}");
        dto.DateTime1.ShouldBe(resultDate.Date);
        dto.DateTime1.Kind.ShouldBe(DateTimeKind.Utc);
        dto.DateTime2.ShouldBe(resultDate);
        dto.DateTime2.Value.Kind.ShouldBe(DateTimeKind.Utc);
    }

    public class DateTimeDto
    {
        public DateTime DateTime1 { get; set; }
        public DateTime? DateTime2 { get; set; }
    }
}
