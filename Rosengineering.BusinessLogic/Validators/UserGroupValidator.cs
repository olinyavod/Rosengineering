using FluentValidation;
using Rosengineering.BusinessLogic.Properties;
using Rosengineering.DAL.Models;

namespace Rosengineering.BusinessLogic.Validators
{
	public class UserGroupValidator : AbstractValidator<UserGroup>
	{
		public UserGroupValidator()
		{
			RuleFor(m => m.Title)
				.Length(0, 50)
				.WithMessage(string.Format(Resources.msgTitleLength, 50));

			RuleFor(m => m.Title)
				.NotEmpty()
				.WithMessage(Resources.msgTitleNotEmpty);
		}
	}
}