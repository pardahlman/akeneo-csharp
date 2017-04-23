using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Akeneo.Serialization
{
	public class AkeneoSerializerSettings
	{
		public static readonly IContractResolver AkeneoContractResolver = new CamelCasePropertyNamesContractResolver
		{
			NamingStrategy = new SnakeCaseNamingStrategy()
		};

		public static readonly JsonSerializerSettings Instance = new JsonSerializerSettings
		{
			Converters = { new ProductConverter(), new AttributeBaseConverter() },
			ContractResolver = AkeneoContractResolver,
			NullValueHandling = NullValueHandling.Ignore
		};
	}
}
