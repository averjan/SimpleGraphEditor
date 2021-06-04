using lab1OOP;
using lab1OOP.PluginManagerClass;
using System;

namespace CustomHexagonPlugin
{
    public class CustomHexagonCommand : IShapePlugin
    {
        public string Name { get { return "Hexagon"; } }

        public CustomShape CreateInstance()
        {
            return new CustomHexagon();
        }
    }
}
