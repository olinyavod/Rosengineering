using System.Data.Entity;

namespace Rosengineering.BusinessLogic
{
	public class ExecuteStatus
	{
		public bool HasError { get; set; }

		public string ErrorMessage { get; set; }
	}

	public class ExecuteStatus<TResult> : ExecuteStatus
	{
		public TResult Result { get; set; }
	}
}