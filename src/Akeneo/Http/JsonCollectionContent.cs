using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Akeneo.Serialization;

namespace Akeneo.Http
{
	public class JsonCollectionContent<TType> : StringContent
	{
		public const string MediaType = "application/vnd.akeneo.collection+json";

		public JsonCollectionContent(IEnumerable<TType> content)
			: this(content, Encoding.UTF8)
		{ }

		public JsonCollectionContent(IEnumerable<TType> content, Encoding encoding)
			: base(Convert(content), encoding, MediaType)
		{ }

		private static string Convert(IEnumerable<TType> content)
		{
			return AkeneoCollectionSerializer.Serialize(content);
		}
	}
}
