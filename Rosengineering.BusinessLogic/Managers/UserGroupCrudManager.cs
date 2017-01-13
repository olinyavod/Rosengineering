using FluentValidation;
using Rosengineering.DAL;
using Rosengineering.DAL.Models;

namespace Rosengineering.BusinessLogic.Managers
{
	public class UserGroupCrudManager : CrudManagerBase<UserGroup>
	{
		public UserGroupCrudManager(IValidator<UserGroup> validator, RosengineeringDbContext context) : base(validator, context)
		{
		}
	}
}