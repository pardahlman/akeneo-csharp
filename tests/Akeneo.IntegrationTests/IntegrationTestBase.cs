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
				ApiEndpoint = new Uri("https://staplesca-staging.cloud.akeneo.com"),
				ClientId = "1_1efsu0wk2u80sc8swssgs0kk4wgg0cw8wg8osw8ssg8cg8o8kk",
				ClientSecret = "584gak576z4sosg4g44w48w8kwwo4gsco84440wcg80cswkg0s",
				UserName = "hartej.grewal@bounteous.com",
				Password = "Hartej4Staples"
            });
		}

		public void Dispose()
		{
			Client?.Dispose();
		}
	}
}
