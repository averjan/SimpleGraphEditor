using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace lab1OOP.SerializeClass
{
    internal class PluginNotFoundException : Exception
    {
        public PluginNotFoundException(string typeName, string assemblyName)
            : base(string.Format("Plugin not found: {0}", typeName))
        {
        }
    }

    public class CustomBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type type = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var currentAssembly in assemblies)
            {
                string assemblyShort = currentAssembly.GetName().Name;
                if (assemblyName == currentAssembly.FullName
                    || assemblyName == assemblyShort)
                {
                    type = currentAssembly.GetType(typeName);
                    break;
                }
            }

            if (type == null)
            {
                throw new PluginNotFoundException(typeName, assemblyName);
            }

            return type;
        }
    }
}
