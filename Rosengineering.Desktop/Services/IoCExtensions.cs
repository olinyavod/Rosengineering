using System;
using System.Windows;
using Autofac;
using Microsoft.Practices.ServiceLocation;

namespace Rosengineering.Desktop.Services
{
	public static class IoCExtensions
	{
		public static TInstance Resolve<TInstance>(this object item)
		{
			return ServiceLocator.Current.GetInstance<TInstance>();
		}

		public static TInstance Resolve<TInstance>(this object item, string name)
		{
			return ServiceLocator.Current.GetInstance<TInstance>(name);
		}

		public static ContainerBuilder RegisterView(this ContainerBuilder builder, Type viewType, string viewName)
		{
			builder.Register(c => new ViewInfo
				{
					ViewName = viewName,
					ViewType = viewType
				}).Keyed<ViewInfo>(viewType)
				.Named<ViewInfo>(viewName)
				.SingleInstance();

			builder.RegisterType(viewType)
				.Named<DependencyObject>(viewName);
			return builder;
		}

		public static ContainerBuilder RegisterView<TView>(this ContainerBuilder builder)
		{
			var viewType = typeof(TView);
			return RegisterView(builder, viewType, viewType.Name);
		}

		public static ContainerBuilder RegisterView<TView>(this ContainerBuilder builder, string viewName)
		{
			var viewType = typeof(TView);
			return RegisterView(builder, viewType, viewName);
		}

		public static ContainerBuilder RegisterViewModel(this ContainerBuilder builder, Type viewModelType,
			string viewModelName)
		{
			builder.Register(c => new ViewModelInfo
				{
					ViewModelName = viewModelName,
					ViewModelType = viewModelType
				}).Keyed<ViewModelInfo>(viewModelType)
				.Named<ViewModelInfo>(viewModelName)
				.SingleInstance();

			builder.RegisterType(viewModelType)
				.Named<object>(viewModelName);

			return builder;
		}

		public static ContainerBuilder RegisterViewModel<TViewModel>(this ContainerBuilder builder, string name)
		{
			var type = typeof(TViewModel);
			return RegisterViewModel(builder, type, name);
		}

		public static ContainerBuilder RegisterViewModel<TViewModel>(this ContainerBuilder builder)
		{
			return RegisterViewModel<TViewModel>(builder, typeof(TViewModel).Name);
		}
	}
}
