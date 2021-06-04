using lab1OOP.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1OOP.Creator
{
    class ShapeCreator
    {
        public CustomShape CreateShape(string description)
        {
            switch (description)
            {
                case "ButtonRectangle":
                    return new CustomRectangle();
                case "ButtonLine":
                    return new CustomLine();
                case "ButtonEllipse":
                    return new CustomEllipse();
                    /*
                case "ButtonHexagon":
                    return new CustomHexagon();
                    */
                case "ButtonRhombus":
                    return new CustomRhombus();
                case "ButtonTriangle":
                    return new CustomTriangle();
                case "ButtonHouse":
                    return new CustomHouse();
                case "ButtonHeart":
                    return new CustomHeart();
                case "ButtonHuman":
                    return new CustomHuman();
                default:
                    return null;
            }
        }
    }
}
