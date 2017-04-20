using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Akeneo.Serialization
{
	public class AkeneoSerializerSettings
	{
		public static readonly JsonSerializerSettings Instance = new JsonSerializerSettings
		{
			Converters = { new ProductConverter() },
			ContractResolver = new CamelCasePropertyNamesContractResolver
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			},
			NullValueHandling = NullValueHandling.Ignore
		};
	}
}
