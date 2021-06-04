using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace lab1OOP.Shapes
{
    [Serializable]
    public class CustomHouse : ComplexShape
    {
        public CustomHouse() : base()
        {
            InternalShapes = new List<CustomShape>
            {
                new CustomTriangle(),
                new CustomRectangle(),
                new CustomRectangle(),
                new CustomRectangle(),
                new CustomRectangle()
            }.ToArray();
        }

        public CustomHouse(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
            InternalShapes = new List<CustomShape>
            {
                new CustomTriangle(),
                new CustomRectangle(),
                new CustomRectangle(),
                new CustomRectangle(),
                new CustomRectangle()
            }.ToArray();
            Resize(x0, y0, x1, y1);
        }

        public override void Resize(double x0, double y0, double x1, double y1)
        {
            base.Resize(x0, y0, x1, y1);
            double width = X1 - X0;
            double height = Y1 - Y0;
            InternalShapes[0].Resize(X0, Y0, X1, (height * 0.4) + Y0);
            InternalShapes[1].Resize(
                X0 + (width * 0.6),
                Y0,
                X0 + (width * 0.8),
                Y0 + (height * 0.3));
            InternalShapes[2].Resize(
                X0 + (width * 0.1),
                Y0 + (0.4 * height),
                X0 + (width * 0.9),
                Y1);
            InternalShapes[3].Resize(
                X0 + (width * 0.2),
                Y0 + (height * 0.5),
                X0 + (width * 0.4),
                Y0 + (height * 0.8));
            InternalShapes[4].Resize(
                X0 + (width * 0.6),
                Y0 + (height * 0.5),
                X0 + (width * 0.8),
                Y1);
        }

        public override void Draw(Canvas canvas, Brush fill, Brush contour)
        {
            if (CanvasIndex == ShapeIsNotOnCavas)
            {
                CanvasIndex = canvas.Children.Count;
            }

            InternalShapes[0].Draw(canvas, fill, contour);
            InternalShapes[1].Draw(canvas, fill, contour);
            InternalShapes[2].Draw(canvas, fill, contour);
            InternalShapes[3].Draw(
                canvas,
                new SolidColorBrush(Colors.Red),
                contour);
            InternalShapes[4].Draw(
                canvas,
                new SolidColorBrush(Colors.Red),
                contour);
        }
    }
}
