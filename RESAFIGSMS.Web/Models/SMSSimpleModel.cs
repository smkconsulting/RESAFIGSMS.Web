using System.ComponentModel.DataAnnotations;

namespace RESAFIGSMS.Web.Models
{
	public class SMSSimpleModel
	{
		[Display(Name = "Destinataire")]
		public string phoneNumber { get; set; }
		[Display(Name = "Expéditeur")]
		public string senderName { get; set; } = "RESAFIG";
		[Display(Name = "Message")]
		public string message { get; set; }
		//public string Email { get; set; }
	}
}
