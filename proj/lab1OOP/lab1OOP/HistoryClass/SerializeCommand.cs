using lab1OOP.Creator;
using lab1OOP.DrawerClass;
using lab1OOP.SerializeClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1OOP.HistoryClass
{
    class SerializeCommand : CommandItem
    {
        readonly CustomCanvas canvas;

        public SerializeCommand(MainWindow mw, CustomCanvas canvas)
        {
            mainWindow = mw;
            this.canvas = canvas;
        }

        public override void Redo()
        {
            if (!mainWindow.dialogService.SaveFileDialog())
            {
                return;
            }

            SerializerCreator serializerCreator = new SerializerCreator();
            string path = mainWindow.dialogService.FilePath;
            ICustomCanvasSerializer serializer =
                    serializerCreator.CreateSerializer(
                        mainWindow.dialogService.FileExtension);
            try
            {
                serializer.Serizalize(mainWindow.UserCanvas, path);
            }
            catch (Exception e)
            {
                mainWindow.dialogService.ShowMessageBox(
                    "Error serializing: " + e.Message,
                    "Error");
                return;
            }
        }
    }
}
