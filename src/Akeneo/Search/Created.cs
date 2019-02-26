using System;
using System.Collections.Generic;

namespace Akeneo.Search
{
	public class Created : Criteria
	{
		public const string Key = "created";

		public static Created Equals(DateTime date)
		{
			return new Created
			{
				Operator = Operators.Equal,
				Value = date.ToString("yyyy-MM-dd HH:mm:ss")
            };
		}

		public static Created NotEquals(DateTime date)
		{
			return new Created
			{
				Operator = Operators.NotEqual,
				Value = date.ToString("yyyy-MM-dd HH:mm:ss")
            };
		}

		public static Created Greater(DateTime date)
		{
            return new Created
			{
				Operator = Operators.Greater,
				Value = date.ToString("yyyy-MM-dd HH:mm:ss")
			};
		}

		public static Created Lower(DateTime date)
		{
			return new Created
			{
				Operator = Operators.Lower,
				Value = date.ToString("yyyy-MM-dd HH:mm:ss")
            };
		}

		public static Created Between(DateTime start, DateTime stop)
		{
			return new Created
			{
				Operator = Operators.Between,
				Value = new List<DateTime>{ start, stop}
			};
		}

		public static Created NotBetween(DateTime start, DateTime stop)
		{
			return new Created
			{
				Operator = Operators.NotBetween,
				Value = new List<DateTime> { start, stop }
			};
		}

		public static Created SinceDays(int dayCount)
		{
			return new Created
			{
				Operator = Operators.SinceDays,
				Value = dayCount
			};
		}
	}
}
