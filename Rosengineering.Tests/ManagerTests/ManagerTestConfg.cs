using Autofac;
using Ploeh.AutoFixture;
using Rosengineering.DAL.Models;

namespace Rosengineering.Tests.ManagerTests
{
	public class ManagerTestConfg<TModel> : IManagerTestConfig
		where TModel : class, IIdentity<int>, new()
	{
		public IContainer Container { get; set; }
		public void ConfigureFixture(IFixture fixture)
		{
			fixture.Customize<TModel>(cfg => cfg.Without(i => i.Id));
		}

		public object CreateInvalidModel()
		{
			return new TModel();
		}
	}
}