using System.Linq;

namespace Akeneo.Search
{
	public class Family : Criteria
	{
		public const string Key = "family";

		public static Family In(params string[] group)
		{
			return new Family
			{
				Operator = Operators.In,
				Value = group.ToList()
			};
		}

		public static Family NotIn(params string[] group)
		{
			return new Family
			{
				Operator = Operators.NotIn,
				Value = group.ToList()
			};
		}

		public static Family Empty()
		{
			return new Family
			{
				Operator = Operators.Equal,
			};
		}

		public static Family NotEmpty()
		{
			return new Family
			{
				Operator = Operators.Equal,
			};
		}
	}
}
