using System.Data.Entity;
using System.Windows;
using Autofac;
using Rosengineering.DAL;

namespace Rosengineering.Desktop
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static IContainer Container { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var builder = new ContainerBuilder();
			builder.RegisterModule<DesktopModule>();

			Container = builder.Build();

			Container.Resolve<IDatabaseInitializer<RosengineeringDbContext>>()
				.InitializeDatabase(Container.Resolve<RosengineeringDbContext>());
		}
	}
}
