using System.Linq;
using FluentValidation;
using Rosengineering.BusinessLogic.ListItems;
using Rosengineering.DAL;
using Rosengineering.DAL.Models;

namespace Rosengineering.BusinessLogic.Managers
{
	public class UserCrudManager : CrudManagerBase<User, UserItem>
	{
		private readonly RosengineeringDbContext _context;

		public UserCrudManager(IValidator<User> validator, RosengineeringDbContext context)
			: base(validator, context)
		{
			_context = context;
		}

		protected override IQueryable<UserItem> GetQuery()
		{
			return from u in _context.Set<User>()
				from g in _context.Set<UserGroup>().Where(i => i.Id == u.UserGroupId)
				select new UserItem
				{
					Id = u.Id,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Birthday = u.Birthday,
					UserGroupId = u.UserGroupId,
					UserGroupTitle = g.Title
				};
		}
	}
}