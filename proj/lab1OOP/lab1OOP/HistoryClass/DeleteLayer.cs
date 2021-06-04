using lab1OOP.DrawerClass;
using System.Windows.Controls;

namespace lab1OOP.HistoryClass
{
    class DeleteLayer : HistoryItem
    {
        private readonly int order;
        private readonly Layer layer;
        private readonly CustomCanvas canvas;

        public DeleteLayer(CustomCanvas canvas, MainWindow mw)
        {
            this.canvas = canvas;
            mainWindow = mw;
            order = mainWindow.ListBoxLayers.SelectedIndex;
            layer = canvas.LayerList[order];
        }

        public DeleteLayer(CustomCanvas canvas, MainWindow mw, Layer layer)
        {
            this.canvas = canvas;
            mainWindow = mw;
            order = canvas.LayerList.IndexOf(layer);
            this.layer = layer;
        }

        public override void Redo()
        {
            canvas.DeleteLayer(order);
            mainWindow.ListBoxLayers.Items.RemoveAt(order);
        }

        public override void Undo()
        {
            var item = new ListBoxItem();
            item.Content = layer.Name;
            mainWindow.ListBoxLayers.Items.Insert(order, item);
            canvas.InsertLayer(layer, order);
        }
    }
}
