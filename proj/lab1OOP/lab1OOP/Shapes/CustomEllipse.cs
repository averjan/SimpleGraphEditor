using lab1OOP.Shapes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace lab1OOP
{
    [Serializable]
    [XmlInclude(typeof(CustomEllipse))]
    public class CustomEllipse : SymmetricShape
    {
        public CustomEllipse() : base()
        {
        }

        public CustomEllipse(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
        }

        public override void Draw(Canvas canvas, Brush fill, Brush contour)
        {
            Ellipse newShape;
            if (CanvasIndex == ShapeIsNotOnCavas)
            {
                newShape = new Ellipse
                {
                    Fill = fill,
                    Stroke = contour
                };
                CanvasIndex = canvas.Children.Count;
                canvas.Children.Add(newShape);
            }

            newShape = (Ellipse)canvas.Children[CanvasIndex];
            newShape.Height = Math.Abs(Y0 - Y1);
            newShape.Width = Math.Abs(X0 - X1);
            Canvas.SetLeft(newShape, X0);
            Canvas.SetTop(newShape, Y0);
            RotateTransform rotateTransform = new RotateTransform(Angle)
            {
                CenterX = (X1 + X0) / 2,
                CenterY = (Y1 + Y0) / 2
            };
            newShape.RenderTransform = rotateTransform;
        }

        public override bool IncludePoint(Point point)
        {
            double radX2 = Math.Pow(Math.Abs(X0 - X1) / 2, 2);
            double radY2 = Math.Pow(Math.Abs(Y0 - Y1) / 2, 2);
            double centerX = (X1 + X0) / 2;
            double centerY = (Y1 + Y0) / 2;
            return (Math.Pow(point.X - centerX, 2) * radY2)
                + (Math.Pow(point.Y - centerY, 2) * radX2) <= radX2 * radY2;
        }
    }
}
