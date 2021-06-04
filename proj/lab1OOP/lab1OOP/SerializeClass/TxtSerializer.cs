using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using lab1OOP.DrawerClass;

namespace lab1OOP.SerializeClass
{
    class TxtSerializer : IFormatter
    {
        public ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SerializationBinder Binder { get ; set; }
        public StreamingContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private readonly Type WorkClass;

        public TxtSerializer(Type type)
        {
            WorkClass = type;
            Binder = new CustomBinder();
        }

        public object Deserialize(Stream serializationStream)
        {
            return Deserialize(serializationStream, WorkClass);
        }

        private object Deserialize(Stream serializationStream, Type type)
        {
            ReadLine(serializationStream); // <\n
            var typeString = ReadLine(serializationStream); // [Type=...]
            Match mType = Regex.Match(typeString, @"\[(.+)=(.+)\]");
            string typeName = mType.Groups[2].Value;
            string assemblyName = typeName.Split('.')[0].Trim();
            type = Binder.BindToType(assemblyName, typeName);
            var instance = Activator.CreateInstance(type);
            DeserializeFields(serializationStream, instance);
            DeserializeProperties(serializationStream, instance);
            ReadLine(serializationStream);
            CallDeserializtionCallbacks(instance);
            return instance;
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            var type = graph.GetType();
            WriteBytes(serializationStream, "<\n");
            if (SerializeByValue(serializationStream, graph))
            {
                return;
            }

            WriteBytes(serializationStream, @"[" + "Type" + " = " + type.ToString() + "]\n");
            SerializeFields(serializationStream, graph);
            SerializeProperties(serializationStream, graph);
            WriteBytes(serializationStream, ">\n");
        }

        private void CallDeserializtionCallbacks(object instance)
        {
            var type = instance.GetType();
            var callBackMethods = type.GetMethods(
                BindingFlags.NonPublic
                | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(
                    typeof(OnDeserializedAttribute),
                    false).Count() > 0).ToList();
            callBackMethods.ForEach((m) =>
            {
                m.Invoke(instance, new object[] { new StreamingContext() });
            });

        }

        private void DeserializeProperties(Stream serializationStream, object instance)
        {
            var type = instance.GetType();
            IList<PropertyInfo> props2 = new List<PropertyInfo>(type.GetProperties());
            foreach (var prop in props2)
            {
                var propType = prop.PropertyType;
                if (propType.IsPrimitive || propType == typeof(Decimal) || propType == typeof(String))
                {
                    string nameString = ReadLine(serializationStream); // [Name=Value]
                    string typeString = ReadLine(serializationStream); // [Type=TypeName]
                    Match m = Regex.Match(nameString, @"\[(.+)=(.+)\]");
                    var converter = TypeDescriptor.GetConverter(propType);
                    var res = converter.ConvertFrom(m.Groups[2].Value);
                    MethodInfo setMethod = prop.GetSetMethod();
                    if (setMethod != null)
                    {
                        prop.SetValue(instance, res);
                    }
                }
            }
        }

        private void DeserializeFields(Stream serializationStream, object instance)
        {
            var type = instance.GetType();
            IList<FieldInfo> props = new List<FieldInfo>(
                type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public));
            foreach (var prop in props)
            {
                DeserializeField(serializationStream, instance, prop);
            }
        }

        private void DeserializeField(Stream serializationStream, object instance, FieldInfo prop)
        {
            Type propType = prop.FieldType;
            if (propType.IsPrimitive || propType == typeof(Decimal) || propType == typeof(String))
            {
                object res = DeserializePrimitive(serializationStream, propType);
                prop.SetValue(instance, res);
            }
            else if (!prop.IsNotSerialized)
            {
                if (typeof(IEnumerable<object>).IsAssignableFrom(propType))
                {
                    object list = DeserializeCollection(serializationStream, propType);
                    prop.SetValue(instance, list);
                }
                else
                {
                    prop.SetValue(instance, Deserialize(serializationStream, prop.FieldType));
                }
            }
        }

        private object DeserializePrimitive(Stream serializationStream, Type type)
        {
            string nameString = ReadLine(serializationStream); // [Name=Value]
            string typeString = ReadLine(serializationStream); // [Type=TypeName]
            Match m = Regex.Match(nameString, @"\[(.+)=(.+)\]");
            var converter = TypeDescriptor.GetConverter(type);
            var res = converter.ConvertFrom(m.Groups[2].Value);
            return res;
        }

