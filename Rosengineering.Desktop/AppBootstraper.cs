using System.Data.Entity;
using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Prism.Autofac;
using Rosengineering.DAL;

namespace Rosengineering.Desktop
{
	public class AppBootstraper : AutofacBootstrapper
	{
		protected override void ConfigureContainerBuilder(ContainerBuilder builder)
		{
			base.ConfigureContainerBuilder(builder);
			builder.RegisterModule<DesktopModule>();
		}

		protected override IContainer CreateContainer(ContainerBuilder containerBuilder)
		{
			var container = base.CreateContainer(containerBuilder);
			ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
			return container;
		}

		protected override DependencyObject CreateShell()
		{
			return Container.ResolveNamed<DependencyObject>("Shell");
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();
			Application.Current.MainWindow = ((Window) Shell);
			Application.Current.MainWindow.Show();
		}

		protected override void InitializeModules()
		{
			Database.SetInitializer(Container.Resolve<IDatabaseInitializer<RosengineeringDbContext>>());
			base.InitializeModules();
		}
	}
}
