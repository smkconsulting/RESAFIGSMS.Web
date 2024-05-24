using Newtonsoft.Json;

namespace RESAFIGSMS.Web.Models
{
	public class AuthenticationResponse
	{
		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("expires_in")]
		public string TokenExpirationTime { get; set; }
	}
}
