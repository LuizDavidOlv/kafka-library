using Confluent.Kafka;
using System.Text.Json;

namespace Kafka.Config
{
    public class Serializer<T> : ISerializer<T> where T : notnull
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = false
        };

        public byte[] Serialize(T data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data, jsonSerializerOptions);
        }
    }
}
