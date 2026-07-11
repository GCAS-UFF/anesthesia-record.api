using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UFF.FichaAnestesica.Domain.Helpers
{
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }     

    // Para DateTime NÃO NULLABLE (data_registro, data_cirurgia, etc.)
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (string.IsNullOrEmpty(str))
                return default;

            str = str.Replace(" ", "T");

            // Remove milissegundos se existirem (ex: .987384)
            if (str.Contains('.'))
                str = str.Substring(0, str.IndexOf('.'));

            return DateTime.Parse(str, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    // Para DateTime NULLABLE
    public class CustomNullableDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (string.IsNullOrEmpty(str))
                return null;

            str = str.Replace(" ", "T");

            if (str.Contains('.'))
                str = str.Substring(0, str.IndexOf('.'));

            return DateTime.Parse(str, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            else
                writer.WriteNullValue();
        }
    }

    // Para peso_kg (vem como string no JSON)
    public class StringToDoubleConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                return double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0;
            }

            return reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
