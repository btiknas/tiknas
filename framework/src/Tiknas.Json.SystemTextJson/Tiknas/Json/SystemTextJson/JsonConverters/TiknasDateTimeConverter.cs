using System;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;
using Tiknas.Timing;

namespace Tiknas.Json.SystemTextJson.JsonConverters;

public class TiknasDateTimeConverter : JsonConverter<DateTime>, ITransientDependency
{
    private readonly IClock _clock;
    private readonly TiknasJsonOptions _options;

    public TiknasDateTimeConverter(IClock clock, IOptions<TiknasJsonOptions> tiknasJsonOptions)
    {
        _clock = clock;
        _options = tiknasJsonOptions.Value;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (_options.InputDateTimeFormats.Any())
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                foreach (var format in _options.InputDateTimeFormats)
                {
                    var s = reader.GetString();
                    if (DateTime.TryParseExact(s, format, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var d1))
                    {
                        return _clock.Normalize(d1);
                    }
                }
            }
            else
            {
                throw new JsonException("Reader's TokenType is not String!");
            }
        }

        if (reader.TryGetDateTime(out var d3))
        {
            return _clock.Normalize(d3);
        }

        var dateText = reader.GetString();
        if (!dateText.IsNullOrWhiteSpace())
        {
            if (DateTime.TryParse(dateText, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var d4))
            {
                return _clock.Normalize(d4);
            }
        }

        throw new JsonException("Can't get datetime from the reader!");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        if (_options.OutputDateTimeFormat.IsNullOrWhiteSpace())
        {
            writer.WriteStringValue(_clock.Normalize(value));
        }
        else
        {
            writer.WriteStringValue(_clock.Normalize(value).ToString(_options.OutputDateTimeFormat, CultureInfo.CurrentUICulture));
        }
    }
}
