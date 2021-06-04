using lab1OOP.DrawerClass;
using System;
using System.IO;
//using System.Text.Json;
using Newtonsoft.Json;

namespace lab1OOP.SerializeClass
{
    class JsonCustomCanvasSerializer : ICustomCanvasSerializer
    {
        public CustomCanvas Deserialize(string path)
        {
            CustomCanvas data = null;
            StreamReader fs;
            try
            {
                using (fs = new StreamReader(path))
                {
                    string doc = fs.ReadToEnd();
                    var options = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    };
                    data = (CustomCanvas)JsonConvert.DeserializeObject(doc, options);
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
            var fs = new FileStream(path, FileMode.Create);
            var f = new StreamWriter(fs);
            var options = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(canvas, options);
            f.Write(json);
            f.Close();
            fs.Close();
            try
            {
            }
            catch
            {
                Console.Write("hi");
            }
            finally
            {
            }
        }
    }
}
