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
    class DeserializeCommand : CommandItem
    {
        CustomCanvas canvas;

        public DeserializeCommand(MainWindow mw, CustomCanvas canvas)
        {
            mainWindow = mw;
            this.canvas = canvas;
        }

        public override void Redo()
        {
            if (!mainWindow.dialogService.OpenFileDialog())
            {
                return;
            }

            SerializerCreator serializerCreator = new SerializerCreator();
            string path = mainWindow.dialogService.FilePath;
            ICustomCanvasSerializer serializer =
                serializerCreator.CreateSerializer(
                    mainWindow.dialogService.FileExtension);
            CustomCanvas data;
            try
            {
                data = serializer.Deserialize(path);
            }
            catch (Exception e)
            {
                mainWindow.dialogService.ShowMessageBox(
                    "Error deserializing: " + e.Message,
                    "Error");
                return;
            }

            for (int i = mainWindow.UserCanvas.Count - 1; i >= 0; i--)
            {
                var historyItem = new DeleteLayer(
                    mainWindow.UserCanvas,
                    mainWindow,
                    mainWindow.UserCanvas.LayerList[i]);
                historyItem.Redo();
            }

            data.LayerList.ForEach(l =>
            {
                var historyItem = new AddLayer(
                    mainWindow.UserCanvas,
                    mainWindow,
                    l);
                historyItem.Redo();
            });
        }
    }
}
