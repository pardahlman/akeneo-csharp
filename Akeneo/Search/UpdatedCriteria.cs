using System;
using System.Collections.Generic;

namespace Akeneo.Search
{
	public class UpdatedCriteria : Criteria
	{
		public const string Key = "updated";

		public static UpdatedCriteria Equals(DateTime date)
		{
			return new UpdatedCriteria
			{
				Operator = Operators.Equal,
				Value = date
			};
		}

		public static UpdatedCriteria NotEquals(DateTime date)
		{
			return new UpdatedCriteria
			{
				Operator = Operators.NotEqual,
				Value = date
			};
		}

		public static UpdatedCriteria Greater(DateTime date)
		{
			return new UpdatedCriteria
			{
				Operator = Operators.Greater,
				Value = date
			};
		}

		public static UpdatedCriteria Lower(DateTime date)
		{
			return new UpdatedCriteria
			{
				Operator = Operators.Lower,
				Value = date
			};
		}

		public static UpdatedCriteria Between(DateTime start, DateTime stop)
		{
			return new UpdatedCriteria
			{
				Operator = Operators.Between,
				Value = new List<DateTime> { start, stop }
			};
		}

		public static UpdatedCriteria NotBetween(DateTime start, DateTime stop)
		{
			return new UpdatedCriteria
			{
				Operator = Operators.NotBetween,
				Value = new List<DateTime> { start, stop }
			};
		}

		public static UpdatedCriteria SinceDays(int dayCount)
		{
			return new UpdatedCriteria
			{
				Operator = Operators.SinceDays,
				Value = dayCount
			};
		}
	}
}
