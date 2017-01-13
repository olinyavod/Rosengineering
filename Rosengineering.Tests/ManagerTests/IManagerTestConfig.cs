using Autofac;
using Ploeh.AutoFixture;

namespace Rosengineering.Tests.ManagerTests
{
	public interface IManagerTestConfig
	{
		IContainer Container { get; set; }

		void ConfigureFixture(IFixture fixture);

		object CreateInvalidModel();
	}
}