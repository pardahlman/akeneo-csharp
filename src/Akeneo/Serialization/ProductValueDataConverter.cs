using System;
using System.Collections.Generic;
using System.Linq;
using Akeneo.Client;
using Akeneo.Model.ProductValues;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Akeneo.Serialization
{
	public class ProductValueDataConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
                if(reader.Value == null)
                    return reader.Value;

                if (float.TryParse((string)reader.Value, out float number))
				{
					return number;
				}
				return reader.Value;
			}

			if (reader.TokenType == JsonToken.Boolean)
			{
				return reader.Value;
			}

			if (reader.TokenType == JsonToken.StartObject)
			{
				var obj = JObject.Load(reader);
				if (IsMetricValue(obj))
				{
                    var metricVal = new MetricProductValue
                    {
                        Amount = obj["amount"].Value<string>() != null ? obj["amount"].ToObject<float>() : float.NaN,
                        Unit = obj["unit"].ToObject<string>()
                    };
					return metricVal;
				}
			}

			if (reader.TokenType == JsonToken.StartArray)
			{
				var array = JArray.Load(reader);
				if (!array.HasValues)
				{
					return Enumerable.Empty<object>();
				}
				if (array.First.Type == JTokenType.String)
				{
					return array.ToObject<List<string>>();
				}
				if (array.First.Type == JTokenType.Object)
				{
				    var items = array
                        .Select(x => new PriceProductValue
				        {
                            Amount = x["amount"].Value<string>() != null ? x["amount"].ToObject<float>() : float.NaN,
                            Currency = x["currency"].ToObject<string>()

                        }).ToList();

				    return items;
				    //return array.ToObject<List<PriceProductValue>>();
				}
				return serializer.Deserialize<List<PriceProductValue>>(reader);
			}

			return reader.Value;
		}

		private static bool IsMetricValue(JObject obj)
		{
			return obj.Property("unit") != null;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}
