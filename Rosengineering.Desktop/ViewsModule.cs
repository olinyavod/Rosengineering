using System.Windows;
using Autofac;

namespace Rosengineering.Desktop
{
	public class ViewsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<MainWindow>()
				.Named<DependencyObject>("Shell");
		}
	}
}