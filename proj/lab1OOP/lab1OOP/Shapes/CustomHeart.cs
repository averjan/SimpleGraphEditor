using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace lab1OOP.Shapes
{
    [Serializable]
    [XmlInclude(typeof(CustomHeart))]
    public class CustomHeart : ComplexShape
    {
        public CustomHeart() : base()
        {
            InternalShapes = new List<CustomShape>
            {
                new CustomEllipse(),
                new CustomEllipse(),
                new CustomRhombus()
            }.ToArray();
        }

        public CustomHeart(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
            InternalShapes = new List<CustomShape>
            {
                new CustomEllipse(),
                new CustomEllipse(),
                new CustomRhombus()
            }.ToArray();
            Resize(x0, y0, x1, y1);
        }

        public override void Resize(double x0, double y0, double x1, double y1)
        {
            base.Resize(x0, y0, x1, y1);
            double width = X1 - X0;
            double height = Y1 - Y0;
            InternalShapes[0].Resize(
                X0 + width * (0.3 - 2 * Math.Sqrt(2) / 10),
                Y0 + height * (0.3 - 2 * Math.Sqrt(2) / 10),
                X0 + width * (0.3 + 2 * Math.Sqrt(2) / 10),
                Y0 + height * (0.3 + 2 * Math.Sqrt(2) / 10));
            InternalShapes[1].Resize(
                X0 + width * (0.7 - 2 * Math.Sqrt(2) / 10),
                Y0 + height * (0.3 - 2 * Math.Sqrt(2) / 10),
                X0 + width * (0.7 + 2 * Math.Sqrt(2) / 10),
                Y0 + height * (0.3 + 2 * Math.Sqrt(2) / 10));
            InternalShapes[2].Resize(
                X0 + (width * 0.1),
                Y0 + (height * 0.1),
                X0 + (width * 0.9),
                Y0 + (height * 0.9));
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
        }
    }
}
