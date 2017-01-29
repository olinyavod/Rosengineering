using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Rosengineering.Desktop.Drawer
{
	public class DrawerAdorner : Adorner
	{
		public DrawerAdorner(UIElement adornedElement) : base(adornedElement)
		{
			_shapes = new List<IShape>();
		}

		private readonly IList<IShape> _shapes;

		protected override void OnRender(DrawingContext e)
		{
			base.OnRender(e);
			foreach (var shape in _shapes)
			{
				shape.Draw(e);
			}
		}

		private IShape _currentShape;

		public void StartAddPoint(Point pt)
		{
			_currentShape = new PointShape();
			_currentShape.StartMove(pt);
			_shapes.Add(_currentShape);
			InvalidateVisual();
		}

		public void Moving(Point pt)
		{
			_currentShape?.Move(pt);
			InvalidateVisual();
		}

		public void EndMove(Point pt)
		{
			_currentShape?.EndMove(pt);
			_currentShape = null;
			InvalidateVisual();
		}

		public void Cancel()
		{
			_shapes.Remove(_currentShape);
			_currentShape = null;
			InvalidateVisual();
		}

		public void SetCurrent(IShape shape, Point pt)
		{
			_currentShape = shape;
			_currentShape.StartMove(pt);
		}

		public IShape GetShape(Point pt)
		{
			foreach (var shape in _shapes)
			{
				if (shape.Contains(pt))
					return shape;
			}
			return null;
		}
	}
}