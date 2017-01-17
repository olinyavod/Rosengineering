using System.Linq;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Properties;
using Rosengineering.Desktop.Views;

namespace Rosengineering.Desktop.ViewModels
{
	public class UserGroupsListViewModel : CrudListViewModelBase<UserGroup>
	{
		public UserGroupsListViewModel() 
			: base(new CrudListConfig(Resources.ttlUserGroups, typeof(UserGroupEditorView).Name)
		{
			AddTitle = Resources.ttlAdUserGroup,
			DetailsTitle = Resources.ttlEditUserGroup
		})
		{
		}

		public string SearchTerm
		{
			get { return GetProperty(() => SearchTerm); }
			set { SetProperty(() => SearchTerm, value, () => OnSearchTermChanged(value)); }
		}

		protected override IQueryable<UserGroup> GetItemsSource()
		{
			var users = base.GetItemsSource();
			if (!string.IsNullOrWhiteSpace(SearchTerm))
			{
				users = users.Where(i => i.Title.Contains(SearchTerm));
			}
			return users;
		}

		private void OnSearchTermChanged(string value)
		{
			ItemsSource = GetItemsSource();
		}

		protected override string GetDeleteMessage(UserGroup item)
		{
			return string.Format(Resources.msgDeleteItem, item.Title);
		}
	}
}
