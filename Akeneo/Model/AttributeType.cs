namespace Akeneo.Model
{
	public class AttributeType
	{
		/// <summary>
		/// It is the unique product’s identifier. The catalog can contain only one.
		/// </summary>
		public const string Identifier = "pim_catalog_identifier";

		/// <summary>
		/// Text
		/// </summary>
		public const string Text = "pim_catalog_text";

		/// <summary>
		/// Long texts
		/// </summary>
		public const string TextArea = "pim_catalog_textarea";

		/// <summary>
		/// Yes/No
		/// </summary>
		public const string Boolean = "pim_catalog_boolean";

		/// <summary>
		/// Number (integer and float)
		/// </summary>
		public const string Number = "pim_catalog_number";

		/// <summary>
		///  	Simple choice list
		/// </summary>
		public const string SimpleSelect = "pim_catalog_simpleselect";

		/// <summary>
		/// Date
		/// </summary>
		public const string Date = "pim_catalog_date";

		/// <summary>
		/// Metric. It consists of a value and a unit (a weight for example).
		/// </summary>
		public const string Metric = "pim_catalog_metric";

		/// <summary>
		/// Collection of prices. Each price contains a value and a currency.
		/// </summary>
		public const string Price = "pim_catalog_price_collection";
	}
}