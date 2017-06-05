using System;
using Akeneo.Model.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Akeneo.Serialization
{
	public class AttributeBaseConverter : JsonConverter
	{
		private static readonly Type Attribute = typeof(AttributeBase);
		private const string AttributeTypeProp = "type";
		private static readonly JsonSerializer SnakeCaseSerialzer =
			new JsonSerializer {ContractResolver = AkeneoSerializerSettings.AkeneoContractResolver};

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value, typeof(GenericAttribute));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);
			var attributeType = obj.Property(AttributeTypeProp)?.Value.ToString();
			switch (attributeType)
			{
				case AttributeType.Identifier:
					return obj.ToObject<IdentifierAttribute>(SnakeCaseSerialzer);
				case AttributeType.Boolean:
					return obj.ToObject<BooleanAttribute>(SnakeCaseSerialzer);
				case AttributeType.Date:
					return obj.ToObject<DateAttribute>(SnakeCaseSerialzer);
				case AttributeType.Metric:
					return obj.ToObject<MetricAttribute>(SnakeCaseSerialzer);
				case AttributeType.Number:
					return obj.ToObject<NumberAttribute>(SnakeCaseSerialzer);
				case AttributeType.Price:
					return obj.ToObject<PriceAttribute>(SnakeCaseSerialzer);
				case AttributeType.SimpleSelect:
					return obj.ToObject<SimpleSelectAttribute>(SnakeCaseSerialzer);
				case AttributeType.Text:
					return obj.ToObject<TextAttribute>(SnakeCaseSerialzer);
				case AttributeType.TextArea:
					return obj.ToObject<TextAreaAttribute>(SnakeCaseSerialzer);
				case AttributeType.MultiSelect:
					return obj.ToObject<MultiSelectAttribute>(SnakeCaseSerialzer);
				case AttributeType.File:
					return obj.ToObject<FileAttribute>(SnakeCaseSerialzer);
				case AttributeType.Image:
					return obj.ToObject<ImageAttribute>(SnakeCaseSerialzer);
				default:
					return obj.ToObject<GenericAttribute>(SnakeCaseSerialzer);
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return Attribute == objectType;
		}
	}
}
