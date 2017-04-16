using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Akeneo.Client.Converters
{
	public class ResponseConverter : JsonConverter
	{
		private static readonly Type ProductValueType = typeof(AkeneoResponse);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var response = new AkeneoResponse();

			while (reader.Read())
			{
				if (reader.TokenType != JsonToken.PropertyName)
				{
					continue;
				}
				if (string.Equals(reader.Value.ToString(), "code"))
				{
					var code = reader.ReadAsInt32();
					response.Code = (HttpStatusCode) code;
					continue;
				}
				if (string.Equals(reader.Value.ToString(), "message"))
				{
					response.Message = reader.ReadAsString();
					continue;
				}
				if (string.Equals(reader.Value.ToString(), "_links"))
				{
					reader.Read();
					response.Links = serializer.Deserialize<Dictionary<string, PaginationLink>>(reader);
					continue;
				}
				if (string.Equals(reader.Value.ToString(), "errors"))
				{
					reader.Read();
					response.Errors = serializer.Deserialize<List<ValidationError>>(reader);
				}
			}
			return response;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == ProductValueType;
		}
	}
}
