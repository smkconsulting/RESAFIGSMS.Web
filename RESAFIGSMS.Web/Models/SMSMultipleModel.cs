using System.ComponentModel.DataAnnotations;

namespace RESAFIGSMS.Web.Models
{
	public class SMSMultipleModel
	{
		[Display(Name = "Destinataire")]
		public IFormFile Upload { get; set; }
		[Display(Name = "Expéditeur")]
		public string senderName { get; set; } = "RESAFIG";
		[Display(Name = "Message")]
		public string message { get; set; }
		//public string Email { get; set; }
	}
}
