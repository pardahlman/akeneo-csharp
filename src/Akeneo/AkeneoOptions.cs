using System;

namespace Akeneo
{
	public class AkeneoOptions
	{
		/// <summary>
		/// The Absolut URL to akeneo. No trailing slash.
		/// </summary>
		public Uri ApiEndpoint { get; set; }

		/// <summary>
		/// The OAuth Client Id (Generated in the PIM)
		/// </summary>
		public string ClientId { get; set; }

		/// <summary>
		/// The OAuth Client Secret (Generated in the PIM)
		/// </summary>
		public string ClientSecret { get; set; }

		/// <summary>
		/// User name
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// User password
		/// </summary>
		public string Password { get; set; }
	}
}
