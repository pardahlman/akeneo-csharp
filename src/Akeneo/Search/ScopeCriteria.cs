namespace Akeneo.Search
{
	public class ScopeCriteria : Criteria
	{

		public static ScopeCriteria For(string channel)
		{
			return new ScopeCriteria
			{
				Operator = Operators.Equal,
				Value = channel
			};
		}
	}
}
