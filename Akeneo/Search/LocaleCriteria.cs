namespace Akeneo.Search
{
	public class LocaleCriteria : Criteria
	{
		public static LocaleCriteria For(string locale)
		{
			return new LocaleCriteria
			{
				Operator = Operators.Equal,
				Value = locale
			};
		}
	}
}
