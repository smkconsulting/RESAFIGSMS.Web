namespace RESAFIGSMS.Web.Models
{
	public partial class SmsResponse
	{
		public string PhoneNumber { get; set; }
		public string Message { get; set; }
		public DateTime DateHeureEnvoiSms { get; set; }
		public string MessageResponse { get; set; }
	}
}
