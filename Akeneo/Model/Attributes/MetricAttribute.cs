namespace Akeneo.Model.Attributes
{
	public class MetricAttribute : TypedAttributeBase
	{
		public override string Type => AttributeType.Metric;

		/// <summary>
		/// Minimum integer value allowed when the attribute type is `pim_catalog_metric`, `pim_catalog_price` or `pim_catalog_number`
		/// </summary>
		public int NumberMin { get; set; }

		/// <summary>
		/// Minimum integer value allowed when the attribute type is `pim_catalog_metric`, `pim_catalog_price` or `pim_catalog_number`
		/// </summary>
		public int NumberMax { get; set; }

		/// <summary>
		/// Whether decimals are allowed when the attribute type is `pim_catalog_metric`, `pim_catalog_price` or `pim_catalog_number`
		/// </summary>
		public bool DecimalsAllowed { get; set; }

		/// <summary>
		/// Whether negative values are allowed when the attribute type is `pim_catalog_metric` or `pim_catalog_number`
		/// </summary>
		public bool NegativeAllowed { get; set; }

		/// <summary>
		/// Metric family when the attribute type is `pim_catalog_metric`
		/// </summary>
		public string MetricFamily { get; set; }

		/// <summary>
		/// Default metric unit when the attribute type is `pim_catalog_metric`
		/// </summary>
		public string DefaultMetricUnit { get; set; }
	}
}