        private object DeserializeCollection(Stream serializationStream, Type type)
        {
            Type itemType = GetCollectionElementType(type);
            var listProp = new List<object>();
            ReadLine(serializationStream); // <\n
            string nameString = ReadLine(serializationStream); // [Name=Value]
            string typeString = ReadLine(serializationStream); // [Type=TypeName]
            while (NextLine(serializationStream) != ">\n")
            {
                listProp.Add(Deserialize(serializationStream, itemType));
            }

            ReadLine(serializationStream); // >\n
            Array items = Array.CreateInstance(itemType, listProp.Count);
            for (int i = 0; i < listProp.Count; i++)
            {
                items.SetValue(listProp[i], i);
            }

            object list = GetCollectionInstance(type, items);
            return list;
        }

        private bool SerializeByValue(Stream serializationStream, object graph)
        {
            var type = graph.GetType();
            if (type.IsPrimitive || type == typeof(decimal)
                || type == typeof(string))
            {
                SerializePrimitiveMember(
                    serializationStream,
                    graph,
                    type);
            }
            else if (typeof(IEnumerable<object>).IsAssignableFrom(type))
            {
                SerializeCollectionMember(serializationStream, graph, graph.GetType());
            }
            else
            {
                return false;
            }

            return true;
        }

        private void SerializeProperties(Stream serializationStream, object graph)
        {
            var type = graph.GetType();
            IList<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());
            foreach (var prop in properties)
            {
                var propType = prop.GetValue(graph).GetType();
                if (propType.IsPrimitive || propType == typeof(Decimal) || propType == typeof(String))
                {
                    var propValue = prop.GetValue(graph);
                    SerializePrimitiveMember(serializationStream, propValue, propType, prop.Name);
                }
            }
        }

        private void SerializeFields(Stream serializationStream, object graph)
        {
            var type = graph.GetType();
            IList<FieldInfo> fields = new List<FieldInfo>(
                type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));

            foreach (var prop in fields)
            {
                SerializeField(serializationStream, graph, prop);
            }
        }

        private void SerializeField(Stream serializationStream, object graph, FieldInfo prop)
        {
            var propType = prop.GetValue(graph).GetType();
            if (!prop.IsNotSerialized)
            {
                if (propType.IsPrimitive || propType == typeof(decimal)
                || propType == typeof(string))
                {
                    SerializePrimitiveMember(
                        serializationStream,
                        prop.GetValue(graph),
                        prop.FieldType,
                        prop.Name);
                }
                else if (typeof(IEnumerable<object>).IsAssignableFrom(propType))
                {
                    SerializeCollectionMember(
                        serializationStream,
                        prop.GetValue(graph),
                        prop.FieldType,
                        prop.Name);
                }
                else
                {
                    Serialize(serializationStream, prop.GetValue(graph));
                }
            }
        }

        private void SerializePrimitiveMember(Stream serializationStream,
            object value, Type type, string name = null)
        {
            if (name == null)
            {
                name = "Value";
            }

            WriteBytes(serializationStream, @"[" + name + " = " + value + "]\n");
            WriteBytes(serializationStream, @"[" + "Type" + " = " + type.ToString() + "]\n");
        }

        private void SerializeCollectionMember(Stream serializationStream,
            object value, Type type, string name = null)
        {
            if (name == null)
            {
                name = "Value";
            }

            var listValue = value as IEnumerable<object>;
            WriteBytes(serializationStream, "<\n");
            WriteBytes(serializationStream, @"[" + name + " = " + value + "]\n");
            WriteBytes(serializationStream, @"[" + "Type" + " = " + type.ToString() + "]\n");
            foreach (var el in listValue)
            {
                Serialize(serializationStream, el);
            }

            WriteBytes(serializationStream, ">\n");
        }

        private void WriteBytes(Stream fs, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            fs.Write(bytes, 0, bytes.Length);
        }

        private string ReadLine(Stream fs)
        {
            string line = "";
            try
            {
                int buffer = 0;
                while ((fs.CanRead) && (buffer != Encoding.UTF8.GetBytes("\n")[0]))
                {
                    buffer = fs.ReadByte();
                    line += Encoding.Default.GetString(new byte[] { (byte)buffer });
                }

                return line;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string NextLine(Stream fs)
        {
            string line = ReadLine(fs);
            fs.Position -= line.Length;
            return line;
        }

        private Type GetCollectionElementType(Type type)
        {
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>)))
            {
                return type.GetGenericArguments()[0];
            }

            if (type.IsArray)
            {
                return type.GetElementType();
            }

            return null;
        }

        private object GetCollectionInstance(Type targetType, Array items)
        {
            if (targetType.IsArray)
            {
                return items;
            }
            else if (targetType.IsGenericType)
            {
                return Activator.CreateInstance(targetType, new object[] { items });
            }

            return null;
        }
    }
}
