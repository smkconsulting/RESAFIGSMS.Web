using Newtonsoft.Json;

namespace RESAFIGSMS.Web.Models
{
	public class SmsMessageRequest
	{
		[JsonProperty("address")]
		public string Recipient { get; set; }
		[JsonProperty("senderAddress")]
		public string Sender { get; set; }
		[JsonProperty("senderName")]
		public string SenderName { get; set; }
		[JsonProperty("outboundSMSTextMessage")]
		public SmsTextMessage SmsTextMessage { get; set; }
	}
}
