namespace Akeneo.Authentication
{
	public class RefreshTokenRequest
	{
		public string GrantType => "refresh_token";
		public string RefreshToken { get; set; }

		public static RefreshTokenRequest For(string refreshToken)
		{
			return new RefreshTokenRequest
			{
				RefreshToken = refreshToken
			};
		}
	}
}