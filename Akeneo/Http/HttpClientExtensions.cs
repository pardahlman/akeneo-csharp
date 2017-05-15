using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Akeneo.Serialization;
using Newtonsoft.Json;

namespace Akeneo.Http
{
	public static class HttpClientExtensions
	{
		public static Task<HttpResponseMessage> PostJsonAsync<TContent>(this HttpClient client, string uri, TContent content, JsonSerializerSettings setting, CancellationToken ct = default (CancellationToken))
		{
			return client.PostAsync(uri, new JsonContent(content, setting), ct);
		}

		public static Task<HttpResponseMessage> PostJsonAsync<TContent>(this HttpClient client, string uri, TContent content,  CancellationToken ct = default(CancellationToken))
		{
			return client.PostJsonAsync(uri, content, AkeneoSerializerSettings.Create, ct);
		}

		public static Task<HttpResponseMessage> PatchAsJsonAsync<TContent>(this HttpClient client, string requestUri, TContent content, CancellationToken ct)
		{
			return client.PatchAsJsonAsync(requestUri, content, AkeneoSerializerSettings.Update, ct);
		}

		public static Task<HttpResponseMessage> PatchAsJsonAsync<TContent>(this HttpClient client, string requestUri, TContent content, JsonSerializerSettings setting, CancellationToken ct)
		{
			return client.PatchAsync(requestUri, new JsonContent(content, setting), ct);
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
