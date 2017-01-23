using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;

namespace Rosengineering.Desktop.ViewModels
{
	public class PhotoEditorViewModel : ViewModelBase
	{
		public ICommand OpenPhotoCommand => new DelegateCommand(OnOpenPhoto);

		private void OnOpenPhoto()
		{
			var dlg = GetService<IOpenFileDialogService>();
			dlg.ShowDialog(e =>
			{
				using (var stream = dlg.File.OpenRead())
				{
					var bitmap  = new BitmapImage();
					bitmap.BeginInit();
					bitmap.StreamSource = stream;
					bitmap.CacheOption = BitmapCacheOption.OnLoad;
					bitmap.EndInit();
					bitmap.Freeze();
					Image = bitmap;
				}
			});
		}

		public ImageSource Image
		{
			get { return GetProperty(() => Image); }
			set { SetProperty(() => Image, value); }
		}
	}
}
