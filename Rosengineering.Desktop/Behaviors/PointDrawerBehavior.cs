using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DevExpress.Mvvm.UI.Interactivity;
using Rosengineering.Desktop.Drawer;

namespace Rosengineering.Desktop.Behaviors
{
	public class PointDrawerBehavior:Behavior<Image>
	{
		private DrawerAdorner _adorner;
		protected override void OnAttached()
		{
			base.OnAttached();
			_adorner = new DrawerAdorner(AssociatedObject) {IsHitTestVisible = false};
			var layer = AdornerLayer.GetAdornerLayer(AssociatedObject);
			layer.Add(_adorner);
			AssociatedObject.MouseUp += AssociatedObjectOnMouseUp;
			AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
			AssociatedObject.MouseDown += AssociatedObjectOnMouseDown;
		}

		private void AssociatedObjectOnMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && e.RightButton == MouseButtonState.Released)
				_adorner.StartAddPoint(e.GetPosition(AssociatedObject));
			e.Handled = true;
		}


		private void AssociatedObjectOnMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Released)
				_adorner.EndMove(e.GetPosition(AssociatedObject));
			else if(e.LeftButton == MouseButtonState.Pressed && e.RightButton == MouseButtonState.Released)
				_adorner.Cancel();
			e.Handled = true;
		}

		private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs e)
		{
			_adorner.Moving(e.MouseDevice.GetPosition(AssociatedObject));
			e.Handled = true;
		}
	}
}