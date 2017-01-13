using Rosengineering.DAL.Models;

namespace Rosengineering.DAL.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<Rosengineering.DAL.RosengineeringDbContext>
    {
        public Configuration()
        {
	        AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rosengineering.DAL.RosengineeringDbContext context)
        {
	        
        }
    }
}
