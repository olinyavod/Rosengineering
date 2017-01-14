using System.Data.Entity;
using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using Rosengineering.DAL;

namespace Rosengineering.Desktop
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App
	{
		public AppBootstraper Bootstraper { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Bootstraper = new AppBootstraper();
			Bootstraper.Run();
		}
	}
}
