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
			_rect = new Rect(0, 0, 15, 15);
			Color = Colors.Red;
			_brush = new SolidColorBrush(Color);
			_pen =new Pen(Brushes.Red, 0);
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
			e.DrawEllipse(_brush, _pen, new Point(_rect.X+_rect.Width/2, _rect.Y+_rect.Height/2), _rect.Width/2, _rect.Height/2);
		}

		public override void StartMove(Point pt)
		{
			_rect.Location = new Point(pt.X - _rect.Width / 2, pt.Y - _rect.Height / 2);
			var c = Color;
			c.A = 150;
			Color = c;
		}

		public override void EndMove(Point pt)
		{
			_rect.Location = new Point(pt.X - _rect.Width / 2, pt.Y - _rect.Height / 2);
			var c = Color;
			c.A = 255;
			Color = c;
		}

		public override void Move(Point pt)
		{
			_rect.Location = new Point(pt.X - _rect.Width / 2, pt.Y - _rect.Height / 2);
		}
	}
}