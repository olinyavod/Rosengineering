using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Rosengineering.BusinessLogic;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Messages;
using Rosengineering.Desktop.Properties;
using Rosengineering.Desktop.Services;

namespace Rosengineering.Desktop.ViewModels
{
	public abstract  class CrudListViewModelBase<TModel, TManager, TItem, TKey> : ViewModelBase
		where TModel : class, IIdentity<TKey>
		where TItem : class, IIdentity<TKey>
		where TManager : class, ICrudManager<TModel, TKey>
	{
		public  TManager Manager
		{
			get { return this.Resolve<TManager>(); }
		}
		private readonly CrudListConfig _config;

		protected CrudListViewModelBase(CrudListConfig config)
		{
			_config = config;
			DisplayTitle = config.DefaultTitle;
			DeleteCommand = new AsyncCommand(OnDeleteAsync, OnCanDelete);
			RefreshCommand = new AsyncCommand(OnRefreshAsync);
			DetailsCommand = new AsyncCommand(OnDetails, OnCanDetails);
			AddCommand = new AsyncCommand(OnAdd, OnCanAdd);
			OnLoadedCommand = new AsyncCommand(OnLoadedAsync);
		}

		public ICommand OnLoadedCommand { get; private set; }

		protected virtual Task OnLoadedAsync()
		{
			ItemsSource = GetItemsSource();
			return Task.FromResult(true);

		}

		public IQueryable<TItem> ItemsSource
		{
			get { return GetProperty(() => ItemsSource); }
			set { SetProperty(() => ItemsSource, value); }
		}

		public RefreshDataMessage RefreshMessage
		{
			get { return GetProperty(() => RefreshMessage); }
			set { SetProperty(() => RefreshMessage, value); }
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
			if (messageBox.Show(GetDeleteMessage(item), DisplayTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
			{
				return;
			}
			var waitService = GetService<ISplashScreenService>();
			try
			{
				waitService.ShowSplashScreen();
				var result = await Manager.DeleteAsync(item.Id);
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
			RefreshMessage = new RefreshDataMessage();
			return Task.FromResult(true);
		}

		public ICommand DetailsCommand { get; private set; }

		protected virtual bool OnCanDetails()
		{
			return SelectedItem != null;
		}

		protected virtual object CreateDetailsParameter(TItem item)
		{
			return new ModelEditorParameter<TKey> {Id = item.Id};
		}

		protected virtual Task OnDetails()
		{
			var windowService = GetService<IWindowService>();
			windowService.Title = _config.DetailsTitle;
			windowService
				.Show(_config.DetailsEditorViewName, CreateDetailsParameter(SelectedItem), this);
			return OnRefreshAsync();
		}

		public ICommand AddCommand { get; private set; }

		protected virtual bool OnCanAdd()
		{
			return true;
		}

		protected virtual Task OnAdd()
		{
			var windowService = GetService<IWindowService>();
			windowService.Title = _config.AddTitle;
			windowService
				.Show(_config.AddEditorViewName, CreateNewParameter(), this);
			return OnRefreshAsync();
		}

		private object CreateNewParameter()
		{
			return new ModelEditorParameter<TKey> {IsNew = true};
		}
	}

	public abstract class CrudListViewModelBase<TModel, TManager, TItem> : CrudListViewModelBase<TModel, TManager, TItem, int> 
		where TModel : class, IIdentity<int> 
		where TManager : class, ICrudManager<TModel> 
		where TItem : class, IIdentity<int>
	{
		protected CrudListViewModelBase(CrudListConfig config) : base(config)
		{
		}
	}

	public abstract class CrudListViewModelBase<TModel, TManager> : CrudListViewModelBase<TModel, TManager, TModel> 
		where TModel : class, IIdentity<int> 
		where TManager : class, ICrudManager<TModel> 
	{
		public CrudListViewModelBase(CrudListConfig config) 
			: base(config)
		{
		}

		protected override IQueryable<TModel> GetItemsSource()
		{
			return Manager.Query;
		}
	}

	public abstract  class CrudListViewModelBase<TModel> : CrudListViewModelBase<TModel, ICrudManager<TModel>>
		where TModel : class, IIdentity<int> 
	{
		public CrudListViewModelBase(CrudListConfig config)
			: base(config)
		{
		}
	}
}
