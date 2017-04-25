using System;
using System.Collections.Generic;
using System.Text;

namespace Akeneo.Model
{
	internal class MediaFile : ModelBase
	{
		/// <summary>
		/// The product to which the media file will be associated.
		/// </summary>
		public MediaProduct Product { get; set; }

		/// <summary>
		/// The binaries of the file
		/// </summary>
		public string File { get; set; }

		public MediaFile()
		{
			Product = new MediaProduct();
		}
	}

	internal class MediaProduct
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
