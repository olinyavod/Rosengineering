using System.Windows;
using System.Windows.Media;

namespace Rosengineering.Desktop.Drawer
{
	public class PointShape : ShapeBase
	{
		private Rect _rect;
		private SolidColorBrush _brush;
		private Pen _pen;

		public PointShape()
		{
			_rect = new Rect(0, 0, 10, 10);
			Color = Colors.Red;
			_brush = new SolidColorBrush(Color);
			_pen =new Pen(_brush, 0);
		}

		protected override Rect GetArea()
		{
			return _rect;
		}

		protected override void OnColorChanged(Color color)
		{
			base.OnColorChanged(color);
			_brush = new SolidColorBrush(Color);
			_pen = new Pen(_brush, 0);
		}

		public override void Draw(DrawingContext e)
		{
			e.DrawEllipse(_brush, _pen, new Point(_rect.X, _rect.Y), _rect.Width, _rect.Height);
		}

		public override void StartMove(Point pt)
		{
			_rect.Location = pt;
			var c = Color;
			c.A = 150;
			Color = c;
		}

		public override void EndMove(Point pt)
		{
			_rect.Location = pt;
			var c = Color;
			c.A = 255;
			Color = c;
		}

		public override void Move(Point newPoint)
		{
			_rect.Location = newPoint;
		}
	}
}