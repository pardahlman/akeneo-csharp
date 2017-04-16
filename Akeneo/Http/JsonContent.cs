using System.Net.Http;
using System.Text;
using Akeneo.Common;
using Newtonsoft.Json;

namespace Akeneo.Http
{
	public class JsonContent : StringContent
	{
		public const string MediaType = "application/json";

		public JsonContent(object content) : this(content, AkeneoSerializerSettings.Instance){ }

		public JsonContent(object content, JsonSerializerSettings settings) 
			: base(
				JsonConvert.SerializeObject(content, settings),
				Encoding.UTF8,
				MediaType
			) { }
	}
}
