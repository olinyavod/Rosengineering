using System.Data.Entity;

namespace Rosengineering.DAL
{
	public class RosengineeringDbContext : DbContext
	{
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
		}
	}
}
