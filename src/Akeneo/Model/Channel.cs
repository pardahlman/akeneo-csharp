using System.Collections.Generic;

namespace Akeneo.Model
{
	public class Channel : ModelBase
	{
		/// <summary>
		/// Channel code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Codes of activated locales for the channel
		/// </summary>
		public List<string> Locales { get; set; }

		/// <summary>
		/// Codes of activated currencies for the channel
		/// </summary>
		public List<string> Currencies { get; set; }

		/// <summary>
		/// Code of the category tree linked to the channel
		/// </summary>
		public string CategoryTree { get; set; }

		/// <summary>
		/// Channel label for the locales
		/// </summary>
		public Dictionary<string, string> Labels { get; set; }
	}
}
