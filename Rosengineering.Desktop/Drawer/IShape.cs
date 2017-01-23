using System.Windows;
using System.Windows.Media;

namespace Rosengineering.Desktop.Drawer
{
	public interface IShape
	{
		bool Contains(Point point);

		Color Color { get; set; }

		void Draw(DrawingContext e);

		void StartMove(Point pt);

		void Move(Point newPoint);

		void EndMove(Point pt);
	}
}