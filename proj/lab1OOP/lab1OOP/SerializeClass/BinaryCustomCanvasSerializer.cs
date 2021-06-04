using lab1OOP.DrawerClass;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab1OOP.SerializeClass
{
    class BinaryCustomCanvasSerializer : ICustomCanvasSerializer
    {
        public CustomCanvas Deserialize(string path)
        {
            CustomCanvas data = null;
            FileStream fs;
            try
            {
                using (fs = new FileStream(path, FileMode.Open))
                {
                    var formatter = new BinaryFormatter
                    {
                        Binder = new CustomBinder()
                    };
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
            try
            {
                using (fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
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
