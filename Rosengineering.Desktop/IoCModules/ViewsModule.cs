using Autofac;
using Rosengineering.Desktop.Services;
using Rosengineering.Desktop.Views;

namespace Rosengineering.Desktop.IoCModules
{
	public class ViewsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterView<UserView>();
		}
	}
}