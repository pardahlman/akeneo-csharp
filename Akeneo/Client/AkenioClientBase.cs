using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Akeneo.Authentication;
using Akeneo.Http;

namespace Akeneo.Client
{
	public abstract class AkenioClientBase : IDisposable
	{
		protected readonly IAuthenticationClient AuthClient;
		protected readonly HttpClient HttpClient;
		private const string BearerAuthHeader = "Bearer";

		protected AkenioClientBase(Uri apiEndPoint, IAuthenticationClient authClient)
		{
			AuthClient = authClient;
			HttpClient = new HttpClient
			{
				BaseAddress = apiEndPoint,
				DefaultRequestHeaders = { Accept = { MediaTypeWithQualityHeaderValue.Parse("*/*") } }
			};
		}

		protected async Task<HttpResponseMessage> GetAsync(string url, CancellationToken ct = default(CancellationToken))
		{
			return await ExecuteAuthenticatedAsync((client, ctx) => client.GetAsync(ctx.RequestUrl, ctx.CancellationToken),
				new HttpCallContext
				{
					CancellationToken = ct,
					RequestUrl = url
				});
		}

		protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken ct = default(CancellationToken))
		{
			return await ExecuteAuthenticatedAsync((client, ctx) => client.DeleteAsync(ctx.RequestUrl, ctx.CancellationToken),
				new HttpCallContext
				{
					CancellationToken = ct,
					RequestUrl = url
				});
		}

		protected async Task<HttpResponseMessage> PatchAsJsonAsync<TContent>(string url, TContent content, CancellationToken ct = default(CancellationToken))
		{
			return await ExecuteAuthenticatedAsync((client, ctx) => client.PatchAsJsonAsync(ctx.RequestUrl, ctx.Content, ctx.CancellationToken),
				new HttpCallContext
				{
					CancellationToken = ct,
					Content = content,
					RequestUrl = url
				});
		}

		protected async Task<HttpResponseMessage> PatchAsJsonCollectionAsync<TContent>(string url, IEnumerable<TContent> collection, CancellationToken ct = default(CancellationToken))
		{
			return await ExecuteAuthenticatedAsync((client, ctx) => client.PatchAsJsonCollectionAsync(ctx.RequestUrl, ctx.Content as IEnumerable<TContent>, ctx.CancellationToken),
				new HttpCallContext
				{
					CancellationToken = ct,
					Content = collection,
					RequestUrl = url
				});
		}

		protected async Task<HttpResponseMessage> PostAsync<TContent>(string url, TContent content, CancellationToken ct = default(CancellationToken))
		{
			return await ExecuteAuthenticatedAsync((client, ctx) => client.PostJsonAsync(ctx.RequestUrl, ctx.Content, ctx.CancellationToken),
				new HttpCallContext
				{
					CancellationToken = ct,
					Content = content,
					RequestUrl = url
				});
		}

		private async Task<HttpResponseMessage> ExecuteAuthenticatedAsync(Func<HttpClient, HttpCallContext, Task<HttpResponseMessage>> func, HttpCallContext context)
		{
			var response = await func(HttpClient, context);
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				await AddAuthHeaderAsync(context.CancellationToken);
				response = await func(HttpClient, context);
			}
			return response;
		}

		protected async Task AddAuthHeaderAsync(CancellationToken ct = default(CancellationToken))
		{
			var tokenResponse = await AuthClient.GetAccessTokenAsync(ct);
			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BearerAuthHeader, tokenResponse.AccessToken);
		}

		public void Dispose()
		{
			HttpClient?.Dispose();
			(AuthClient as IDisposable)?.Dispose();
		}

		private class HttpCallContext
		{
			public string RequestUrl { get; set; }
			public object Content { get; set; }
			public CancellationToken CancellationToken { get; set; }
		}
	}
}
