using System.Collections.Generic;
using System.Linq;

namespace Akeneo.Common
{
	public class DictionaryFactory
	{
		public static Dictionary<TKey, TValue> Create<TKey, TValue>(params KeyValuePair<TKey, TValue>[] kvps)
		{
			return kvps.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		}
	}
}
