using System;
using System.Collections.Generic;
using System.Linq;
using Akeneo.Client;
using Akeneo.Model;
using Akeneo.Model.Attributes;

namespace Akeneo.Extensions
{
	public static class AttributeBaseExtensions
	{
		public static KeyValuePair<string, List<ProductValue>> CreateValues(this AttributeBase attribute, params ProductValue[] productValues)
		{
			return new KeyValuePair<string, List<ProductValue>>(attribute.Code, productValues.ToList());
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this SimpleSelectAttribute attribute, string data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this MultiSelectAttribute attribute, string data, string locale = null, string scope = null)
		{
			return attribute.CreateValue(new List<string>{data}, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this MultiSelectAttribute attribute, IEnumerable<string> data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this NumberAttribute attribute, float data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object) data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this NumberAttribute attribute, int data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this NumberAttribute attribute, decimal data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this NumberAttribute attribute, double data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this TextAttribute attribute, string data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this TextAreaAttribute attribute, string data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this DateAttribute attribute, DateTime data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this PriceAttribute attribute, float amount, string currency, string locale = null, string scope = null)
		{
			return attribute.CreateValue(new PriceProductValue{Amount = amount, Currency = currency}, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this PriceAttribute attribute, PriceProductValue data, string locale = null, string scope = null)
		{
			return attribute.CreateValue(new List<PriceProductValue>{data}, locale, scope);
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this PriceAttribute attribute, IEnumerable<PriceProductValue> data, string locale = null, string scope = null)
		{
			return attribute.CreateValue((object)data, locale, scope);
		}

		private static KeyValuePair<string, List<ProductValue>> CreateValue(this AttributeBase attribute, object data, string locale = null, string scope = null)
		{
			return new KeyValuePair<string, List<ProductValue>>(attribute.Code, new List<ProductValue>{new ProductValue
			{
				Data = data,
				Locale = locale,
				Scope = scope
			}});
		}

		public static KeyValuePair<string, List<ProductValue>> CreateValue(this KeyValuePair<string, List<ProductValue>> kvp, object data, string locale = null, string scope = null)
		{
			kvp.Value.Add(new ProductValue{Data = data, Locale = locale, Scope = scope});
			return kvp;
		}
	}
}
