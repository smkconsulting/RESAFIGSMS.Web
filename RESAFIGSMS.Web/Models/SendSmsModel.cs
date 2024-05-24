using Newtonsoft.Json;

namespace RESAFIGSMS.Web.Models
{
	public class SendSmsModel
	{
		[JsonProperty("outboundSMSMessageRequest")]
		public SmsMessageRequest SmsMessageRequest { get; set; }

		//[JsonProperty("senderName")]
		//public string SenderName { get; set; }
	}
}
