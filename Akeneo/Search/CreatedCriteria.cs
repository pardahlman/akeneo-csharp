using System;
using System.Collections.Generic;

namespace Akeneo.Search
{
	public class CreatedCriteria : Criteria
	{
		public const string Key = "created";

		public static CreatedCriteria Equals(DateTime date)
		{
			return new CreatedCriteria
			{
				Operator = Operators.Equal,
				Value = date
			};
		}

		public static CreatedCriteria NotEquals(DateTime date)
		{
			return new CreatedCriteria
			{
				Operator = Operators.NotEqual,
				Value = date
			};
		}

		public static CreatedCriteria Greater(DateTime date)
		{
			return new CreatedCriteria
			{
				Operator = Operators.Greater,
				Value = date
			};
		}

		public static CreatedCriteria Lower(DateTime date)
		{
			return new CreatedCriteria
			{
				Operator = Operators.Lower,
				Value = date
			};
		}

		public static CreatedCriteria Between(DateTime start, DateTime stop)
		{
			return new CreatedCriteria
			{
				Operator = Operators.Between,
				Value = new List<DateTime>{ start, stop}
			};
		}

		public static CreatedCriteria NotBetween(DateTime start, DateTime stop)
		{
			return new CreatedCriteria
			{
				Operator = Operators.NotBetween,
				Value = new List<DateTime> { start, stop }
			};
		}

		public static CreatedCriteria SinceDays(int dayCount)
		{
			return new CreatedCriteria
			{
				Operator = Operators.SinceDays,
				Value = dayCount
			};
		}
	}
}
