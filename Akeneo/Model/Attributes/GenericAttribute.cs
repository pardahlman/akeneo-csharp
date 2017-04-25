using System;
using System.Collections.Generic;

namespace Akeneo.Model.Attributes
{
	public class GenericAttribute : AttributeBase
	{
		public string Type { get; set; }
		/// <summary>
		/// Number maximum of characters allowed for the value of the attribute when the attribute type is `pim_catalog_text`, `pim_catalog_textarea` or `pim_catalog_identifier`
		/// </summary>
		public int MaxCharacters { get; set; }

		/// <summary>
		/// Validation rule type used to validate any attribute value when the attribute type is `pim_catalog_text` or `pim_catalog_identifier`
		/// </summary>
		public string ValidationRule { get; set; }

		/// <summary>
		/// Regexp expression used to validate any attribute value when the attribute type is `pim_catalog_text` or `pim_catalog_identifier`
		/// </summary>
		public string ValidationRegExp { get; set; }

		/// <summary>
		/// Whether the WYSIWYG interface is shown when the attribute type is `pim_catalog_textarea`
		/// </summary>
		public bool WysiwygEnabled { get; set; }

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

		/// <summary>
		/// Minimum date allowed when the attribute type is `pim_catalog_date`
		/// </summary>
		public DateTime DateMin { get; set; }

		/// <summary>
		/// Maximum date allowed when the attribute type is `pim_catalog_date`
		/// </summary>
		public DateTime DateMax { get; set; }

		/// <summary>
		///  Extensions allowed when the attribute type is `pim_catalog_file` or `pim_catalog_image`
		/// </summary>
		public List<string> AlloweddExtensions { get; set; }

		/// <summary>
		/// Max file size when the attribute type is `pim_catalog_file` or `pim_catalog_image`
		/// </summary>
		public string MaxFileSize { get; set; }
	}
}