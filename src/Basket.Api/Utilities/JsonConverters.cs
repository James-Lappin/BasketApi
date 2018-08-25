using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Basket.Api.Utilities
{
    /// <summary>
    /// Converts a long id into a string
    /// </summary>
    /// <remarks>
    /// Json doesnt have a int 64 type. For it to be valid Json I need to convert it to a string.
    /// <remarks>
    public class IdToStringConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jt = JValue.ReadFrom(reader);

            return jt.Value<long>();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(System.Int64).Equals(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}