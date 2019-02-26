using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Akeneo.Authentication;
using Akeneo.Client;
using Akeneo.Common;
using Akeneo.Consts;
using Akeneo.Exceptions;
using Akeneo.Http;
using Akeneo.Logging;
using Akeneo.Model;
using Akeneo.Model.Attributes;
using Akeneo.Search;
using Akeneo.Serialization;

namespace Akeneo
{
	public class AkeneoClient : AkenioClientBase, IAkeneoClient
	{
		private readonly EndpointResolver _endpointResolver;
		private readonly SearchQueryBuilder _searchBuilder;
		private readonly ILog _logger = LogProvider.For<AkeneoClient>();

        public AkeneoClient(AkeneoOptions options)
			: this(options.ApiEndpoint, new AuthenticationClient(options.ApiEndpoint, options.ClientId, options.ClientSecret, options.UserName, options.Password)) { }

		public AkeneoClient(Uri apiEndPoint, IAuthenticationClient authClient) : base(apiEndPoint, authClient)
		{
			_logger.Info($"Instantiating Akeneo .NET client for endpoint '{apiEndPoint}'.");
			_endpointResolver = new EndpointResolver();
			_searchBuilder = new SearchQueryBuilder();
		}

		public async Task<TModel> GetAsync<TModel>(string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResource<TModel>(code);
			_logger.Debug($"Getting resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await GetAsync(endpoint, ct);
			return response.IsSuccessStatusCode
				? await response.Content.ReadAsJsonAsync<TModel>()
				: default(TModel);
		}

	    public async Task<string> GetByUrl(string url, CancellationToken ct = default(CancellationToken))
	    {
	        _logger.Debug($"Getting resource from URL '{url}'.");
	        HttpResponseMessage response = await GetAsync(url, ct);
	        return await response.Content.ReadAsStringAsync();
	    }

        public async Task<TModel> GetAsync<TModel>(string parentCode, string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResource<TModel>(parentCode, code);
			_logger.Debug($"Getting resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await GetAsync(endpoint, ct);
			return response.IsSuccessStatusCode
				? await response.Content.ReadAsJsonAsync<TModel>()
				: default(TModel);
		}

		public Task<PaginationResult<TModel>> SearchAsync<TModel>(IEnumerable<Criteria> criterias, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var queryString = _searchBuilder.GetQueryString(criterias);
			return FilterAsync<TModel>(queryString, ct);
		}

	    public Task<List<TModel>> SearchAndGetAllAsync<TModel>(IEnumerable<Criteria> criterias, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
	    {
	        var queryString = _searchBuilder.GetQueryStringParam(criterias);
	        return FilterAndGetAllAsync<TModel>(queryString, null, ct);
	    }

        public async Task<PaginationResult<TModel>> FilterAsync<TModel>(string queryString, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResourceType<TModel>();
			_logger.Debug($"Filtering resource '{typeof(TModel).Name}' from URL '{endpoint}' with query '{queryString}'.");
			var response = await GetAsync($"{endpoint}{queryString}", ct);
			var result = await response.Content.ReadAsJsonAsync<PaginationResult<TModel>>();
			result.Code = response.StatusCode;
			return result;
		}
	    public async Task<List<TModel>> FilterAndGetAllAsync<TModel>(string queryString, string cursor = null, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
	    {
	        var endpoint = _endpointResolver.WithSearchAfter<TModel>(100, cursor);
            _logger.Debug($"Filtering resource '{typeof(TModel).Name}' from URL '{endpoint}' with query '{queryString}'.");
	        var response = await GetAsync($"{endpoint}{queryString}", ct);

	        if (!response.IsSuccessStatusCode) throw new HttpRequestException($"Error while executing GET reques: {endpoint}{queryString}");

	        PaginationResult<TModel> paginationResult = await response.Content.ReadAsJsonAsync<PaginationResult<TModel>>();
	        paginationResult.Code = response.StatusCode;

	        List<TModel> items = paginationResult.GetItems();

	        var nextCursor = paginationResult.Links.GetCursor();

	        if (nextCursor != null)
	        {
	            var nextItems = await FilterAndGetAllAsync<TModel>(queryString, nextCursor, ct);
	            items.AddRange(nextItems);
	        }

	        return items;
	    }

        public Task<PaginationResult<TModel>> GetManyAsync<TModel>(int page = 1, int limit = 10, bool withCount = false, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			return GetManyAsync<TModel>(null, page, limit, withCount, ct);
		}

        public async Task<PaginationResult<TModel>> GetManyAsync<TModel>(string parentCode, int page = 1, int limit = 10, bool withCount = false, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForPagination<TModel>(parentCode, page, limit, withCount);
			_logger.Debug($"Getting multiple resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await GetAsync(endpoint, ct);
			return response.IsSuccessStatusCode
				? await response.Content.ReadAsJsonAsync<PaginationResult<TModel>>()
				: PaginationResult<TModel>.Empty;
		}

        public async Task<List<TModel>> GetAllAsync<TModel>(string cursor = null, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.WithSearchAfter<TModel>(100, cursor);
			_logger.Debug($"Getting multiple resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await GetAsync(endpoint, ct);

		    if (!response.IsSuccessStatusCode) return new List<TModel>();

		    PaginationResult<TModel> paginationResult = await response.Content.ReadAsJsonAsync<PaginationResult<TModel>>();
		    var items = paginationResult.GetItems();
                
		    var nextCursor = paginationResult.Links.GetCursor();

		    if (nextCursor != null)
		    {
		        var nextItems = await GetAllAsync<TModel>(nextCursor, ct);
		        items.AddRange(nextItems);
		    }

		    return items;
		}

		public async Task<AkeneoResponse> CreateAsync<TModel>(TModel model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var option = model as AttributeOption;
			var endpoint = _endpointResolver.ForResourceType<TModel>(option?.Attribute ?? string.Empty);
			_logger.Debug($"Creating resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await PostAsync(endpoint, model, AkeneoSerializerSettings.Create, ct);
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode, new KeyValuePair<string, PaginationLink>(PaginationLinks.Location, new PaginationLink { Href = response.Headers?.Location?.ToString() }))
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}

		public async Task<AkeneoResponse> UpdateAsync<TModel>(TModel model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResource(model);
			_logger.Debug($"Updating resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await PatchAsJsonAsync(endpoint, model, AkeneoSerializerSettings.Update, ct);
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode, new KeyValuePair<string, PaginationLink>(PaginationLinks.Location, new PaginationLink { Href = response.Headers?.Location?.ToString() }))
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}

		public async Task<AkeneoResponse> UpdateAsync<TModel>(string identifier, object model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = $"{_endpointResolver.ForResourceType<TModel>()}/{identifier}";
			_logger.Debug($"Updating resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await PatchAsJsonAsync(endpoint, model, AkeneoSerializerSettings.Update, ct);
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode, new KeyValuePair<string, PaginationLink>(PaginationLinks.Location, new PaginationLink { Href = response.Headers?.Location?.ToString() }))
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}

		public async Task<List<AkeneoBatchResponse>> CreateOrUpdateAsync<TModel>(IEnumerable<TModel> model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var option = model.FirstOrDefault() as AttributeOption;
			var endpoint = _endpointResolver.ForResourceType<TModel>(option?.Attribute);
			_logger.Debug($"Updating or creating resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await PatchAsJsonCollectionAsync(endpoint, model, ct);
			var contentStr = await response.Content.ReadAsStringAsync();
			return AkeneoCollectionSerializer.Deserialize<AkeneoBatchResponse>(contentStr).ToList();
		}

		public async Task<AkeneoResponse> DeleteAsync<TModel>(string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var endpoint = _endpointResolver.ForResource<TModel>(code);
			_logger.Debug($"Deleting resource '{typeof(TModel).Name}' from URL '{endpoint}'.");
			var response = await DeleteAsync(endpoint, ct);
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode)
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}

		public async Task<AkeneoResponse> UploadAsync(MediaUpload media, CancellationToken ct = default(CancellationToken))
		{
			if (!File.Exists(media.FilePath))
			{
				throw new FileNotFoundException($"File with path {media.FilePath} not found.");
			}
			var filename = media.FileName ?? Path.GetFileName(media.FilePath);
			var formContent = new MultipartFormDataContent
			{
				{new JsonContent(media.Product, AkeneoSerializerSettings.Update) , "product" },
				{ new StreamContent(File.OpenRead(media.FilePath)), "file", filename }
			};

			_logger.Debug($"Preparing to upload image '{filename}' from '{media.FilePath}'.");
			var response = await HttpClient.PostAsync(Endpoints.MediaFiles, formContent, ct);
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				await AddAuthHeaderAsync(ct);
				response = await HttpClient.PostAsync(Endpoints.MediaFiles, new MultipartFormDataContent
				{
					{new JsonContent(media.Product, AkeneoSerializerSettings.Update) , "product" },
					{ new StreamContent(File.OpenRead(media.FilePath)), "file", filename }
				}, ct);
			}
			return response.IsSuccessStatusCode
				? AkeneoResponse.Success(response.StatusCode, new KeyValuePair<string, PaginationLink>(PaginationLinks.Location, new PaginationLink{ Href = response.Headers?.Location?.ToString()}))
				: await response.Content.ReadAsJsonAsync<AkeneoResponse>();
		}

		public async Task<MediaDownload> DownloadAsync(string mediaCode, CancellationToken ct = default(CancellationToken))
		{
			var response = await GetAsync($"{Endpoints.MediaFiles}/{mediaCode}/download", ct);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				var body = await response.Content.ReadAsJsonAsync<AkeneoResponse>();
				_logger.Warn($"Unable to load Media File '{mediaCode}'. Http Status Code: {body.Code}. Message: {body.Message}");
				throw new OperationUnsuccessfulException($"Unable to load Media File '{mediaCode}'.", body);
			}
			return new MediaDownload
			{
				FileName = response.Content.Headers.ContentDisposition.FileName,
				Stream = await response.Content.ReadAsStreamAsync()
			};
		}
	}
}
