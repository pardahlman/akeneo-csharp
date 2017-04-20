using System.Collections.Generic;

namespace Akeneo.Model.Attributes
{
	public abstract class AttributeBase : ModelBase
	{
		/// <summary>
		/// Attribute code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Attribute labels for each locale. Key: LocaleCode, Value: Value for Locale
		/// </summary>
		public Dictionary<string, string> Labels { get; set; }

		/// <summary>
		/// Attribute group
		/// </summary>
		public string Group { get; set; }

		/// <summary>
		/// Order of the attribute in its group
		/// </summary>
		public int SortOrder { get; set; }

		/// <summary>
		/// Whether the attribute is localizable, i.e. can have one value by local
		/// </summary>
		public bool Localizable { get; set; }

		/// <summary>
		/// Whether the attribute is scopable, i.e. can have one value by channel
		/// </summary>
		public bool Scopable { get; set; }

		/// <summary>
		/// To make the attribute locale specfic, specify here for which locales it is specific
		/// </summary>
		public List<string> AvailableLocales { get; set; }

		/// <summary>
		/// Whether two values for the attribute cannot be the same
		/// </summary>
		public bool Unique { get; set; }

		/// <summary>
		/// Whether the attribute can be used as a filter for the product grid in the PIM user interface
		/// </summary>
		public bool UseableAsGridFilter { get; set; }
	}

	public abstract class TypedAttributeBase : AttributeBase
	{
		/// <summary>
		/// Attribute type
		/// </summary>
		public abstract string Type { get; }
	}
}
