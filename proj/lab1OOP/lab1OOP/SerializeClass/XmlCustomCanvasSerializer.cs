using lab1OOP.DrawerClass;
using lab1OOP.PluginManagerClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace lab1OOP.SerializeClass
{
    class XmlCustomCanvasSerializer : ICustomCanvasSerializer
    {
        public CustomCanvas Deserialize(string path)
        {
            CustomCanvas data = null;
            FileStream fs;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var possibleTypes = new List<Type>();

            assemblies.Where(a =>
            {
                return a.GetTypes()
                .Any(t => t.GetInterfaces().Contains(typeof(IShapePlugin)));
            }).ToList()
            .ForEach(e => possibleTypes.AddRange(e.GetExportedTypes()));

            try
            {
                using (fs = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer formatter = new XmlSerializer(
                        typeof(CustomCanvas),
                        possibleTypes.ToArray());
                    data = (CustomCanvas)formatter.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return data;
        }

        public void Serizalize(CustomCanvas canvas, string path)
        {
            FileStream fs;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var possibleTypes = new List<Type>();

            assemblies.Where(a =>
            {
                return a.GetTypes()
                .Any(t => t.GetInterfaces().Contains(typeof(IShapePlugin)));
            }).ToList()
            .ForEach(e => possibleTypes.AddRange(e.GetExportedTypes()));

            try
            {
                using (fs = new FileStream(path, FileMode.Create))
                {
                    var formatter = new XmlSerializer(
                        typeof(CustomCanvas),
                        possibleTypes.ToArray());
                    formatter.Serialize(fs, canvas);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
