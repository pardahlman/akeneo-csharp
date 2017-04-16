using System.Collections.Generic;
using System.Net;

namespace Akeneo.Client
{
	public class AkeneoResponse
	{
		public HttpStatusCode Code { get; set; }
		public string Message { get; set; }
		public Dictionary<string, PaginationLink> Links { get; set; }
		public List<ValidationError> Errors { get; set; }

		public static AkeneoResponse Success(HttpStatusCode code)
		{
			return new AkeneoResponse
			{
				Code = code
			};
		}
	}

	public class ValidationError
	{
		public string Property { get; set; }
		public string Message { get; set; }
		public string Attribute { get; set; }
		public string Locale { get; set; }
		public string Scope { get; set; }
		public string Currency { get; set; }
	}
}
