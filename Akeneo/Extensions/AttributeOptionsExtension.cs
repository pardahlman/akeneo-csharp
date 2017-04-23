using System;
using System.Collections.Generic;
using System.Linq;
using Akeneo.Model;
using Akeneo.Model.Attributes;

namespace Akeneo.Extensions
{
	public static class AttributeOptionsExtension
	{
		public static KeyValuePair<string, List<ProductValue>> CreateSimpleValue(this AttributeOption option, string locale = null, string scope = null)
		{
			return new KeyValuePair<string, List<ProductValue>>(option.Attribute, new List<ProductValue>
			{
				new ProductValue
				{
					Locale = locale,
					Data = option.Code,
					Scope = scope
				}
			});
		}

		public static KeyValuePair<string, List<ProductValue>> CreateMultiValue(this AttributeOption option, string locale = null, string scope = null)
		{
			return new KeyValuePair<string, List<ProductValue>>(option.Attribute, new List<ProductValue>
			{
				new ProductValue
				{
					Locale = locale,
					Data = new List<string>{option.Code},
					Scope = scope
				}
			});
		}

		public static KeyValuePair<string, List<ProductValue>> CreateMultiValues(this IEnumerable<AttributeOption> option, string locale = null, string scope = null)
		{
			var optionGrps = option
				.GroupBy(o => o.Attribute, o => o.Code)
				.ToList();

			if (optionGrps.Count != 1)
			{
				throw new NotSupportedException("Can only create Values for same type of option");
			}

			var optionGrp = optionGrps.First();
			return new KeyValuePair<string, List<ProductValue>>(optionGrp.Key, new List<ProductValue>{ new ProductValue
			{
				Data = optionGrp.ToList(),
				Locale = locale,
				Scope = scope
			} });
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this KeyValuePair<string, List<ProductValue>> kvp, string data, string locale = null, string scope = null)
		{
			kvp.Value.Add(new ProductValue
			{
				Locale = locale,
				Scope = scope,
				Data = data
			});
			return kvp;
		}
	}
}
