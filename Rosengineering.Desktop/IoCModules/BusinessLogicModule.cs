using Autofac;
using FluentValidation;
using Rosengineering.BusinessLogic;
using Rosengineering.BusinessLogic.Managers;
using Rosengineering.BusinessLogic.Validators;
using Rosengineering.DAL.Models;

namespace Rosengineering.Desktop.IoCModules
{
	public class BusinessLogicModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

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