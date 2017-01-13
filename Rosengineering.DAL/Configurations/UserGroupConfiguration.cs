using System.Data.Entity.ModelConfiguration;
using Rosengineering.DAL.Models;

namespace Rosengineering.DAL.Configurations
{
	public class UserGroupConfiguration : EntityTypeConfiguration<UserGroup>
	{
		public UserGroupConfiguration()
		{
			ToTable("UserGroups");

			HasKey(m => m.Id);

			Property(m => m.Title)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}