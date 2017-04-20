using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Akeneo.Common;
using Akeneo.Serialization;
using Newtonsoft.Json;

namespace Akeneo.Http
{
	public class AkeneoCollectionSerializer
	{
		private static readonly Regex JsonObjects = new Regex("{.*}", RegexOptions.Compiled);

		public static string Serialize<TModel>(IEnumerable<TModel> collection)
		{
			var contentBuilder = new StringBuilder();
			foreach (var obj in collection)
			{
				contentBuilder.Append(JsonConvert.SerializeObject(obj, AkeneoSerializerSettings.Instance));
				contentBuilder.AppendLine();
			}
			var serializedContent = contentBuilder.ToString();
			return serializedContent;
		}

		public static IEnumerable<TType> Deserialize<TType>(string serialized)
		{
			var matches = JsonObjects.Matches(serialized);
			foreach (Match match in matches)
			{
				yield return JsonConvert.DeserializeObject<TType>(match.Value, AkeneoSerializerSettings.Instance);
			}
		}
	}
}
