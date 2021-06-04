using lab1OOP.Shapes;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace lab1OOP.DrawerClass
{
    [Serializable]
    [XmlInclude(typeof(Drawer))]
    public class Drawer : IDeserializationCallback
    {
        [JsonIgnore]
        [XmlIgnore]
        [NonSerialized]
        public CustomCanvas CustomCanvas;

        [NonSerialized]
        [XmlElement]
        public Brush fill;

        [NonSerialized]
        [XmlElement]
        public Brush contour;

        [JsonInclude]
        public string fillString;

        [JsonInclude]
        public string contourString;

        [JsonInclude]
        public CustomShape Figure;

        // The point from which user stretches new shape when create
        [XmlIgnore]
        [field: NonSerialized]
        public Point BasePoint { get; set; }

        public Drawer()
        {
        }

        public Drawer(CustomCanvas canvas, CustomShape figure)
        {
            CustomCanvas = canvas;
            Figure = figure;
        }

        [XmlIgnore]
        [JsonIgnore]
        public Brush Fill
        {
            get { return fill; }
            set
            {
                fill = value;
                var converter = new BrushConverter();
                fillString = converter.ConvertToString(fill);
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public Brush Contour
        {
            get { return contour; }
            set
            {
                contour = value;
                var converter = new BrushConverter();
                contourString = converter.ConvertToString(contour);
            }
        }

        public void Show()
        {
            Figure.Draw(CustomCanvas.Canvas, Fill, Contour);
        }

        public void EditShape(Point p)
        {
            double x0, y0, x1, y1;
            x0 = BasePoint.X;
            y0 = BasePoint.Y;

            // If Shift pressed then sides changed evenly
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                // Sign of yOffset and xOffset
                // define the direction of mouse movement
                double yOffset = p.Y - BasePoint.Y;
                double xOffset = p.X - BasePoint.X;
                double max = Math.Abs(Math.Max(yOffset, xOffset));
                y1 = BasePoint.Y +
                    (Math.Sign(yOffset) * max);
                x1 = BasePoint.X +
                    (Math.Sign(xOffset) * max);
            }
            else
            {
                x1 = p.X;
                y1 = p.Y;
            }

            Figure.Resize(x0, y0, x1, y1);
            Show();
        }

        public void Errase()
        {
            Figure.RemoveFromCanvas();
        }

        [OnDeserialized]
        private void SetColorValues(StreamingContext stream)
        {
            var converter = new BrushConverter();
            fill = (Brush)converter.ConvertFrom(fillString);
            contour = (Brush)converter.ConvertFrom(contourString);
        }

        public void OnDeserialization(object sender)
        {
            var converter = new BrushConverter();
            fill = (Brush)converter.ConvertFrom(fillString);
            contour = (Brush)converter.ConvertFrom(contourString);
        }
    }
}
