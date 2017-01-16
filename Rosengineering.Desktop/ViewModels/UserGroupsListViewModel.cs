using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Properties;
using Rosengineering.Desktop.Views;

namespace Rosengineering.Desktop.ViewModels
{
	public class UserGroupsListViewModel : CrudListViewModelBase<UserGroup>
	{
		public UserGroupsListViewModel() : base(new CrudListConfig(Resources.ttlUserGroups, typeof(UserGroupEditorView).Name)
		{
			AddTitle = Resources.ttlAdUserGroup,
			DetailsTitle = Resources.ttlEditUserGroup
		})
		{
		}
	}
}
