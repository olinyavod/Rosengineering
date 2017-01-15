using System;

namespace Rosengineering.DAL.Models
{
	public class User : ModelBase
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime Birthday { get; set; }

		public int UserGroupId { get; set; }

		public virtual UserGroup UserGroup { get; set; }

	}
}