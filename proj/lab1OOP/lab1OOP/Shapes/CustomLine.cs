using lab1OOP.Shapes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab1OOP
{
    [Serializable]
    public class CustomLine : AsymmetricShape
    {
        public CustomLine() : base()
        {
        }

        public CustomLine(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
        }

        public override void Draw(Canvas canvas, Brush fill, Brush contour)
        {
            Line newShape;
            if (CanvasIndex == ShapeIsNotOnCavas)
            {
                newShape = new Line
                {
                    StrokeThickness = 10,
                    Stroke = contour
                };
                CanvasIndex = canvas.Children.Count;
                canvas.Children.Add(newShape);
            }

            newShape = (Line)canvas.Children[CanvasIndex];
            newShape.X1 = X0;
            newShape.Y1 = Y0;
            newShape.X2 = X1;
            newShape.Y2 = Y1;
        }

        public override bool IncludePoint(Point point)
        {
            double originalLine = Distance(
                new Point(X0, Y0),
                new Point(X1, Y1));
            double lineTo = Distance(new Point(X0, Y0), point);
            double lineFrom = Distance(point, new Point(X1, Y1));
            return Math.Round(originalLine, 2) ==
                Math.Round(lineTo + lineFrom, 2);
        }

        private double Distance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
