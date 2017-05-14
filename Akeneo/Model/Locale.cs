using System;
using System.Collections.Generic;
using System.Text;

namespace Akeneo.Model
{
	public class Locale : ModelBase
	{
		/// <summary>
		/// Locale code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Whether the locale is enabled
		/// </summary>
		public bool Enabled { get; set; }
	}
}
