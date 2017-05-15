using System;
using System.Net.Http;
using System.Threading.Tasks;
using Akeneo.Common;
using Akeneo.Serialization;
using Newtonsoft.Json;

namespace Akeneo.Http
{
	public static class HttpContentExtensions
	{
		public static async Task<TType> ReadAsJsonAsync<TType>(this HttpContent content, JsonSerializerSettings settings = null)
		{
			if (!string.Equals(content.Headers.ContentType.MediaType, JsonContent.MediaType, StringComparison.CurrentCultureIgnoreCase))
			{
				return default(TType);
			}
			var serialied = await content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<TType>(serialied, settings ?? AkeneoSerializerSettings.Create);
		}
	}
}
