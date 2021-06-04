using lab1OOP.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab1OOP
{
    [Serializable]
    public class CustomRhombus : SymmetricShape
    {
        public CustomRhombus() : base()
        {
        }

        public CustomRhombus(double x0, double y0, double x1, double y1)
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
            double midX = (X0 + X1) / 2;
            double midY = (Y0 + Y1) / 2;
            var pointList = new List<Point>
            {
                new Point(midX, Y0),
                new Point(X1, midY),
                new Point(midX, Y1),
                new Point(X0, midY)
            };

            return pointList;
        }
    }
}
