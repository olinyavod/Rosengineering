using System.Collections.Generic;
using FluentValidation.Results;

namespace Rosengineering.BusinessLogic
{
	public class ModifyStatus<TResult> : ExecuteStatus<TResult>
	{
		public bool IsValid { get; set; }

		public IEnumerable<ValidationFailure> FailPropeties { get; set; }

		public bool WIthErrors => HasError || !IsValid;
	}
}