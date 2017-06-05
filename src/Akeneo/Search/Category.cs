using System.Linq;

namespace Akeneo.Search
{
	public class Category : Criteria
	{
		public const string Key = "categories";

		private Category() { }

		public static Category In(params string[] categories)
		{
			return new Category
			{
				Operator = Operators.In,
				Value = categories.ToList()
			};
		}

		public static Category NotIn(params string[] categories)
		{
			return new Category
			{
				Operator = Operators.NotIn,
				Value = categories.ToList()
			};
		}

		public static Category InOrUnclassified(params string[] categories)
		{
			return new Category
			{
				Operator = Operators.InOrUnclassified,
				Value = categories.ToList()
			};
		}

		public static Category InChild(params string[] categories)
		{
			return new Category
			{
				Operator = Operators.InChildren,
				Value = categories.ToList()
			};
		}

		public static Category NotInChild(params string[] categories)
		{
			return new Category
			{
				Operator = Operators.NotInChildren,
				Value = categories.ToList()
			};
		}

		public static Category Unclassified()
		{
			return new Category
			{
				Operator = Operators.Unclassified
			};
		}
	}
}