using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

#pragma warning disable 1

namespace lab1OOP.Shapes
{
    [Serializable]
    public class CustomHuman : ComplexShape
    {
        public CustomHuman() : base()
        {
            InternalShapes = new List<CustomShape>
            {
                new CustomEllipse(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine()
            }.ToArray();
        }

        public CustomHuman(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
            InternalShapes = new List<CustomShape>
            {
                new CustomEllipse(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine(),
                new CustomLine()
            }.ToArray();
            Resize(x0, y0, x1, y1);
        }

        public override void Resize(double x0, double y0, double x1, double y1)
        {
            base.Resize(x0, y0, x1, y1);
            double width = X1 - X0;
            double height = Y1 - Y0;
            InternalShapes[0].Resize(
                X0 + width * 0.3,
                Y0 + height * 0.1,
                X0 + width * 0.5,
                Y0 + height * 0.3);
            InternalShapes[1].Resize(
                X0 + width * 0.4,
                Y0 + height * 0.2,
                X0 + width * 0.7,
                Y0 + height * 0.5);
            InternalShapes[2].Resize(
                X0 + width * 0.7,
                Y0 + height * 0.5,
                X0 + (width * 0.9),
                Y0 + (height * 0.9));
            InternalShapes[3].Resize(
                X0 + width * 0.7,
                Y0 + height * 0.5,
                X0 + (width * 0.5),
                Y0 + (height * 0.7));
            InternalShapes[4].Resize(
                X0 + (width * 0.5),
                Y0 + (height * 0.7),
                X0 + (width * 0.7),
                Y0 + (height * 0.9));
            InternalShapes[5].Resize(
                X0 + (width * 0.5),
                Y0 + (height * 0.3),
                X0 + (width * 0.6),
                Y0);
            InternalShapes[6].Resize(
                X0 + (width * 0.5),
                Y0 + (height * 0.3),
                X0 + (width * 0.3),
                Y0 + (height * 0.4));
            InternalShapes[7].Resize(
                X0 + (width * 0.3),
                Y0 + (height * 0.4),
                X0 + (width * 0.35),
                Y0 + (height * 0.2));
        }

        public override void Draw(Canvas canvas, Brush fill, Brush contour)
        {
            if (CanvasIndex == ShapeIsNotOnCavas)
            {
                CanvasIndex = canvas.Children.Count;
            }

            InternalShapes[0].Draw(canvas, new SolidColorBrush(Colors.White), contour);
            InternalShapes[1].Draw(canvas, fill, contour);
            InternalShapes[2].Draw(canvas, fill, contour);
            InternalShapes[3].Draw(canvas, fill, contour);
            InternalShapes[4].Draw(canvas, fill, contour);
            InternalShapes[5].Draw(canvas, fill, contour);
            InternalShapes[6].Draw(canvas, fill, contour);
            InternalShapes[7].Draw(canvas, fill, contour);
        }
    }
}
