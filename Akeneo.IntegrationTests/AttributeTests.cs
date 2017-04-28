using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Akeneo.Common;
using Akeneo.Model;
using Akeneo.Model.Attributes;
using Akeneo.Serialization;
using Newtonsoft.Json;
using Xunit;

namespace Akeneo.IntegrationTests
{
	public class AttributeTests : IntegrationTestBase
	{
		[Fact]
		public async Task Get_Many_Async()
		{
			var pagination = await Client.GetManyAsync<AttributeBase>();

			Assert.NotNull(pagination);
			Assert.Equal(pagination.CurrentPage, 1);
		}

		[Fact]
		public async Task Create_Many_Async()
		{
			var result = await Client.CreateOrUpdateAsync(new List<NumberAttribute>
			{
				new NumberAttribute
				{
					AvailableLocales = new List<string> {Locales.EnglishUs, Locales.SwedenSwedish},
					Code = "test_6",
					Group = AkeneoDefaults.DefaultAttributeGroup,
					NegativeAllowed = true
				},
				new NumberAttribute
				{
					AvailableLocales = new List<string> {Locales.EnglishUs, Locales.SwedenSwedish},
					Code = "test_7",
					Group = AkeneoDefaults.DefaultAttributeGroup,
					NegativeAllowed = true
				}
			});

			Assert.NotNull(result);
			Assert.Equal(result.Count, 2);
		}

		[Fact]
		public async Task Create_Update_Delete_Number_Atttribute()
		{
			/* Create */
			var number = new NumberAttribute
			{
				AvailableLocales = new List<string> { Locales.EnglishUs, Locales.SwedenSwedish },
				Code = "test_2",
				Group = AkeneoDefaults.DefaultAttributeGroup,
				NegativeAllowed = true
			};

			var createResponse = await Client.CreateAsync(number);

			Assert.Equal(createResponse.Code, HttpStatusCode.Created);

			/* Update */
			number.NegativeAllowed = false;
			var updateResponse = await Client.UpdateAsync(number);

			Assert.Equal(updateResponse.Code, HttpStatusCode.NoContent);

			/* Delete */
			var deleteResponse = await Client.DeleteAsync<NumberAttribute>(number.Code);
			Assert.True(deleteResponse.Code == HttpStatusCode.MethodNotAllowed, "API does not support removal of attributes");
		}

		[Fact]
		public async Task Create_Image_Attribute()
		{
			var media = new MediaUpload
			{
				Product =
				{
					Identifier = "tyfon-bb-1000-1000-3m-3m",
					Attribute = "Product_Image_Medium"
				},
				FilePath = "C:\\tmp\\banhof.png",
				FileName = "banhof3.png"
			};

			var response = await Client.CreateAsync(media);
			var retrieved = await Client.GetManyAsync<MediaFile>();
		}

		[Fact]
		public async Task Should_Return()
		{
			var product = new Product
			{
				Identifier = "product_001",
			};
			//var response = await Client.CreateAsync(product);
			product.Values = new Dictionary<string, List<ProductValue>>
			{
				{"multiselect_attribute", new List<ProductValue> {new ProductValue {Data = "foo"}}}
			};
			var update = await Client.UpdateAsync(product);
		}
	}
}
