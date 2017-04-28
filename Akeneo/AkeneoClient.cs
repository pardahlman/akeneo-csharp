using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Akeneo.Authentication;
using Akeneo.Client;
using Akeneo.Common;
using Akeneo.Http;
using Akeneo.Model;
using Akeneo.Model.Attributes;
using Akeneo.Serialization;

namespace Akeneo
{
	public class AkeneoClient : AkenioClientBase, IAkeneoClient
	{
		private readonly EndpointResolver _endpointResolver;

		public AkeneoClient(AkeneoOptions options)
			: this(options.ApiEndpoint, new AuthenticationClient(options.ApiEndpoint, options.ClientId, options.ClientSecret, options.UserName, options.Password)) { }

		public AkeneoClient(Uri apiEndPoint, IAuthenticationClient authClient) : base(apiEndPoint, authClient)
		{
			_endpointResolver = new EndpointResolver();
		}

		public async Task<TModel> GetAsync<TModel>(string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResource<TModel>(code);
			var response = await GetAsync(endpoint, ct);
			return response.IsSuccessStatusCode
				? await response.Content.ReadAsJsonAsync<TModel>()
				: default(TModel);
		}

		public async Task<TModel> GetAsync<TModel>(string parentCode, string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResource<TModel>(parentCode, code);
			var response = await GetAsync(endpoint, ct);
			return response.IsSuccessStatusCode
				? await response.Content.ReadAsJsonAsync<TModel>()
				: default(TModel);
		}

		public Task<PaginationResult<TModel>> GetManyAsync<TModel>(int page = 1, int limit = 10, bool withCount = false, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			return GetManyAsync<TModel>(null, page, limit, withCount, ct);
		}

		public async Task<PaginationResult<TModel>> GetManyAsync<TModel>(string parentCode, int page = 1, int limit = 10, bool withCount = false, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForPagination<TModel>(parentCode, page, limit, withCount);
			var response = await GetAsync(endpoint, ct);
			return response.IsSuccessStatusCode
				? await response.Content.ReadAsJsonAsync<PaginationResult<TModel>>()
				: PaginationResult<TModel>.Empty;
		}

		public async Task<AkeneoResponse> CreateAsync<TModel>(TModel model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var option = model as AttributeOption;
			var endpoint = _endpointResolver.ForResourceType<TModel>(option?.Attribute ?? string.Empty);
			var response = await PostAsync(endpoint, model, ct);
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode)
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}

		public async Task<AkeneoResponse> CreateAsync(MediaUpload media, CancellationToken ct = default(CancellationToken))
		{
			var filename = media.FileName ?? Path.GetFileName(media.FilePath);
			var formContent = new MultipartFormDataContent
			{
				{new JsonContent(media.Product) , "product" },
				{ new StreamContent(File.OpenRead(media.FilePath)), "file", filename }
			};
			await AddAuthHeaderAsync(ct);
			var response = await HttpClient.PostAsync(Endpoints.MediaFiles, formContent, ct);
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode)
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}

		public async Task<AkeneoResponse> UpdateAsync<TModel>(TModel model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResource(model);
			var response = await PatchAsJsonAsync(endpoint, model, ct);
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode)
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}

		public async Task<List<AkeneoBatchResponse>> CreateOrUpdateAsync<TModel>(IEnumerable<TModel> model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var option = model.FirstOrDefault() as AttributeOption;
			var endpoint = _endpointResolver.ForResourceType<TModel>(option?.Attribute);
			var response = await PatchAsJsonCollectionAsync(endpoint, model, ct);
			var contentStr = await response.Content.ReadAsStringAsync();
			return AkeneoCollectionSerializer.Deserialize<AkeneoBatchResponse>(contentStr).ToList();
		}

		public async Task<AkeneoResponse> DeleteAsync<TModel>(string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResource<TModel>(code);
			var response = await DeleteAsync(endpoint, ct);
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode)
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}
	}
}
