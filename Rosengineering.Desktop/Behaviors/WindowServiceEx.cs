using System.Windows;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Native;

namespace Rosengineering.Desktop.Behaviors
{
	public class WindowServiceEx : WindowService, IWindowServiceEx
	{
		private Size? _size;

		Size? IWindowServiceEx.Size
		{
			get { return _size; }
			set { _size = value; }
		}

		protected override IWindowSurrogate CreateWindow(object view)
		{
			var window = base.CreateWindow(view);
			if (_size != null)
			{
				window.RealWindow.SizeToContent = SizeToContent.Manual;
				window.RealWindow.Height = _size.Value.Height;
				window.RealWindow.Width = _size.Value.Width;
			}
			return window;
		}
	}
}
