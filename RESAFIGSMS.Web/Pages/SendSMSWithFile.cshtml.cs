using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RESAFIGSMS.Web.Helpers;
using RESAFIGSMS.Web.Models;
using RESAFIGSMS.Web.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RESAFIGSMS.Web.Pages
{
	public class SendSMSWithFileModel : PageModel
	{
		//[BindProperty]
		//public IFormFile Upload { get; set; }

		[BindProperty]
		public SMSMultipleModel SMSMultiples { get; set; } = default!;

		private IHostingEnvironment _env;
		public List<string> listeNumeros { get; set; } = new List<string>();

		public SendSMSWithFileModel(IHostingEnvironment environment)
		{
			_env = environment;
		}
		public void OnGet()
		{
		}

		public void LoadNumber()
		{
			//  var fileName = fileUpload
			//Console.WriteLine()
		}

		public async void OnPostLoadFile()
		{
			if (!ModelState.IsValid || SMSMultiples == null)
				return;

			var fileName = Path.Combine(_env.ContentRootPath, SMSMultiples.Upload.FileName);
			using (var fileStream = new FileStream(fileName, FileMode.Create))
			{
				await SMSMultiples.Upload.CopyToAsync(fileStream);
			}
			listeNumeros = FileHelpers.getListNumber(fileName);
			//SMSMultiples.listNumbers = listeNumeros;
			if (listeNumeros != null && listeNumeros.Count > 0)
			{
				int nbSms = 0;
				var smsClient = await SmsClient.Authenticate("Basic N1ptZFdTY0NUM1VCZkg1SFEzZW0yNWdna3h0WElBbDA6a2s5NHpRZ2xZcVdCOGZiMQ==");

				if (!string.IsNullOrEmpty(SMSMultiples.message))
				{
					foreach (var item in listeNumeros)
					{
						var response = await smsClient.SendSms(SMSMultiples.message, "2250000", item, SMSMultiples.senderName);
						if (response.IsSuccess)
						{
							nbSms++;
						}

					}
					ViewData["Msg"] = string.Format("{0} message(s) envoy\u00e9(s) avec succ\u00e8s", nbSms);
				}
			}


			//Send SMS
			//SMSMultiples.listNumbers = listeNumeros.ToList();


		}

		//     public async void OnPostAsync()
		//     {
		//         if (!ModelState.IsValid || SMSMultiples == null)
		//             return;
		////Send SMS
		//         //SMSMultiples.listNumbers = listeNumeros.ToList();
		//var smsClient = await SmsClient.Authenticate("Basic N1ptZFdTY0NUM1VCZkg1SFEzZW0yNWdna3h0WElBbDA6a2s5NHpRZ2xZcVdCOGZiMQ==");

		//if (!string.IsNullOrEmpty(SMSMultiples.message))
		//         {
		//             foreach (var item in SMSMultiples.listNumbers)
		//             {
		//                 var response = await smsClient.SendSms(SMSMultiples.message, "2250000",item,SMSMultiples.senderName);
		//             }
		//         }
		//     }
	}
}
