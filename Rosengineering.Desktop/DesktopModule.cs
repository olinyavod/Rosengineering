using System.Data.Entity;
using Autofac;
using FluentValidation;
using Rosengineering.BusinessLogic;
using Rosengineering.BusinessLogic.Managers;
using Rosengineering.BusinessLogic.Validators;
using Rosengineering.DAL;
using Rosengineering.DAL.Models;

namespace Rosengineering.Desktop
{
	public class DesktopModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<RosengineeringDbContext>()
				.SingleInstance();
			builder.RegisterType<RosengineeringDbInitializer>()
				.As<IDatabaseInitializer<RosengineeringDbContext>>()
				.SingleInstance();

			builder.RegisterType<UserValidator>()
				.As<IValidator<User>>()
				.SingleInstance();

			builder.RegisterType<UserGroupValidator>()
				.As<IValidator<UserGroup>>()
				.SingleInstance();

			builder.RegisterType<UserCrudManager>()
				.As<ICrudManager<User>>()
				.SingleInstance();

			builder.RegisterType<UserGroupCrudManager>()
				.As<ICrudManager<UserGroup>>()
				.SingleInstance();
		}
	}
}