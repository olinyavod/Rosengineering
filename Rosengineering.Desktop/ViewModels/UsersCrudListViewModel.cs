using System.Windows.Input;
using DevExpress.Mvvm;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Properties;
using Rosengineering.Desktop.Views;

namespace Rosengineering.Desktop.ViewModels
{
	public class UsersCrudListViewModel : CrudListViewModelBase<User>
	{
		public UsersCrudListViewModel() 
			: base(new CrudListConfig(Resources.ttlUsers, typeof(UserEditorView).Name)
			{
				AddTitle = Resources.ttlAddUser,
				DetailsTitle = Resources.ttlEditUser
			})
		{
			ShowUserGroupsCommand = new DelegateCommand(OnShowUserGroups);
		}

		protected override string GetDeleteMessage(User item)
		{
			return string.Format(Resources.msgDeleteItem, string.Join(" ", item.FirstName, item.LastName));
		}

		public ICommand ShowUserGroupsCommand { get; private set; }

		void OnShowUserGroups()
		{
			var windowService = GetService<IWindowService>();
			windowService.Title = Resources.ttlUserGroups;
			windowService.Show(typeof(UserGroupsListView).Name, null, this);
		}
	}
}