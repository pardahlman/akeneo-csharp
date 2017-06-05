using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akeneo.Client;
using Akeneo.Consts;
using Akeneo.Model;

namespace Akeneo.Extensions
{
	public static class AkeneoClientExtensions
	{
		public static Task<List<TModel>> GetAllAsync<TModel>(this IAkeneoClient client, int limit = 10, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			return client.GetAllAsync<TModel>(null, limit, ct);
		}

		public static async Task<List<TModel>> GetAllAsync<TModel>(this IAkeneoClient client, string parentCode, int limit = 10, CancellationToken ct = default(CancellationToken)) where TModel : ModelBase
		{
			var result = new List<TModel>();
			var page = 1;
			bool hasMore = false;
			do
			{
				var pagination = await client.GetManyAsync<TModel>(parentCode, page, limit, ct: ct);
				result.AddRange(pagination.GetItems());
				hasMore = pagination.Links.ContainsKey(PaginationLinks.Next);
				page++;
			} while (hasMore);
			return result;
		}
	}
}
