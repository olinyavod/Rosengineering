using System;
using Rosengineering.DAL.Models;

namespace Rosengineering.BusinessLogic.ListItems
{
	public class UserItem : IIdentity<int>
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime Birthday { get; set; }

		public string UserGroupTitle { get; set; }

		public int UserGroupId { get; set; }
	}
}
