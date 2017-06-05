using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Akeneo.Serialization;
using Newtonsoft.Json;

namespace Akeneo.Search
{
	public class SearchQueryBuilder
	{
		private const string SearchStart = "?search=";
		private const string SearchLocale = "&search_locale=";
		private const string SearchScope = "&search_scope=";

		public string GetQueryString(IEnumerable<Criteria> criterias)
		{
			var searchDictionary = GetSearchDictionary(criterias);
			var channel = GetChannels(criterias);
			var locale = GetLocales(criterias);
			return GetQueryString(searchDictionary, channel, locale);
		}

		public string GetQueryString(Dictionary<string, List<Criteria>> criterias, string scope = null, string locale = null)
		{
			if (!criterias?.Keys.Any() ?? true)
			{
				throw new ArgumentException("Unable to build search query without any provided criteras.");
			}

			var builder = new StringBuilder(SearchStart);
			builder.Append(JsonConvert.SerializeObject(criterias, AkeneoSerializerSettings.Create));

			if (!string.IsNullOrEmpty(scope))
			{
				builder.Append(SearchScope);
				builder.Append(scope);
			}

			if (!string.IsNullOrEmpty(locale))
			{
				builder.Append(SearchLocale);
				builder.Append(locale);
			}

			return builder.ToString();
		}

		public Dictionary<string, List<Criteria>> GetSearchDictionary(IEnumerable<Criteria> criterias)
		{
			criterias = criterias.ToList();
			var result = new Dictionary<string, List<Criteria>>();
			Add<Category>(result, criterias, Category.Key);
			Add<Status>(result, criterias, Status.Key);
			Add<Completeness>(result, criterias, Completeness.Key);
			Add<GroupCriteria>(result, criterias, GroupCriteria.Key);
			Add<Family>(result, criterias, Family.Key);
			Add<Created>(result, criterias, Created.Key);
			Add<UpdatedCriteria>(result, criterias, UpdatedCriteria.Key);
			AddProductValueCriterias(result, criterias);

			return result;
		}

		private static void Add<TCriteria>(IDictionary<string, List<Criteria>> dictionary, IEnumerable<Criteria> all, string key) where TCriteria : Criteria
		{
			var matching = all.OfType<TCriteria>().ToList<Criteria>();
			if (matching.Any())
			{
				dictionary.Add(key, matching);
			}
		}

		private static void AddProductValueCriterias(IDictionary<string, List<Criteria>> dictionary, IEnumerable<Criteria> all)
		{
			var productValues = all.OfType<ProductValue>();
			var attributeGroup = productValues.GroupBy(p => p.AttributeCode);
			foreach (var attributeGrp in attributeGroup)
			{
				dictionary.Add(attributeGrp.Key, attributeGrp.ToList<Criteria>());
			}
		}

		public string GetChannels(IEnumerable<Criteria> criterias)
		{
			return criterias
				.OfType<ScopeCriteria>()
				.FirstOrDefault()?.Value as string;
		}

		public string GetLocales(IEnumerable<Criteria> criterias)
		{
			return criterias
				.OfType<LocaleCriteria>()
				.FirstOrDefault()?.Value as string;
		}
	}
}
