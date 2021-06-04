using System;

namespace lab1OOP.Shapes
{
    [Serializable]
    public abstract class SymmetricShape : CustomShape
    {
        public SymmetricShape() : base()
        {
        }

        public SymmetricShape(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
        }

        public override void Resize(double x0, double y0, double x1, double y1)
        {
            X0 = Math.Min(x0, x1);
            X1 = Math.Max(x0, x1);
            Y0 = Math.Min(y0, y1);
            Y1 = Math.Max(y0, y1);
        }
    }
}
