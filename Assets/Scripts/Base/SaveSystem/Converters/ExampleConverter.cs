using System;
using Base.SaveSystem.Data;
using Base.SaveSystem.Interfaces;
using Base.SaveSystem.Other;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Base.SaveSystem.Converters
{
    public class ExampleConverter : JsonConverter<ExampleData>, IConverter<ExampleData>
    {
        public override void WriteJson(JsonWriter writer, ExampleData value, JsonSerializer serializer)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartObject();

            writer.WritePropertyName(ExampleKeys.ITEMS);
            writer.WriteStartArray();

            foreach(ExampleDataItem item in value.ExampleDataItems)
            {
                writer.WriteStartObject();
                writer.WriteField(ExampleKeys.INT, item.IntExample);
                writer.WriteField(ExampleKeys.BOOL, item.BoolExample);
                writer.WriteField(ExampleKeys.STRING, item.StringExample);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public override ExampleData ReadJson(JsonReader reader, Type objectType, ExampleData existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ExampleData result = new ExampleData();

            if(reader.TokenType == JsonToken.Null)
            {
                return result;
            }

            JObject jObject = JObject.Load(reader);

            if(jObject[ExampleKeys.ITEMS] is JArray itemsArray)
            {
                foreach(JToken jToken in itemsArray)
                {
                    ExampleDataItem newItem = new ExampleDataItem()
                    {
                        IntExample = jToken.ReadInt(ExampleKeys.INT),
                        BoolExample = jToken.ReadBool(ExampleKeys.BOOL),
                        StringExample = jToken.ReadString(ExampleKeys.STRING)
                    };

                    result.ExampleDataItems.Add(newItem);
                }
            }

            return result;
        }

        public JsonConverter Converter
        {
            get
            {
                return this;
            }
        }
        private class ExampleKeys
        {
            public const string ITEMS = "Items";
            public const string INT = "IntItem";
            public const string BOOL = "BoolItem";
            public const string STRING = "StringItem";
        }
    }
}