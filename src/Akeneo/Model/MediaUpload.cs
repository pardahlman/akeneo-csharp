namespace Akeneo.Model
{
	public class MediaUpload
	{
		/// <summary>
		/// The product to which the media file will be associated.
		/// </summary>
		public MediaProduct Product { get; set; }

		/// <summary>
		/// Path to the file
		/// </summary>
		public string FilePath { get; set; }

		/// <summary>
		/// Name of the file
		/// </summary>
		public string FileName { get; set; }

		public MediaUpload() {
			Product = new MediaProduct();
		}
	}

	public class MediaProduct
	{
		/// <summary>
		/// Product Identifier
		/// </summary>
		public string Identifier { get; set; }

		/// <summary>
		/// Attribute Code
		/// </summary>
		public string Attribute { get; set; }

		/// <summary>
		/// Channel Code
		/// </summary>
		public string Scope { get; set; }

		/// <summary>
		/// Locale Code
		/// </summary>
		public string Locale { get; set; }
	}
}
