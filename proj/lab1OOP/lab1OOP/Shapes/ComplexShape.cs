using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json.Serialization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Windows;
using System.Xml.Serialization;

namespace lab1OOP.Shapes
{
    [Serializable]
    abstract public class ComplexShape : SymmetricShape
    {
        [JsonProperty]
        public CustomShape[] InternalShapes;

        public ComplexShape() : base()
        {
        }

        public ComplexShape(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
        }

        public override void RemoveFromCanvas()
        {
            base.RemoveFromCanvas();
            foreach (CustomShape el in InternalShapes)
            {
                el.RemoveFromCanvas();
            }
        }

        public override bool IncludePoint(Point point)
        {
            return InternalShapes.Any(Shape => Shape.IncludePoint(point));
        }
    }
}
