using System;

namespace lab1OOP.Shapes
{
    [Serializable]
    public abstract class AsymmetricShape : CustomShape
    {
        public AsymmetricShape() : base()
        {
        }

        public AsymmetricShape(double x0, double y0, double x1, double y1)
            : base(x0, y0, x1, y1)
        {
        }

        public override void Resize(double x0, double y0, double x1, double y1)
        {
            X0 = x0;
            Y0 = y0;
            X1 = x1;
            Y1 = y1;
        }
    }
}
