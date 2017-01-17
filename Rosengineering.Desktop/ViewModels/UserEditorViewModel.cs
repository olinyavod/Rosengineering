using System;
using System.Collections.Generic;
using System.Linq;
using Rosengineering.BusinessLogic;
using Rosengineering.BusinessLogic.ListItems;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Services;

namespace Rosengineering.Desktop.ViewModels
{
	public class UserEditorViewModel : ModelEditorViewModelBase<User, UserItem>
	{
		protected override void OnParameterChanged(object parameter)
		{
			base.OnParameterChanged(parameter);
			UserGroupsSource = this.Resolve<ICrudManager<UserGroup>>().Query.ToList();
			var p = parameter as ModelEditorParameter<int>;
			if(p == null)return;
			if (p.IsNew)
			{
				Birthday = DateTime.Today;
				IsChanged = false;
			}
		}

		public string FirstName
		{
			get { return GetProperty(() => FirstName); }
			set { SetProperty(() => FirstName, value); }
		}

		public string LastName
		{
			get { return GetProperty(() => LastName); }
			set { SetProperty(() => LastName, value); }
		}

		public int UserGroupId
		{
			get { return GetProperty(() => UserGroupId); }
			set { SetProperty(() => UserGroupId, value); }
		}

		public UserGroup UserGroup
		{
			get { return GetProperty(() => UserGroup); }
			set { SetProperty(() => UserGroup, value); }
		}

		public IEnumerable<UserGroup> UserGroupsSource
		{
			get { return GetProperty(() => UserGroupsSource); }
			set { SetProperty(() => UserGroupsSource, value); }
		}

		public DateTime Birthday
		{
			get { return GetProperty(() => Birthday); }
			set { SetProperty(() => Birthday, value); }
		}
	}
}
