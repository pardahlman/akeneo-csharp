using System.Collections.Generic;

namespace Akeneo.Model
{
	public class Family : ModelBase
	{
		/// <summary>
		/// Family code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Attribute code used as label
		/// </summary>
		public string AttributeAsLabel { get; set; }

		/// <summary>
		/// Attributes codes that compose the family.

		/// </summary>
		public List<string> Attributes { get; set; }

		/// <summary>
		/// Attributes codes of the family that are required for the completeness calculation for each channel
		/// Key: Channel Code
		/// Value: List of Attribute Codes
		/// </summary>
		public Dictionary<string, List<string>> AttributeRequirements { get; set; }

		/// <summary>
		/// Family labels for each locale 
		/// </summary>
		public Dictionary<string,string> Labels { get; set; }
	}
}
