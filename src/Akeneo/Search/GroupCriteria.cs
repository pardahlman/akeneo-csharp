using System.Linq;

namespace Akeneo.Search
{
	public class GroupCriteria : Criteria
	{
		public const string Key = "groups";

		public static GroupCriteria In(params string[] group)
		{
			return new GroupCriteria
			{
				Operator = Operators.In,
				Value = group.ToList()
			};
		}

		public static GroupCriteria NotIn(params string[] group)
		{
			return new GroupCriteria
			{
				Operator = Operators.NotIn,
				Value = group.ToList()
			};
		}

		public static GroupCriteria Empty()
		{
			return new GroupCriteria
			{
				Operator = Operators.Equal,
			};
		}

		public static GroupCriteria NotEmpty()
		{
			return new GroupCriteria
			{
				Operator = Operators.Equal,
			};
		}
	}
}
