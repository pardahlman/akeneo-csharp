namespace Akeneo.Model.Attributes
{
	public class PriceAttribute : TypedAttributeBase
	{
		public override string Type => AttributeType.Price;

		/// <summary>
		/// Minimum integer value allowed when the attribute type is `pim_catalog_metric`, `pim_catalog_price` or `pim_catalog_number`
		/// </summary>
		public float? NumberMin { get; set; }

		/// <summary>
		/// Minimum integer value allowed when the attribute type is `pim_catalog_metric`, `pim_catalog_price` or `pim_catalog_number`
		/// </summary>
		public float? NumberMax { get; set; }

		/// <summary>
		/// Whether decimals are allowed when the attribute type is `pim_catalog_metric`, `pim_catalog_price` or `pim_catalog_number`
		/// </summary>
		public bool DecimalsAllowed { get; set; }
	}
}