using System;

namespace Akeneo.IntegrationTests
{
	public abstract class IntegrationTestBase : IDisposable
	{
		public AkeneoClient Client { get; set; }

		protected IntegrationTestBase()
		{
			Client = new AkeneoClient(new AkeneoOptions
			{
				ApiEndpoint = new Uri("http://localhost:8080"),
				ClientId = "1_5zt5dbkbo50cogwkcw00woosgc08wocg8ccs80844ow48kkgkg",
				ClientSecret = "4gr06ahrp4e8cowo4w0s4c8owkcw0skg084cko0wcow0cok8sg",
				UserName = "admin",
				Password = "admin"
			});
		}

		public void Dispose()
		{
			Client?.Dispose();
		}
	}
}
