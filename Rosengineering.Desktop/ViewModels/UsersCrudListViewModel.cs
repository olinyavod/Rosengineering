using Rosengineering.DAL.Models;
using Rosengineering.Desktop.Properties;
using Rosengineering.Desktop.Views;

namespace Rosengineering.Desktop.ViewModels
{
	public class UsersCrudListViewModel : CrudListViewModelBase<User>
	{
		public UsersCrudListViewModel() 
			: base(new CrudListConfig(Resources.ttlUsers, typeof(UserView).Name))
		{
		}
	}
}