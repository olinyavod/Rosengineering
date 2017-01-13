using System.Data.Entity;
using Autofac;
using Rosengineering.DAL;

namespace Rosengineering.Desktop
{
	public class DesktopModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<RosengineeringDbContext>()
				.SingleInstance();
			builder.RegisterType<RosengineeringDbInitializer>()
				.As<IDatabaseInitializer<RosengineeringDbContext>>()
				.SingleInstance();
		}
	}
}