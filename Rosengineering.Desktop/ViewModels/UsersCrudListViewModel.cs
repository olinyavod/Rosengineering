using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Behaviors;
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
			ExitCommand = new DelegateCommand(OnExit);
		}

		protected override string GetDeleteMessage(User item)
		{
			return string.Format(Resources.msgDeleteItem, string.Join(" ", item.FirstName, item.LastName));
		}

		public ICommand ShowUserGroupsCommand { get; private set; }

		void OnShowUserGroups()
		{
			var windowService = GetService<IWindowServiceEx>();
			windowService.Title = Resources.ttlUserGroups;
			windowService.Size = new Size(300, 350);
			windowService.Show(typeof(UserGroupsListView).Name, null, this);
		}

		public ICommand ExitCommand { get; private set; }

		void OnExit()
		{
			GetService<ICurrentWindowService>().Close();
		}

		public string SearchTerm
		{
			get { return GetProperty(() => SearchTerm); }
			set { SetProperty(() => SearchTerm, value, () => OnSearchTermChanged(value)); }
		}

		protected override IQueryable<User> GetItemsSource()
		{
			var users = base.GetItemsSource();
			if (!string.IsNullOrWhiteSpace(SearchTerm))
			{
				users = users.Where(i => i.FirstName.Contains(SearchTerm) || i.LastName.Contains(SearchTerm));
			}
			return users;
		}

		private void OnSearchTermChanged(string value)
		{
			ItemsSource = GetItemsSource();
		}
	}
}