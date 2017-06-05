using System;
using Akeneo.Model;
using Newtonsoft.Json;

namespace Akeneo.Serialization
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
			var r = new ProductValue();
			serializer.Populate(reader, r);
			return r;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == ProductValueType;
		}
	}
}
