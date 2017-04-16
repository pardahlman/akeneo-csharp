using System.Collections.Generic;

namespace Akeneo.Model
{
	public class Category : ModelBase
	{
		/// <summary>
		/// Category code (required)
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Category code of the parent's category
		/// </summary>
		public string Parent { get; set; }

		/// <summary>
		/// Category labels for each locale
		/// </summary>
		public Dictionary<string, string> Labels { get; set; }

		public Category()
		{
			Labels = new Dictionary<string, string>();
		}
	}

	public class Label
	{
		public string Locale { get; set; }
		public string Value { get; set; }
	}
}
