namespace Akeneo.Model.Attributes
{
	public class TextAreaAttribute : TypedAttributeBase
	{
		public override string Type => AttributeType.TextArea;

		/// <summary>
		/// Number maximum of characters allowed for the value of the attribute when the attribute type is `pim_catalog_text`, `pim_catalog_textarea` or `pim_catalog_identifier`
		/// </summary>
		public int? MaxCharacters { get; set; }

		/// <summary>
		/// Whether the WYSIWYG interface is shown when the attribute type is `pim_catalog_textarea`
		/// </summary>
		public bool WysiwygEnabled { get; set; }
	}
}