using System.Data.Entity;
using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rosengineering.BusinessLogic;
using Rosengineering.DAL;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop;

namespace Rosengineering.Tests.ManagerTests
{
	[TestFixture(typeof(User), typeof(ManagerTestConfg<User>))]
	[TestFixture(typeof(UserGroup), typeof(ManagerTestConfg<UserGroup>))]
	public class CrudManagerTests<TModel, TConfig> 
		where TModel : class, IIdentity<int>
		where TConfig : IManagerTestConfig, new()
	{
		public IContainer Container { get; private set; }

		public ICrudManager<TModel> Manager { get; private set; }

		public IFixture Fixture { get; private set; }

		public TConfig Config { get; private set; }

		[SetUp]
		public virtual void OnSetUp()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new DesktopModule());

			Container = builder.Build();

			Database.SetInitializer(Container.Resolve<IDatabaseInitializer<RosengineeringDbContext>>());

			Manager = Container.Resolve<ICrudManager<TModel>>();

			Fixture = new Fixture();
			Config = new TConfig {Container = Container};
			Config.ConfigureFixture(Fixture);
		}
		
		

		[Test]
		public void ShouldAddWithoutErrors()
		{
			var oldModel = Fixture.Create<TModel>();
			var addResult = Manager.Add(oldModel);

			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);
			Assert.IsTrue(addResult.IsValid);
			Assert.IsEmpty(addResult.FailPropeties);
			Assert.IsFalse(addResult.WithErrors);
		}

		[Test]
		public async Task ShouldAddWithoutErrorsAsync()
		{
			var oldModel = Fixture.Create<TModel>();
			var addResult = await Manager.AddAsync(oldModel);

			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);
			Assert.IsTrue(addResult.IsValid);
			Assert.IsEmpty(addResult.FailPropeties);
			Assert.IsFalse(addResult.WithErrors);
		}

		[Test]
		public void ShouldAddWithByNullModel()
		{
			var addResult = Manager.Add(null);

			Assert.IsTrue(addResult.HasError);
			Assert.IsNotEmpty(addResult.ErrorMessage);
			Assert.IsTrue(addResult.IsValid);
			Assert.IsEmpty(addResult.FailPropeties);
			Assert.IsTrue(addResult.WithErrors);
		}

		[Test]
		public async Task ShouldAddWithByNullModelAsync()
		{
			var addResult = await Manager.AddAsync(null);

			Assert.IsTrue(addResult.HasError);
			Assert.IsNotEmpty(addResult.ErrorMessage);
			Assert.IsTrue(addResult.IsValid);
			Assert.IsEmpty(addResult.FailPropeties);
			Assert.IsTrue(addResult.WithErrors);
		}

		[Test]
		public void ShouldbeNotAddInvalidModel()
		{
			var addResult = Manager.Add(((TModel) Config.CreateInvalidModel()));

			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);
			Assert.IsFalse(addResult.IsValid);
			Assert.IsNotEmpty(addResult.FailPropeties);
			Assert.IsTrue(addResult.WithErrors);
		}

		[Test]
		public async Task ShouldbeNotAddInvalidModelAsync()
		{
			var addResult = await Manager.AddAsync(((TModel)Config.CreateInvalidModel()));

			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);
			Assert.IsFalse(addResult.IsValid);
			Assert.IsNotEmpty(addResult.FailPropeties);
			Assert.IsTrue(addResult.WithErrors);
		}

		
	}
}
