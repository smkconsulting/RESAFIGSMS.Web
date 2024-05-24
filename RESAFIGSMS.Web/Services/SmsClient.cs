using Newtonsoft.Json;
using RESAFIGSMS.Web.Models;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace RESAFIGSMS.Web.Services
{
	public class SmsClient
	{
		public HttpClient HttpClient { get; }
		private SmsClient(HttpClient httpClient)
		{
			HttpClient = httpClient;
		}
		public static async Task<SmsClient> Authenticate(string authorizationHeader)
		{
			var httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://api.orange.com/")
			};
			httpClient.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
			var pairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("grant_type", "client_credentials")
			};

			var result = await httpClient.PostAsync("oauth/v3/token", new FormUrlEncodedContent(pairs));

			if (result.IsSuccessStatusCode)
			{
				httpClient = await GetAuthResult(result);
				return new SmsClient(httpClient);
			}
			throw new Exception(await result.Content.ReadAsStringAsync());
		}

		private static async Task<HttpClient> GetAuthResult(HttpResponseMessage result)
		{
			var response = JsonConvert.DeserializeObject<AuthenticationResponse>(await result.Content.ReadAsStringAsync());
			var httpClient = new HttpClient()
			{
				BaseAddress = new Uri("https://api.orange.com/")
			};
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(response.TokenType, response.AccessToken);
			return httpClient;
		}

		public async Task<(HttpStatusCode code, string result)> PostAsync(object content, string uri)
		{
			using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
			{
				var json = JsonConvert.SerializeObject(content);
				using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
				{
					request.Content = stringContent;

					using (var response = await HttpClient
						.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
						.ConfigureAwait(false))
					{
						return (response.StatusCode, await response.Content.ReadAsStringAsync());
					}
				}
			}
		}

		public async Task<(HttpStatusCode code, string result)> GetAsync(string uri)
		{
			var result = await HttpClient.GetAsync(uri);
			return (result.StatusCode, await result.Content.ReadAsStringAsync());
		}

	}
}
