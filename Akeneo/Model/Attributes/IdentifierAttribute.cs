namespace Akeneo.Model.Attributes
{
	public class IdentifierAttribute : TypedAttributeBase
	{
		public override string Type => AttributeType.Identifier;

		/// <summary>
		/// Number maximum of characters allowed for the value of the attribute when the attribute type is `pim_catalog_text`, `pim_catalog_textarea` or `pim_catalog_identifier`
		/// </summary>
		public int? MaxCharacters { get; set; }

		/// <summary>
		/// Validation rule type used to validate any attribute value when the attribute type is `pim_catalog_text` or `pim_catalog_identifier`
		/// </summary>
		public string ValidationRule { get; set; }

		/// <summary>
		/// Regexp expression used to validate any attribute value when the attribute type is `pim_catalog_text` or `pim_catalog_identifier`
		/// </summary>
		public string ValidationRegExp { get; set; }
	}
}