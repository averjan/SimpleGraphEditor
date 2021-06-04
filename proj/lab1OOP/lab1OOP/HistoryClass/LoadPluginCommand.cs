using lab1OOP.PluginManagerClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace lab1OOP.HistoryClass
{
    class LoadPluginCommand : CommandItem
    {
        public LoadPluginCommand(MainWindow mw)
        {
            mainWindow = mw;
        }

        public override void Redo()
        {
            if (!mainWindow.dialogService.LoadPluginDialog())
            {
                return;
            }

            var dll = Assembly.LoadFile(mainWindow.dialogService.FilePath);
            var types = dll.GetExportedTypes();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                if (instance is IShapePlugin plugin)
                {
                    var rb = new RadioButton
                    {
                        Content = plugin.Name
                    };
                    rb.Checked += delegate
                    {
                        mainWindow.Drawer.Figure = plugin.CreateInstance();
                    };

                    mainWindow.SPFigurePanel.Children.Add(rb);
                }
            }
        }
    }
}
