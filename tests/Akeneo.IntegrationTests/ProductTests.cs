using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Akeneo.Common;
using Akeneo.Model;
using Akeneo.Search;
using Xunit;
using Category = Akeneo.Search.Category;
using Family = Akeneo.Search.Family;
using ProductValue = Akeneo.Search.ProductValue;

namespace Akeneo.IntegrationTests
{
	public class ProductTests : IntegrationTestBase
	{
		[Fact]
		public async Task Should_Be_Able_To_Add_Update_And_Remove_Product()
		{
			var product = new Product
			{
				Identifier = "test_product",
				Categories = new List<string> {AkeneoDefaults.Category},
				Enabled = true
			};

			var createResponse = await Client.CreateAsync(product);
			var updateResponse = await Client.UpdateAsync(product);
			var deleteResponse = await Client.DeleteAsync<Product>(product.Identifier);
			Assert.Equal(HttpStatusCode.Created, createResponse.Code);
			Assert.Equal(HttpStatusCode.NoContent, updateResponse.Code);
			Assert.Equal(HttpStatusCode.NoContent, deleteResponse.Code);
		}

		[Fact]
		public async Task Should_Be_Able_To_Search()
		{
		    var result = await Client.SearchAsync<Product>(new List<Criteria>
		    {
		        ProductValue.Contains("sku", "tv"),
		        Category.In("Default_Base_Pack_Template"),
		        Family.In("Default_Base_Pack_Template"),
		        Completeness.Equal(100, AkeneoDefaults.Channel),
		        Status.Enabled()
		    });
        }

		[Fact]
		public async Task Should_Be_Able_To_Update_With_Dynamic_Object()
		{
			var result = await Client.UpdateAsync<Product>("tyfon-tv-6m-0m-company", new {categories = new []{ "boxer" } });
		}
	}
}
