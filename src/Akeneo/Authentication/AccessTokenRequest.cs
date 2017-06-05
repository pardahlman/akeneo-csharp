namespace Akeneo.Authentication
{
	public class AccessTokenRequest
	{
		public string GrantType => "password";
		public string Password { get; set; }
		public string Username { get; set; }

		public static AccessTokenRequest For(string userName, string password)
		{
			return new AccessTokenRequest
			{
				Username = userName,
				Password = password
			};
		}
	}
}