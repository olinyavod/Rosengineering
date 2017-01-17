using System.Windows;
using DevExpress.Mvvm;

namespace Rosengineering.Desktop.Behaviors
{
	public interface IWindowServiceEx :IWindowService
	{
		Size? Size { get; set; }
	}
}