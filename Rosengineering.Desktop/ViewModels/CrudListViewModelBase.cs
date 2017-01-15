using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Rosengineering.BusinessLogic;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Properties;
using Rosengineering.Desktop.Services;

namespace Rosengineering.Desktop.ViewModels
{
	public abstract  class CrudListViewModelBase<TModel, TItem, TKey> : ViewModelBase
		where TModel : class, IIdentity<TKey>
		where TItem : class, IIdentity<TKey>
	{
		private readonly ICrudManager<TModel, TKey> _manager;

		protected CrudListViewModelBase(ICrudManager<TModel, TKey> manager)
		{
			_manager = manager;
			DeleteCommand = new AsyncCommand(OnDeleteAsync, OnCanDelete);
			RefreshCommand = new AsyncCommand(OnRefreshAsync);
		}

		protected override void OnParameterChanged(object parameter)
		{
			base.OnParameterChanged(parameter);
			ItemsSource = GetItemsSource();
		}

		public IQueryable<TItem> ItemsSource
		{
			get { return GetProperty(() => ItemsSource); }
			set { SetProperty(() => ItemsSource, value); }
		}

		protected abstract IQueryable<TItem> GetItemsSource();

		public TItem SelectedItem
		{
			get { return GetProperty(() => SelectedItem); }
			set { SetProperty(() => SelectedItem, value); }
		}

		public string DisplayTitle
		{
			get { return GetProperty(() => DisplayTitle); }
			set { SetProperty(() => DisplayTitle, value); }
		}

		public ICommand DeleteCommand { get; private set; }

		protected virtual bool OnCanDelete()
		{
			return SelectedItem != null;
		}

		protected virtual string GetDeleteMessage(TItem item)
		{
			return string.Format(Resources.msgDeleteItem, item);
		}

		protected virtual async Task OnDeleteAsync()
		{
			var messageBox = GetService<IMessageBoxService>();
			var item = SelectedItem;
			if (messageBox.Show(GetDeleteMessage(item), DisplayTitle, MessageButton.YesNo, MessageIcon.Question) != MessageBoxResult.Yes)
			{
				return;
			}
			var waitService = GetService<ISplashScreenService>();
			try
			{
				waitService.ShowSplashScreen();
				var result = await _manager.DeleteAsync(item.Id);
				if (result.HasError)
				{
					this.ShowError(DisplayTitle, result);
					return;
				}
				await OnRefreshAsync();
			}
			catch (Exception e)
			{
				waitService.HideSplashScreen();
				this.ShowError(DisplayTitle, e);
			}
			finally
			{
				waitService.HideSplashScreen();
			}
		}

		public ICommand RefreshCommand { get; set; }

		protected virtual Task OnRefreshAsync()
		{
			ItemsSource = GetItemsSource();
			return Task.FromResult(true);
		}

		public ICommand DetailsCommand { get; private set; }

		protected virtual bool OnCanDetails()
		{
			return SelectedItem != null;
		}

		protected abstract string DetailsViewName { get; }

		protected virtual object CreateDetailsParameter(TItem item)
		{
			return new ModelEditorParameter {Id = item.Id};
		}

		protected virtual Task OnDetails()
		{
			GetService<IWindowService>()
				.Show(DetailsViewName, CreateDetailsParameter(SelectedItem), this);
			return Task.FromResult(true);
		}
	}
}
