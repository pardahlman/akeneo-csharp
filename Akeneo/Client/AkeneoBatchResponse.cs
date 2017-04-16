using System.Net;

namespace Akeneo.Client
{
	public class AkeneoBatchResponse
	{
		/// <summary>
		/// Line number
		/// </summary>
		public int Line { get; set; }

		/// <summary>
		/// Resource identifier, only filled when the resource is a product
		/// </summary>
		public string Identifier { get; set; }

		/// <summary>
		/// Resource code, only filled when the resource is a category, a family or an attribute
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// HTTP status code
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// Message explaining the error
		/// </summary>
		public string Message { get; set; }
	}
}
