using System.Threading.Tasks;
using Akeneo.Model;
using Xunit;

namespace Akeneo.IntegrationTests
{
	public class MediaFileTests : IntegrationTestBase
	{
		[Fact]
		public async Task Shoud_Upload_Image()
		{
			/* Setup */
			var fileUpload = new MediaUpload
			{
				Product =
				{
					Identifier = "tyfon-bb-1000-1000-3m-3m",
					Attribute = "Product_Image_Medium"
				},
				FilePath = "C:\\tmp\\product_logo.png"
			};

			/* Test */
			var response = await Client.UploadAsync(fileUpload);
			/* Assert */
		}
	}
}
