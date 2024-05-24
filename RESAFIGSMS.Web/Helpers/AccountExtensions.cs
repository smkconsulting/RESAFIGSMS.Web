using RESAFIGSMS.Web.Models;
using static RESAFIGSMS.Web.Helpers.ResponseHelper;
using System.Net.Mail;
using System.Net;
using RESAFIGSMS.Web.Services;

namespace RESAFIGSMS.Web.Helpers
{
	public static class AccountExtensions
	{
		public static async Task<Result<string>> VueBalance(this SmsClient smsClient)
		{
			var response = await smsClient.GetAsync("sms/admin/v1/contracts");
			if (response.code == HttpStatusCode.OK)
				return new Result<string>(true, string.Empty, response.result);
			return GetError<string>(response);
		}
		public static async Task<Result<string>> GetStatistics(this SmsClient smsClient)
		{
			var response = await smsClient.GetAsync("sms/admin/v1/statistics");
			if (response.code == HttpStatusCode.OK)
				return new Result<string>(true, string.Empty, response.result);
			return GetError<string>(response);
		}
		public static async Task<Result<string>> GetPurchaseHistory(this SmsClient smsClient)
		{
			var response = await smsClient.GetAsync("sms/admin/v1/purchaseorders");
			if (response.code == HttpStatusCode.OK)
				return new Result<string>(true, string.Empty, response.result);
			return GetError<string>(response);
		}
	}
}
