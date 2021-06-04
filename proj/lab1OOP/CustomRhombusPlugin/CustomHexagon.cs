using lab1OOP.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace CustomHexagonPlugin
{
    [Serializable]
    [XmlInclude(typeof(CustomHexagon))]
    public class CustomHexagon : SymmetricShape
    {
        public CustomHexagon() : base()
        {
        }

        public CustomHexagon(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
        }

        public override void Draw(Canvas canvas, Brush fill, Brush contour)
        {
            Polygon newShape;
            if (CanvasIndex == ShapeIsNotOnCavas)
            {
                newShape = new Polygon
                {
                    Fill = fill,
                    Stroke = contour
                };

                CanvasIndex = canvas.Children.Count;
                canvas.Children.Add(newShape);
            }

            newShape = (Polygon)canvas.Children[CanvasIndex];
            newShape.Points.Clear();
            GetPolygonPoints().ForEach(p => newShape.Points.Add(p));
            RotateTransform rotateTransform = new RotateTransform(Angle)
            {
                CenterX = (X1 + X0) / 2,
                CenterY = (Y1 + Y0) / 2
            };
            newShape.RenderTransform = rotateTransform;
        }

        public override bool IncludePoint(Point point)
        {
            var pointList = GetPolygonPoints();
            bool result = false;
            int j = pointList.Count() - 1;
            for (int i = 0; i < pointList.Count(); i++)
            {
                if (pointList[i].Y < point.Y && pointList[j].Y >= point.Y ||
                    pointList[j].Y < point.Y && pointList[i].Y >= point.Y)
                {
                    if (pointList[i].X + (point.Y - pointList[i].Y) /
                        (pointList[j].Y - pointList[i].Y) *
                        (pointList[j].X - pointList[i].X) < point.X)
                    {
                        result = !result;
                    }
                }

                j = i;
            }

            return result;
        }

        private List<Point> GetPolygonPoints()
        {
            var pointList = new List<Point>();
            double midX = (X0 + X1) / 2;
            double midY = (Y0 + Y1) / 2;
            double radX = Math.Abs((X0 - X1) / 2);
            double radY = Math.Abs((Y0 - Y1) / 2);
            for (sbyte i = 0; i < 6; i++)
            {
                double angle = 2 * Math.PI * i / 6;
                pointList.Add(new Point(midX + (radX * Math.Cos(angle)),
                    midY + (radY * Math.Sin(angle))));
            }

            return pointList;
        }
    }
}
