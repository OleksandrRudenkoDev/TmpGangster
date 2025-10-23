using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SaveSystem.Other
{
    public static class JsonConverterBaseExtensions
    {
        public static void WriteField(this JsonWriter writer, string key, string value)
        {
            writer.WritePropertyName(key);
            writer.WriteValue(value);
        }

        public static void WriteField(this JsonWriter writer, string key, float value)
        {
            writer.WritePropertyName(key);
            writer.WriteValue(value);
        }

        public static void WriteField(this JsonWriter writer, string key, int value)
        {
            writer.WritePropertyName(key);
            writer.WriteValue(value);
        }

        public static void WriteField(this JsonWriter writer, string key, bool value)
        {
            writer.WritePropertyName(key);
            writer.WriteValue(value);
        }

        public static string ReadString(this JObject jObject, string key)
        {
            return jObject[key]?.Value<string>() ?? string.Empty;
        }

        public static float ReadFloat(this JObject jObject, string key)
        {
            return jObject[key]?.Value<float>() ?? 0;
        }

        public static int ReadInt(this JObject jObject, string key)
        {
            return jObject[key]?.Value<int>() ?? 0;
        }

        public static bool ReadBool(this JObject jObject, string key)
        {
            return jObject[key]?.Value<bool>() ?? false;
        }

        public static string ReadString(this JToken jObject, string key)
        {
            return jObject[key]?.Value<string>() ?? string.Empty;
        }

        public static float ReadFloat(this JToken jObject, string key)
        {
            return jObject[key]?.Value<float>() ?? 0;
        }

        public static int ReadInt(this JToken jObject, string key)
        {
            return jObject[key]?.Value<int>() ?? 0;
        }

        public static bool ReadBool(this JToken jObject, string key)
        {
            return jObject[key]?.Value<bool>() ?? false;
        }
    }

}