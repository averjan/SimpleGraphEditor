using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace lab1OOP.DrawerClass
{
    [Serializable]
    [XmlInclude(typeof(Layer))]
    public class Layer
    {
        [JsonIgnore]
        [XmlIgnore]
        public int Count { get { return DrawerList.Count; } set { } }

        [JsonInclude]
        public List<Drawer> DrawerList;

        public string Name = null;

        public Layer()
        {
            DrawerList = new List<Drawer>();
        }
    }
}
