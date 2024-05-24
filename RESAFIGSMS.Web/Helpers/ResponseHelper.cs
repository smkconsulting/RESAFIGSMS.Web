using RESAFIGSMS.Web.Models;
using System.Net;

namespace RESAFIGSMS.Web.Helpers
{
	public static class ResponseHelper
	{
		public static Result<T> GetError<T>((HttpStatusCode code, string result) response) where T : class
		{
			return new Result<T>(false, $"error_code: {(int)response.code}, body: {response.result}", null);
		}
	}
}
