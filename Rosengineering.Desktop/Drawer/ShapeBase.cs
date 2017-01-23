using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

namespace Rosengineering.Desktop.Drawer
{
	public abstract class ShapeBase : IShape
	{
		private Color _color;

		public bool Contains(Point point)
		{
			return GetArea().Contains(point);
		}

		protected abstract Rect GetArea();

		public Color Color
		{
			get { return _color; }
			set
			{
				if (_color != value)
				{
					_color = value;
					OnColorChanged(value);
				}
			}
		}

		protected virtual void OnColorChanged(Color color)
		{
			
		}

		public abstract void Draw(DrawingContext e);

		

		public abstract void Move(Point newPoint);
		public abstract void StartMove(Point pt);


		public abstract void EndMove(Point pt);

	}
}
