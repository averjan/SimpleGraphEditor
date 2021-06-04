using lab1OOP.SerializeClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1OOP.Creator
{
    class SerializerCreator
    {
        public ICustomCanvasSerializer CreateSerializer(string extension)
        {
            switch (extension.ToLower())
            {
                case ".dat":
                    {
                        return new BinaryCustomCanvasSerializer();
                    }
                case ".xml":
                    {
                        return new XmlCustomCanvasSerializer();
                    }
                case ".cus":
                    {
                        return new FormatCustomCanvasSerializer();
                    }
                case ".json":
                    {
                        return new JsonCustomCanvasSerializer();
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
