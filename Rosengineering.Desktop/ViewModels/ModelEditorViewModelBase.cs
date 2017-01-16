using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using DevExpress.Mvvm;
using Rosengineering.BusinessLogic;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Services;
using Xceed.Wpf.AvalonDock.Properties;

namespace Rosengineering.Desktop.ViewModels
{
	public abstract class ModelEditorViewModelBase<TModel, TKey, TManager> : ViewModelBase, INotifyDataErrorInfo
		where TModel : class, IIdentity<TKey>
		where TManager : class, ICrudManager<TModel, TKey>
	{
		private readonly IDictionary<string, string> _validationErrors;

		public TKey Id { get; set; }

		protected ModelEditorViewModelBase()
		{
			_validationErrors = new Dictionary<string, string>();
			PropertyChanged += OnPropertyChanged;
			CancelCommand = new DelegateCommand(OnCancel, OnCanCancel);
			SaveCommand = new AsyncCommand(OnSaveAsync, OnCanSave);
			ClosingCommand = new AsyncCommand<CancelEventArgs>(OnClosing);
		}

		protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (_validationErrors.ContainsKey(e.PropertyName))
			{
				_validationErrors.Remove(e.PropertyName);
				HasErrors = _validationErrors.Count > 0;
				RaiseErrosChanged(e.PropertyName);
			}
			var property = Model.GetType().GetProperty(e.PropertyName);
			if (property != null)
				IsChanged = true;
		}

		public TModel Model
		{
			get { return GetProperty(() => Model); }
			set { SetProperty(() => Model, value, old => OnModelChanged(old, value)); }
		}

		protected virtual void OnModelChanged(TModel oldValue, TModel newValue)
		{
			this.Resolve<IMapper>()
				.Map(newValue, this);
			IsChanged = false;
		}

		public TManager Manager
		{
			get { return this.Resolve<TManager>(); }
		}

		public IEnumerable GetErrors(string propertyName)
		{
			return _validationErrors.ContainsKey(propertyName)
				? new[] {_validationErrors[propertyName]}
				: new string[0];
		}

		public bool HasErrors
		{
			get { return GetProperty(() => HasErrors); }
			set { SetProperty(() => HasErrors, value, old => OnHasErrorsChanged(old, value)); }
		}

		protected virtual void OnHasErrorsChanged(bool oldValue, bool newValue)
		{

		}

		public bool IsChanged
		{
			get { return GetProperty(() => IsChanged); }
			set { SetProperty(() => IsChanged, value, old => OnIsChangedOnChanged(old, value)); }
		}

		protected virtual void OnIsChangedOnChanged(bool oldValue, bool newValue)
		{

		}

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		protected void RaiseErrosChanged(string propertyName)
		{
			if (ErrorsChanged != null)
			{
				ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
			}
		}

		protected virtual TModel CreateNewModel()
		{
			return Activator.CreateInstance<TModel>();
		}

		public bool IsNew
		{
			get { return GetProperty(() => IsNew); }
			set { SetProperty(() => IsNew, value, oldValue => OnIsNewChanged(oldValue, value)); }
		}

		protected virtual void OnIsNewChanged(bool oldValue, bool newValue)
		{

		}

		public string DisplayTitle
		{
			get { return GetProperty(() => DisplayTitle); }
			set { SetProperty(() => DisplayTitle, value); }
		}

		protected override void OnParameterChanged(object parameter)
		{
			base.OnParameterChanged(parameter);
			var p = parameter as ModelEditorParameter<TKey>;
			if (p == null) return;

			if (p.IsNew)
			{
				Model = CreateNewModel();
				IsNew = p.IsNew;
			}
			else
			{
				var getResult = Manager.Get(p.Id);
				if (getResult.HasError)
				{
					this.ShowError(DisplayTitle, getResult);
					return;
				}
				Model = getResult.Result;
			}
		}

		public ICommand ClosingCommand { get; private set; }

		protected virtual string GetClosingMessage()
		{
			return Properties.Resources.msgClosingMessage;
		}

		protected virtual async Task OnClosing(CancelEventArgs e)
		{
			if (IsChanged)
			{
				e.Cancel = true;
				switch (
					GetService<IMessageBoxService>()
						.Show(GetClosingMessage(), DisplayTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
				{
					case MessageBoxResult.Yes:
						if (await SaveChangesAsync())
						{
							e.Cancel = false;
							GetService<ICurrentWindowService>().Close();

						}
						break;
					case MessageBoxResult.No:
						e.Cancel = false;
						break;
				}
			}
		}

		protected virtual async Task<bool> SaveChangesAsync()
		{
			TModel model = Model;
			this.Resolve<IMapper>().Map(this, model);
			ModifyStatus<TModel> result;
			if (IsNew)
			{
				result = await Manager.AddAsync(model);
			}
			else
			{
				result = await Manager.UpdateAsync(model);
			}
			if (result.HasError)
			{
				this.ShowError(DisplayTitle, result);
				return false;
			}
			if (!result.IsValid)
			{
				foreach (var propety in result.FailPropeties)
				{
					if (_validationErrors.ContainsKey(propety.PropertyName))
						_validationErrors[propety.PropertyName] = propety.ErrorMessage;
					else
						_validationErrors.Add(propety.PropertyName, propety.ErrorMessage);
					HasErrors = true;
					RaiseErrosChanged(propety.PropertyName);
				}
				return false;
			}
			IsChanged = false;
			return true;
		}

		public ICommand SaveCommand { get; private set; }

		protected virtual bool OnCanSave()
		{
			return IsChanged && !HasErrors;
		}

		protected virtual async Task OnSaveAsync()
		{
			if (await SaveChangesAsync())
			{
				GetService<ICurrentWindowService>().Close();
			}
		}

		public ICommand CancelCommand { get; private set; }

		protected virtual bool OnCanCancel()
		{
			return true;
		}

		protected virtual void OnCancel()
		{
			IsChanged = false;
			GetService<ICurrentWindowService>().Close();
		}

	}

	public abstract class ModelEditorViewModelBase<TModel, TManager> : ModelEditorViewModelBase<TModel,int, TManager> 
		where TModel : class, IIdentity<int>
		where TManager : class, ICrudManager<TModel, int>
	{
	}

	public class ModelEditorViewModelBase<TModel> : ModelEditorViewModelBase<TModel, ICrudManager<TModel>> 
		where TModel : class, IIdentity<int>
	{
	}
}