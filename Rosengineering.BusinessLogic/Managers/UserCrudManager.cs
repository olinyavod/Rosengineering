using FluentValidation;
using Rosengineering.DAL;
using Rosengineering.DAL.Models;

namespace Rosengineering.BusinessLogic.Managers
{
	public class UserCrudManager : CrudManagerBase<User>
	{
		public UserCrudManager(IValidator<User> validator, RosengineeringDbContext context) : base(validator, context)
		{
		}
	}
}