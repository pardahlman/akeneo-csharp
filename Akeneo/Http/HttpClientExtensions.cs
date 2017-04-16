using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Akeneo.Http
{
	public static class HttpClientExtensions
	{
		public static Task<HttpResponseMessage> PostAsJsonAsync<TContent>(this HttpClient client, string uri, TContent content, CancellationToken ct = default (CancellationToken))
		{
			return client.PostAsync(uri, new JsonContent(content), ct);
		}

		public static Task<HttpResponseMessage> PatchAsJsonAsync<TContent>(this HttpClient client, string requestUri, TContent content, CancellationToken ct)
		{
			return client.PatchAsync(requestUri, new JsonContent(content), ct);
		}

		public static Task<HttpResponseMessage> PatchAsJsonCollectionAsync<TContent>(this HttpClient client, string requestUri, IEnumerable<TContent> content, CancellationToken ct)
		{
			return client.PatchAsync(requestUri, new JsonCollectionContent<TContent>(content), ct);
		}

		public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content, CancellationToken ct)
		{
			var request = new HttpRequestMessage(
				new HttpMethod("PATCH"),
				new Uri(requestUri, UriKind.Relative))
			{
				Content = content
			};

			return client.SendAsync(request, ct);
		}
	}
}
