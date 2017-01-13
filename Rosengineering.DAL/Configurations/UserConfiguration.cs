using System.Data.Entity.ModelConfiguration;
using Rosengineering.DAL.Models;

namespace Rosengineering.DAL.Configurations
{
	public class UserConfiguration : EntityTypeConfiguration<User>
	{
		public UserConfiguration()
		{
			ToTable("Users");

			HasKey(i => i.Id);

			Property(m => m.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			Property(m => m.LastName)
				.IsOptional()
				.HasMaxLength(50);

			HasRequired(m => m.UserGroup)
				.WithMany()
				.HasForeignKey(m => m.UserGroupId);
		}
	}
}
