using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Akeneo.Common;
using Akeneo.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Akeneo.Http
{
	public class JsonContent : StringContent
	{
		public const string MediaType = "application/json";

		public JsonContent(object content) : this(content, AkeneoSerializerSettings.Create) { }

		public JsonContent(object content, JsonSerializerSettings settings)
			: base(
				content is string ? (string) content : JsonConvert.SerializeObject(content, settings),
				Encoding.UTF8,
				MediaType
			)
		{ }
	}
}
