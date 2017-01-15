using System;
using System.Windows;
using DevExpress.Mvvm;
using Rosengineering.BusinessLogic;
using Xceed.Wpf.DataGrid.Views;

namespace Rosengineering.Desktop.Services
{
	public static  class ViewModelExtensions
	{
		public static void ShowError(this object viewModel, string title, Exception ex)
		{
			var messageBox = viewModel.GetMessageBoxService();
			if (messageBox != null)
				messageBox.Show(ex.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		public static IMessageBoxService GetMessageBoxService(this object viewModel)
		{
			var services = viewModel as ISupportServices;
			if (services != null)
				services.ServiceContainer.GetService<IMessageBoxService>();
			return null;
		}

		public static TStatus ShowError<TStatus>(this object viewModel, string title, TStatus status)
			where TStatus : ExecuteStatus
		{
			if (status.HasError)
			{
				var messageBox = viewModel.GetMessageBoxService();
				if (messageBox != null)
					messageBox.Show(status.ErrorMessage, title, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return status;
		}
	}
}
