using Rosengineering.DAL.Models;

namespace Rosengineering.DAL.Migrations
{
	using System.Data.Entity.Migrations;

	public sealed class Configuration : DbMigrationsConfiguration<Rosengineering.DAL.RosengineeringDbContext>
    {
        public Configuration()
        {
	        AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rosengineering.DAL.RosengineeringDbContext context)
        {
			context.Set<UserGroup>()
				.AddOrUpdate(m => m.Title,
					new UserGroup
					{
						Title = "VIP"
					},
					new UserGroup
					{
						Title = "Обычный"
					});	
		}
    }
}
