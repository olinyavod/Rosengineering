using System.Linq;
using System.Windows;
using System.Windows.Data;
using DevExpress.Mvvm.UI.Interactivity;
using Rosengineering.Desktop.Messages;
using Xceed.Wpf.DataGrid;

namespace Rosengineering.Desktop.Behaviors
{
	public class DataGridControlItemsSourceBehavior : Behavior<DataGridControl>
	{
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
			"ItemsSource", typeof(object), typeof(DataGridControlItemsSourceBehavior), new PropertyMetadata(default(object), ItemsSourcePropertyChangedCallback));

		private static void ItemsSourcePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control = (DataGridControlItemsSourceBehavior) sender;
			var items = e.NewValue as IQueryable;
			control.AssociatedObject.ItemsSource = items != null ? new DataGridCollectionView(items) : null;
		}


		public object ItemsSource
		{
			get { return (object) GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public static readonly DependencyProperty RefreshDataMessageProperty = DependencyProperty.Register(
			"RefreshDataMessage", typeof(RefreshDataMessage), typeof(DataGridControlItemsSourceBehavior), new PropertyMetadata(default(RefreshDataMessage), RefreshDataMessagePropertyChangedCallback));

		private static void RefreshDataMessagePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control = (DataGridControlItemsSourceBehavior) sender;
			var collectionView = control.AssociatedObject.ItemsSource as CollectionView;
			if (collectionView != null)
				collectionView.Refresh();
		}

		public RefreshDataMessage RefreshDataMessage
		{
			get { return (RefreshDataMessage) GetValue(RefreshDataMessageProperty); }
			set { SetValue(RefreshDataMessageProperty, value); }
		}
	}
}