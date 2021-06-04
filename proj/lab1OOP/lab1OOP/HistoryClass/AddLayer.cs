using lab1OOP.DrawerClass;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace lab1OOP.HistoryClass
{
    class AddLayer : HistoryItem
    {
        private readonly Layer layer;
        private readonly CustomCanvas canvas;
        private readonly int listBoxCount;

        public AddLayer(CustomCanvas canvas, MainWindow mw, string Name = null)
        {
            layer = new Layer();
            this.canvas = canvas;
            mainWindow = mw;
            layer.Name = CreateLayerName(mainWindow.ListBoxLayers, Name);
        }

        public AddLayer(CustomCanvas canvas, MainWindow mw, Layer layer)
        {
            this.layer = layer;
            this.canvas = canvas;
            mainWindow = mw;
            listBoxCount = mainWindow.ListBoxLayers.Items.Count;
        }

        public override void Redo()
        {
            var item = new ListBoxItem();
            item.Content = layer.Name;
            mainWindow.ListBoxLayers.Items.Insert(listBoxCount, item);
            canvas.InsertLayer(layer, listBoxCount);
            mainWindow.ListBoxLayers.SelectedItem = item;
        }

        public override void Undo()
        {
            mainWindow.ListBoxLayers.Items.RemoveAt(listBoxCount);
            canvas.DeleteLayer(listBoxCount);
        }

        private int GetListBoxItemContent(ListBox lb)
        {
            int maxVal = 0;
            foreach (ListBoxItem item in lb.Items)
            {
                Match m = Regex.Match(item.Content.ToString(), @"^layer([0-9]+)$");
                if (m.Success)
                {
                    int layerNumber = int.Parse(m.Groups[1].Value);
                    maxVal = (layerNumber > maxVal) ? layerNumber : maxVal;
                }
            }

            return maxVal;
        }

        private string CreateLayerName(ListBox lb, string nameByUser)
        {
            if (nameByUser == null)
            {
                var listBoxCount = lb.Items.Count;
                var listBoxItemContent = listBoxCount == 0 ? 0
                    : GetListBoxItemContent(lb) + 1;
                return "layer" + listBoxItemContent.ToString();
            }

            return nameByUser;
        }
    }
}
