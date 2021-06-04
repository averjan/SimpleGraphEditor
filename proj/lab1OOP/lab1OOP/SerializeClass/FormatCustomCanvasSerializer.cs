using lab1OOP.DrawerClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1OOP.SerializeClass
{
    class FormatCustomCanvasSerializer : ICustomCanvasSerializer
    {
        public CustomCanvas Deserialize(string path)
        {
            CustomCanvas data;
            FileStream fs;
            try
            {
                using (fs = new FileStream(path, FileMode.Open))
                {
                    var formatter = new TxtSerializer(typeof(CustomCanvas));
                    data = (CustomCanvas)formatter.Deserialize(fs);
                }
            }
            catch
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
                using (fs = new FileStream(path, FileMode.Create))
                {
                    TxtSerializer formatter = new TxtSerializer(typeof(CustomCanvas));
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
