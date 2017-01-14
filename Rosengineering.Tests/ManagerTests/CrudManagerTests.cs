﻿using System.Data.Entity;
using System.Threading.Tasks;
using Autofac;
using AutoMapper.QueryableExtensions.Impl;
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

		[Test]
		public void UpdateExistsModel()
		{
			var model = Fixture.Create<TModel>();
			var addResult = Manager.Add(model);
			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);

			var updateResult = Manager.Update(model);

			Assert.IsFalse(updateResult.HasError, updateResult.ErrorMessage);
			Assert.IsFalse(updateResult.WithErrors);
			Assert.IsTrue(updateResult.IsValid);
			Assert.IsEmpty(updateResult.FailPropeties);
		}

		[Test]
		public async Task UpdateExistsModelAsync()
		{
			var model = Fixture.Create<TModel>();
			var addResult = await Manager.AddAsync(model);
			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);

			var updateResult = await Manager.UpdateAsync(model);

			Assert.IsFalse(updateResult.HasError, updateResult.ErrorMessage);
			Assert.IsFalse(updateResult.WithErrors);
			Assert.IsTrue(updateResult.IsValid);
			Assert.IsEmpty(updateResult.FailPropeties);
		}

		[Test]
		public void UpdateNullModel()
		{
			var updateResult = Manager.Update(null);

			Assert.IsTrue(updateResult.HasError);
			Assert.IsNotEmpty(updateResult.ErrorMessage);
			Assert.IsTrue(updateResult.WithErrors);
			Assert.IsTrue(updateResult.IsValid);
			Assert.IsEmpty(updateResult.FailPropeties);
		}

		[Test]
		public async Task UpdateNullModelAsync()
		{
			var updateResult = await Manager.UpdateAsync(null);

			Assert.IsTrue(updateResult.HasError);
			Assert.IsNotEmpty(updateResult.ErrorMessage);
			Assert.IsTrue(updateResult.WithErrors);
			Assert.IsTrue(updateResult.IsValid);
			Assert.IsEmpty(updateResult.FailPropeties);
		}

		[Test]
		public void GetExistsModel()
		{
			var model = Fixture.Create<TModel>();
			var addResult = Manager.Add(model);
			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);

			var getResult = Manager.Get(model.Id);

			Assert.IsFalse(getResult.HasError, getResult.ErrorMessage);
			Assert.NotNull(getResult.Result);
		}

		[Test]
		public async Task GetExistsModelAsync()
		{
			var model = Fixture.Create<TModel>();
			var addResult = await Manager.AddAsync(model);
			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);

			var getResult = await Manager.GetAsync(model.Id);

			Assert.IsFalse(getResult.HasError, getResult.ErrorMessage);
			Assert.NotNull(getResult.Result);
		}

		[Test]
		public void GetNotExistsModel()
		{
			var getResult = Manager.Get(-1);
			Assert.IsFalse(getResult.HasError, getResult.ErrorMessage);
			Assert.Null(getResult.Result);
		}

		[Test]
		public async Task GetNotExistsModelAsync()
		{
			var getResult = await Manager.GetAsync(-1);
			Assert.IsFalse(getResult.HasError, getResult.ErrorMessage);
			Assert.Null(getResult.Result);
		}

		[Test]
		public void DeleteExistsModel()
		{
			var model = Fixture.Create<TModel>();
			var addResult = Manager.Add(model);
			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);

			var deleteResult = Manager.Delete(model.Id);

			Assert.False(deleteResult.HasError, deleteResult.ErrorMessage);
		}

		[Test]
		public async Task DeleteExistsModelAsync()
		{
			var model = Fixture.Create<TModel>();
			var addResult = await Manager.AddAsync(model);
			Assert.IsFalse(addResult.HasError, addResult.ErrorMessage);

			var deleteResult = await Manager.DeleteAsync(model.Id);

			Assert.False(deleteResult.HasError, deleteResult.ErrorMessage);
		}

		[Test]
		public void DeleteNotExistsModel()
		{
			var deleteResult =  Manager.Delete(-1);

			Assert.IsTrue(deleteResult.HasError);
		}

		[Test]
		public async Task DeleteNotExistsModelAsync()
		{
			var deleteResult = await Manager.DeleteAsync(-1);

			Assert.IsTrue(deleteResult.HasError);
		}


	}
}
