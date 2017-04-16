using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Akeneo.Common;
using Akeneo.Model;
using Xunit;

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

			Assert.Equal(createResponse.Code, HttpStatusCode.Created);
			Assert.Equal(updateResponse.Code, HttpStatusCode.NoContent);
			Assert.Equal(deleteResponse.Code, HttpStatusCode.NoContent);
		}
	}
}
