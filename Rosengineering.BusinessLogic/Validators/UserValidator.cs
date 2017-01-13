using FluentValidation;
using Rosengineering.BusinessLogic.Properties;
using Rosengineering.DAL.Models;

namespace Rosengineering.BusinessLogic.Validators
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator()
		{
			RuleFor(m => m.FirstName)
				.NotEmpty()
				.WithMessage(Resources.msgFirstNameNotEmpty);

			RuleFor(m => m.FirstName)
				.Length(0, 50)
				.WithMessage(string.Format(Resources.msgFisrtNameLength, 50));

			RuleFor(m => m.LastName)
				.Length(0, 50)
				.WithMessage(string.Format(Resources.msdLastNameLehgtn, 50));

			RuleFor(m => m.UserGroup)
				.NotNull()
				.WithMessage(Resources.msgMustSelectUsrGroup);
		}
	}
}
