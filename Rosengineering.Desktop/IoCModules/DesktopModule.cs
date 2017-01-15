using System.Collections.Generic;
using System.Data.Entity;
using Autofac;
using AutoMapper;
using Rosengineering.DAL;
using Rosengineering.Desktop.Services;

namespace Rosengineering.Desktop.IoCModules
{
	public class DesktopModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterModule(new ViewsModule())
				.RegisterModule(new BusinessLogicModule());

			builder.RegisterType<RosengineeringDbContext>()
				.SingleInstance();
			builder.RegisterType<RosengineeringDbInitializer>()
				.As<IDatabaseInitializer<RosengineeringDbContext>>()
				.SingleInstance();

			builder.RegisterType<Mapping>()
				.As<Profile>();

			builder.Register(c => new MapperConfiguration(cfg =>
			{
				foreach (var profile in this.Resolve<IEnumerable<Profile>>())
				{
					cfg.AddProfile(profile);
				}
			})).SingleInstance();

			builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper())
				.SingleInstance();
		}

		
	}
}