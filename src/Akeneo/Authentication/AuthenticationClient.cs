using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Akeneo.Client;
using Akeneo.Common;
using Akeneo.Http;

namespace Akeneo.Authentication
{
	public interface IAuthenticationClient
	{
		Task<AccessTokenResponse> RequestAccessTokenAsync(CancellationToken ct = default(CancellationToken));
		Task<AccessTokenResponse> RequestRefreshTokenAsync(string refreshToken, CancellationToken ct = default(CancellationToken));
		Task<AccessTokenResponse> GetAccessTokenAsync(CancellationToken ct = default(CancellationToken));
	}

	public class AuthenticationClient : IAuthenticationClient, IDisposable
	{
		private readonly string _username;
		private readonly string _password;
		private readonly HttpClient _httpClient;
		private AccessTokenResponse _latestAccessToken;
		private const string BasicAuthHeader = "Basic";

		public AuthenticationClient(Uri apiEndPoint, string clientId, string clientSecret, string username, string password)
		{
			_username = username;
			_password = password;
			_httpClient = new HttpClient
			{
				BaseAddress = apiEndPoint,
				DefaultRequestHeaders =
				{
					Authorization = new AuthenticationHeaderValue(BasicAuthHeader, CreateAuthHeader(clientId, clientSecret))
				}
			};
		}

		public virtual async Task<AccessTokenResponse> GetAccessTokenAsync(CancellationToken ct = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(_latestAccessToken?.RefreshToken))
			{
				_latestAccessToken = await RequestAccessTokenAsync(ct);
			}
			else
			{
				_latestAccessToken = await RequestRefreshTokenAsync(_latestAccessToken.RefreshToken, ct);
			}
			return _latestAccessToken;
		}

		public virtual async Task<AccessTokenResponse> RequestAccessTokenAsync(CancellationToken ct = default(CancellationToken))
		{
			var response = await _httpClient.PostAsync(
				Endpoints.OAuthToken,
				new JsonContent(AccessTokenRequest.For(_username, _password)),
				ct
			);

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsJsonAsync<AccessTokenResponse>();
			}
			var payload = await response.Content.ReadAsJsonAsync<AkeneoResponse>();
			throw new Exception($"Akeneo PIM replied with a non-successful status code {payload.Code}\n{payload.Message}");
		}

		public virtual async Task<AccessTokenResponse> RequestRefreshTokenAsync(string refreshToken, CancellationToken ct = default(CancellationToken))
		{
			if (string.IsNullOrEmpty(refreshToken))
			{
				throw new Exception("No refresh token found. Try to create an access token.");
			}

			var response = await _httpClient.PostAsync(
				Endpoints.OAuthToken,
				new JsonContent(RefreshTokenRequest.For(refreshToken)),
				ct
			);

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadAsJsonAsync<AccessTokenResponse>();
			}
			throw new Exception("Failed to get Auth token");
		}

		private static string CreateAuthHeader(string clientId, string clientSecret)
		{
			return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
		}

		public void Dispose()
		{
			_httpClient?.Dispose();
		}
	}
}
