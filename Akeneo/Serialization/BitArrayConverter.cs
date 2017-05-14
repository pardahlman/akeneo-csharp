using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Akeneo.Serialization
{
	public class BitArrayConverter : JsonConverter
	{
		private static readonly Type BitArray = typeof(BitArray);
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is BitArray bitArray)
			{
				var builder = new StringBuilder();
				foreach (bool b in bitArray)
				{
					builder.Append(b ? "1" : "0");
				}
				writer.WriteValue(builder.ToString());
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return BitArray == objectType;
		}
	}
}
