using lab1OOP.Shapes;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace lab1OOP
{
    [Serializable]
    [XmlInclude(typeof(CustomShape)), XmlInclude(typeof(CustomRectangle)),
        XmlInclude(typeof(CustomEllipse)), /*XmlInclude(typeof(CustomHexagon)),*/
        XmlInclude(typeof(CustomLine)), XmlInclude(typeof(CustomRhombus)),
        XmlInclude(typeof(CustomHeart)), XmlInclude(typeof(CustomHouse)),
        XmlInclude(typeof(CustomHuman)), XmlInclude(typeof(CustomTriangle)),
        XmlInclude(typeof(ComplexShape)), XmlInclude(typeof(SymmetricShape)),
        XmlInclude(typeof(AsymmetricShape))]
    [XmlInclude(typeof(SolidColorBrush)), XmlInclude(typeof(Brush)), XmlInclude(typeof(MatrixTransform))]
    public abstract class CustomShape
    {
        [JsonInclude]
        public const int ShapeIsNotOnCavas = -1;

        // System.Windows.Controls Canvas uses Children collection property
        // to store all drawn Shapes.
        // CanvasIndex defines index of the object in this collection
        [JsonInclude]
        public virtual int CanvasIndex { get; set; }

        // X0, Y0 define the coordinates of the top left drawing corner
        [JsonInclude]
        public virtual double X0 { get; set; }

        [JsonInclude]
        public virtual double Y0 { get; set; }

        // X1, Y1 define the coordinates of the bottom right corner
        [JsonInclude]
        public virtual double X1 { get; set; }

        [JsonInclude]
        public virtual double Y1 { get; set; }

        // Rotation angle of a shape
        [JsonInclude]
        public int Angle = 0;

        public CustomShape(double x0, double y0, double x1, double y1)
        {
            X0 = x0;
            Y0 = y0;
            X1 = x1;
            Y1 = y1;
            CanvasIndex = ShapeIsNotOnCavas;
        }

        [JsonConstructor]
        public CustomShape()
        {
            CanvasIndex = ShapeIsNotOnCavas;
        }

        public abstract void Draw(Canvas canvas, Brush fill, Brush contour);

        public abstract void Resize(double x0, double y0, double x1, double y1);

        public abstract bool IncludePoint(Point point);

        public virtual void RemoveFromCanvas()
        {
            CanvasIndex = ShapeIsNotOnCavas;
        }
    }
}