namespace Akeneo.Search
{
	public class Status : Criteria
	{
		public const string Key = "enabled";

		public static Status Enabled()
		{
			return new Status
			{
				Operator = Operators.Equal,
				Value = true
			};
		}

		public static Status Disabled()
		{
			return new Status
			{
				Operator = Operators.Equal,
				Value = false
			};
		}
	}
}
