using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RESAFIGSMS.Web.Helpers;
using RESAFIGSMS.Web.Models;
using RESAFIGSMS.Web.Services;

namespace RESAFIGSMS.Web.Pages
{
    public class SendSimpleSMSModel : PageModel
    {
        [BindProperty]
        public SMSSimpleModel SMSSimpleModels { get; set; } = default!;

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid || SMSSimpleModels == null)
                return;

            //Send SMS
            var smsClient = await SmsClient.Authenticate("Basic N1ptZFdTY0NUM1VCZkg1SFEzZW0yNWdna3h0WElBbDA6a2s5NHpRZ2xZcVdCOGZiMQ==");

            var response = await smsClient.SendSms(SMSSimpleModels.message, "2250000", SMSSimpleModels.phoneNumber, SMSSimpleModels.senderName);
            if (response.IsSuccess)
                ViewData["Msg"] = string.Format("Message envoy\u00e9 avec succ\u00e8s");
            else
                ViewData["Msg"] = string.Format("Une erreur s'est produite lors de l'envoi du sms");

        }

        //private static async Task SendingSimpleSms(SmsClient smsClient)
        //{
        //	if (!string.IsNullOrEmpty(message))
        //	{
        //		//foreach (var item in listPhoneNumber)
        //		//{
        //			var response = await smsClient.SendSms(message, , item, senderName);
        //			if (response.IsSuccess)
        //				isSuccess = true;

        //			else
        //				isSuccess = false;
        //		//}

        //	}

        //}
    }
}
