using lab1OOP.Shapes;
using System;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace lab1OOP
{
    [Serializable]
    [XmlInclude(typeof(CustomRectangle))]
    public class CustomRectangle : SymmetricShape
    {
        [JsonConstructor]
        public CustomRectangle() : base()
        {
        }

        public CustomRectangle(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
        }

        public override void Draw(Canvas canvas, Brush fill, Brush contour)
        {
            Rectangle newShape;
            if (CanvasIndex == ShapeIsNotOnCavas)
            {
                newShape = new Rectangle
                {
                    Fill = fill
                };
                CanvasIndex = canvas.Children.Count;
                canvas.Children.Add(newShape);
            }

            newShape = (Rectangle)canvas.Children[CanvasIndex];
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
            return (point.X <= X1) && (point.X >= X0) &&
                (point.Y <= Y1) && (point.Y >= Y0);
        }
    }
}
