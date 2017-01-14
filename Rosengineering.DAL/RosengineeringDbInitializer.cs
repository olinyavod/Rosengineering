using System.Data.Entity;
using System.Data.Entity.Migrations;
using Rosengineering.DAL.Migrations;
using Rosengineering.DAL.Models;

namespace Rosengineering.DAL
{
	public class RosengineeringDbInitializer : MigrateDatabaseToLatestVersion<RosengineeringDbContext, Configuration>
	{
		
	}
}
