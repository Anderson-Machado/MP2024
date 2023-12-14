using System.Text.Json;
using System.Text.Json.Serialization;

namespace MP.CrossCutting.Utils.Converts
{
    public class IsoDateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return DateTime.Parse(reader.GetString());
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd"));
        }
    }
}