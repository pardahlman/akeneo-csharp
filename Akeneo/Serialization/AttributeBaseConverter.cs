using System;
using System.Reflection;
using Akeneo.Model;
using Akeneo.Model.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Akeneo.Serialization
{
	public class AttributeBaseConverter : JsonConverter
	{
		private static readonly Type Attribute = typeof(AttributeBase);
		private const string AttributeTypeProp = "type";

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);
			var attributeType = obj.Property(AttributeTypeProp)?.Value.ToString();
			switch (attributeType)
			{
				case AttributeType.Identifier:
					return obj.ToObject<IdentifierAttribute>();
				case AttributeType.Boolean:
					return obj.ToObject<BooleanAttribute>();
				case AttributeType.Date:
					return obj.ToObject<DateAttribute>();
				case AttributeType.Metric:
					return obj.ToObject<MetricAttribute>();
				case AttributeType.Number:
					return obj.ToObject<NumberAttribute>();
				case AttributeType.Price:
					return obj.ToObject<PriceAttribute>();
				case AttributeType.SimpleSelect:
					return obj.ToObject<SimpleSelectAttribute>();
				case AttributeType.Text:
					return obj.ToObject<TextAttribute>();
				case AttributeType.TextArea:
					return obj.ToObject<TextAreaAttribute>();
				case AttributeType.MultiSelect:
					return obj.ToObject<MulticastDelegate>();
				case AttributeType.File:
					return obj.ToObject<FileAttribute>();
				case AttributeType.Image:
					return obj.ToObject<ImageAttribute>();
				default:
					return obj.ToObject<GenericAttribute>();
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return Attribute.GetTypeInfo().IsAssignableFrom(objectType);
		}
	}
}
