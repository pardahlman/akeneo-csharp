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
				ClientId = "1_3qwnpneuey80o080g0gco84ow4gsoo88skc880ssckgcg0okkg",
				ClientSecret = "3aw5l2xnvugwg0kc800g4k8s4coo80kkkc8ccs0so08gg08oc8",
				UserName = "admin",
				Password = "admdin"
			});
		}

		public void Dispose()
		{
			Client?.Dispose();
		}
	}
}
