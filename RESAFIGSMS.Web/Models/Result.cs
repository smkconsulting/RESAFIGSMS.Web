namespace RESAFIGSMS.Web.Models
{
	public class Result<T> where T : class
	{

		public Result(bool isSuccess, string error, T value)
		{
			IsSuccess = isSuccess;
			Error = error;
			Value = value;
		}


		public string Error { get; }
		public bool IsSuccess { get; }
		public T Value { get; }
	}
}
