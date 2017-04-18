using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akeneo.Client.Converters
{
	public class ProductValueDataConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			// TODO: Look into this
			if (reader.TokenType == JsonToken.StartArray)
			{
				return serializer.Deserialize<List<PriceProductValue>>(reader);
			}
			return reader.Value;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}
