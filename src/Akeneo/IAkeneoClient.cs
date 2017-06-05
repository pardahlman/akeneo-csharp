using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Akeneo.Client;
using Akeneo.Model;
using Akeneo.Search;

namespace Akeneo
{
	public interface IAkeneoClient
	{
		/// <summary>
		/// Get resource by code.
		/// </summary>
		/// <typeparam name="TModel">Resource type</typeparam>
		/// <param name="code">Resource code</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>Akeneo response</returns>
		Task<TModel> GetAsync<TModel>(string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;

		/// <summary>
		/// Get resource by code and parent code. This can be used for nested resources, such as Attribute Options.
		/// </summary>
		/// <typeparam name="TModel">Resource type</typeparam>
		/// <param name="parentCode">Code of Parent Resource</param>
		/// <param name="code">Code of Resource</param>
		/// <param name="ct">Cancellation token</param>
		/// <returns>Akeneo response</returns>
		Task<TModel> GetAsync<TModel>(string parentCode, string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;

		/// <summary>
		/// Get multiple resources.
		/// </summary>
		/// <typeparam name="TModel">Resource type</typeparam>
		/// <param name="page">Page (default 1)</param>
		/// <param name="limit">Number of results per page (default 10, max 100)</param>
		/// <param name="withCount"> Return the count of products in the response. Be carefull with that, on a big catalog, it can decrease performance in a significative way</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>Akeneo response</returns>
		Task<PaginationResult<TModel>> GetManyAsync<TModel>(int page = 1, int limit = 10, bool withCount = false, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;

		/// <summary>
		/// Get resource by code and parent code. This can be used for nested resources, such as Attribute Options.
		/// </summary>
		/// <typeparam name="TModel">Resource Type</typeparam>
		/// <param name="parentCode">Code of Parent Resource</param>
		/// <param name="page">Page (deafult 1)</param>
		/// <param name="limit">Number of results per page (default 10, max 100)</param>
		/// <param name="withCount">Return the count of products in the response. Be carefull with that, on a big catalog, it can decrease performance in a significative way</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>A pagination result</returns>
		Task<PaginationResult<TModel>> GetManyAsync<TModel>(string parentCode, int page = 1, int limit = 10, bool withCount = false, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;

		/// <summary>
		/// Create Resource
		/// </summary>
		/// <typeparam name="TModel">Resource Type</typeparam>
		/// <param name="model">Resource</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>Akeneo response</returns>
		Task<AkeneoResponse> CreateAsync<TModel>(TModel model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;

		/// <summary>
		/// Update Resource
		/// </summary>
		/// <typeparam name="TModel">Resource Type</typeparam>
		/// <param name="model">Resource</param>
		/// <param name="ct">Cancellation Token</param>
		/// <returns>Akeneo response</returns>
		Task<AkeneoResponse> UpdateAsync<TModel>(TModel model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;
		Task<List<AkeneoBatchResponse>> CreateOrUpdateAsync<TModel>(IEnumerable<TModel> model, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;
		Task<AkeneoResponse> DeleteAsync<TModel>(string code, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;
		Task<AkeneoResponse> UploadAsync(MediaUpload media, CancellationToken ct = default(CancellationToken));
		Task<MediaDownload> DownloadAsync(string mediaCode, CancellationToken ct = default(CancellationToken));
		Task<PaginationResult<TModel>> SearchAsync<TModel>(IEnumerable<Criteria> criterias, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;
		Task<PaginationResult<TModel>> FilterAsync<TModel>(string queryString, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase;
	}
}