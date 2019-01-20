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
			    ApiEndpoint = new Uri("http://localhost:8081"),
			    ClientId = "1_27xlkd53wou8ogggwwwksk48s0sgsoogwkowws8wko88gcs0os",
			    ClientSecret = "65rfqpc5a3okws0w4k0kgcwswwg0ggwg48wc40gcckso88sk44",
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
