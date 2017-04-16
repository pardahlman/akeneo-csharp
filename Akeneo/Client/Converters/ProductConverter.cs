using System;
using Akeneo.Model;
using Newtonsoft.Json;

namespace Akeneo.Client.Converters
{
	public class ProductConverter : JsonConverter
	{
		private static readonly Type ProductValueType = typeof(ProductValue);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var productValue = value as ProductValue;

			writer.WriteStartObject();

			writer.WritePropertyName("data");
			serializer.Serialize(writer, productValue.Data);

			writer.WritePropertyName("locale");
			if (string.IsNullOrEmpty(productValue.Locale))
			{
				writer.WriteNull();
			}
			else
			{
				writer.WriteValue(productValue.Locale);
			}

			writer.WritePropertyName("scope");
			if (string.IsNullOrEmpty(productValue.Scope))
			{
				writer.WriteNull();
			}
			else
			{
				writer.WriteValue(productValue.Scope);
			}

			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var result = new ProductValue();
			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.EndObject)
				{
					break;
				}
				if (reader.TokenType != JsonToken.PropertyName)
				{
					continue;
				}
				var propName = reader.Value.ToString();
				if (string.Equals(propName, "data"))
				{
					reader.Read();
					result.Data = serializer.Deserialize<object>(reader);
					continue;
				}
				if (string.Equals(propName, "locale"))
				{
					result.Locale = reader.ReadAsString();
					continue;
				}
				if (string.Equals(propName, "scope"))
				{
					result.Scope = reader.ReadAsString();
				}
			}
			reader.Read();
			return result;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == ProductValueType;
		}
	}
}
