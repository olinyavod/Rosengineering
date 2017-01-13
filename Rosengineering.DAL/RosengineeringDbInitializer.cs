using System.Data.Entity;
using System.Data.Entity.Migrations;
using Rosengineering.DAL.Models;

namespace Rosengineering.DAL
{
	public class RosengineeringDbInitializer : CreateDatabaseIfNotExists<RosengineeringDbContext>
	{
		public RosengineeringDbInitializer()
		{
			
		}

		protected override void Seed(RosengineeringDbContext context)
		{
			base.Seed(context);
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
