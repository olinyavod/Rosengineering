using Rosengineering.DAL.Models;

namespace Rosengineering.Desktop.ViewModels
{
	public class UserGroupEditorViewModel : ModelEditorViewModelBase<UserGroup>
	{
		public string Title
		{
			get { return GetProperty(() => Title); }
			set { SetProperty(() => Title, value); }
		}
	}
}
