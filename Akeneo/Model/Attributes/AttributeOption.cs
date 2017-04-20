using System.Collections.Generic;

namespace Akeneo.Model.Attributes
{
	public class AttributeOption : ModelBase
	{
		/// <summary>
		/// Code of option
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Code of attribute related to the attribute optionCode of attribute related to the attribute option
		/// </summary>
		public string Attribute { get; set; }

		/// <summary>
		/// Order of attribute option
		/// </summary>
		public int SortOrder { get; set; }

		/// <summary>
		/// Attribute option labels for each locale
		/// </summary>
		public Dictionary<string,string> Labels { get; set; }
	}
}
