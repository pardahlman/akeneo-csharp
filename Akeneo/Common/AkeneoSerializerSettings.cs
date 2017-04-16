using Akeneo.Client.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Akeneo.Common
{
	public class AkeneoSerializerSettings
	{
		public static readonly JsonSerializerSettings Instance = new JsonSerializerSettings
		{
			Converters = { new ProductConverter(), new ResponseConverter() },
			ContractResolver = new CamelCasePropertyNamesContractResolver
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			},
			NullValueHandling = NullValueHandling.Ignore
		};
	}
}
