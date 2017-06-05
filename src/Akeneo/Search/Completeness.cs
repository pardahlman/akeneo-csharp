namespace Akeneo.Search
{
	public class Completeness : Criteria
	{
		public const string Key = "completeness";

		public string Scope { get; set; }

		public static Completeness Greater(int completness, string channel, bool allLocales = false)
		{
			return new Completeness
			{
				Operator = allLocales
					? Operators.GreaterOnAllLocales
					: Operators.Greater,
				Value = completness,
				Scope = channel
			};
		}

		public static Completeness GreaterOrEqual(int completness, string channel, bool allLocales = false)
		{
			return new Completeness
			{
				Operator = allLocales
					? Operators.GreaterOrEqualOnAllLocales
					: Operators.GreaterOrEqual,
				Value = completness,
				Scope = channel
			};
		}

		public static Completeness Lower(int completness, string channel, bool allLocales = false)
		{
			return new Completeness
			{
				Operator = allLocales
					? Operators.LowerOnAllLocales
					: Operators.Lower,
				Value = completness,
				Scope = channel
			};
		}

		public static Completeness LowerOrEqual(int completness, string channel, bool allLocales = false)
		{
			return new Completeness
			{
				Operator = allLocales
					? Operators.LowerOrEqualOnAllLocales
					: Operators.LowerOrEqual,
				Value = completness,
				Scope = channel
			};
		}

		public static Completeness Equal(int completness, string channel)
		{
			return new Completeness
			{
				Operator = Operators.Equal,
				Value = completness,
				Scope = channel
			};
		}

		public static Completeness NotEqual(int completness, string channel)
		{
			return new Completeness
			{
				Operator = Operators.NotEqual,
				Value = completness,
				Scope = channel
			};
		}
	}
}
