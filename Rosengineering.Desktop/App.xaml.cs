using System.Data.Entity;
using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using Microsoft.Practices.ServiceLocation;
using Rosengineering.DAL;
using Rosengineering.Desktop.Services;

namespace Rosengineering.Desktop
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App
	{
		public IContainer Container { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var builder = new ContainerBuilder();
			builder.RegisterModule<DesktopModule>();

			Container = builder.Build();
			ViewLocator.Default = new AutofacViewLocator(Container);
			ViewModelLocator.Default = new AutofacViewModelLocator(Container);

			ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));

			Database.SetInitializer(Container.Resolve<IDatabaseInitializer<RosengineeringDbContext>>());

		}
	}
}
