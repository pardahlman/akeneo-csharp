using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akeneo.Search
{
	public class ProductValue : Criteria
	{
		public string Scope { get; set; }
		public string Locale { get; set; }

		[JsonIgnore]
		public string AttributeCode { get; set; }

		public static ProductValue StartsWith(string attributeCode, string value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.StartsWith,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue EndsWith(string attributeCode, string value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.EndsWith,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue Contains(string attributeCode, string value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.Contains,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue DoesNotContain(string attributeCode, string value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.DoesNotContain,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue Empty(string attributeCode, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.Empty,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue NotEmpty(string attributeCode, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.Empty,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue Equal(string attributeCode, object value,string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.Equal,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue NotEqual(string attributeCode, object value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.NotEqual,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue Greater(string attributeCode, int value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.Greater,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue GreaterOrEqual(string attributeCode, int value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.GreaterOrEqual,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue Lower(string attributeCode, int value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.Lower,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue LowerOrEqual(string attributeCode, int value, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.LowerOrEqual,
				Value = value,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue Between(string attributeCode, DateTime from, DateTime to, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.Between,
				Value = new List<DateTime>{from, to},
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue NotBetween(string attributeCode, DateTime from, DateTime to, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.NotBetween,
				Value = new List<DateTime> { from, to },
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue In(string attributeCode, string optionCode, string scope = null, string locale = null)
		{
			return In(attributeCode, new List<string> {optionCode}, scope, locale);
		}

		public static ProductValue In(string attributeCode, List<string> optionCode, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.In,
				Value = optionCode,
				Scope = scope,
				Locale = locale
			};
		}

		public static ProductValue NotIn(string attributeCode, string optionCode, string scope = null, string locale = null)
		{
			return NotIn(attributeCode, new List<string> {optionCode}, scope, locale);
		}

		public static ProductValue NotIn(string attributeCode, List<string> optionCode, string scope = null, string locale = null)
		{
			return new ProductValue
			{
				AttributeCode = attributeCode,
				Operator = Operators.NotIn,
				Value = optionCode,
				Scope = scope,
				Locale = locale
			};
		}
	}
}
