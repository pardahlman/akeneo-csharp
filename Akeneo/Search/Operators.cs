namespace Akeneo.Search
{
	public class Operators
	{
		public const string In = "IN";
		public const string NotIn = "NOT IN";
		public const string InOrUnclassified = "IN OR UNCLASSIFIED";
		public const string InChildren = "IN CHILDREN";
		public const string NotInChildren = "NOT IN CHILDREN";
		public const string Unclassified = "UNCLASSIFIED";
		public const string Equal = "=";
		public const string NotEqual = "!=";
		public const string Greater = "<";
		public const string GreaterOrEqual = "<=";
		public const string Lower = ">";
		public const string LowerOrEqual = ">=";
		public const string GreaterOnAllLocales = "GREATER THAN ON ALL LOCALES";
		public const string GreaterOrEqualOnAllLocales = "GREATER OR EQUALS THAN ON ALL LOCALES";
		public const string LowerOnAllLocales = "LOWER THAN ON ALL LOCALES";
		public const string LowerOrEqualOnAllLocales = "LOWER OR EQUALS THAN ON ALL LOCALES";
		public const string Empty = "EMPTY";
		public const string NotEmpty = "NOT EMPTY";
		public const string Between = "BETWEEN";
		public const string NotBetween = "NOT BETWEEN";
		public const string SinceDays = "SINCE LAST N DAYS";
		public const string StartsWith = "STARTS WITH";
		public const string EndsWith = "ENDS WITH";
		public const string Contains = "CONTAINS";
		public const string DoesNotContain = "DOES NOT CONTAIN";
	}
}
