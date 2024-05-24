using Newtonsoft.Json;

namespace RESAFIGSMS.Web.Models
{
	public class SmsTextMessage
	{
		[JsonProperty("message")]
		public string Message { get; set; }
	}
}
