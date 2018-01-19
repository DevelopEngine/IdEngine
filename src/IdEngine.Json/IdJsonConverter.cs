using System;
using Newtonsoft.Json;

namespace IdEngine.Json
{
    internal class IdJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Id);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var s = reader.ReadAsString();
            return Id.TryParse(s, out var c)
                ? c
                : null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Id)value).ToString());
        }
    }
}
