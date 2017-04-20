using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Akeneo.Common;
using Akeneo.Model;
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
				AvailableLocales = new List<string> {Locales.EnglishUs, Locales.SwedenSwedish},
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
	}
}
