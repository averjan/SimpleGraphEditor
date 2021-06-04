using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization;

namespace lab1OOP.DrawerClass
{
    [Serializable]
    [XmlInclude(typeof(CustomCanvas))]
    public class CustomCanvas
    {
        [NonSerialized]
        [JsonIgnore]
        [XmlIgnore]
        public readonly Canvas Canvas;

        [JsonIgnore]
        [XmlIgnore]
        public int Count { get { return LayerList.Count; } }

        [JsonInclude]
        public List<Layer> LayerList = new List<Layer>();

        [NonSerialized]
        [JsonIgnore]
        [XmlIgnore]
        public Layer ActiveLayer;

        [JsonIgnore]
        [XmlIgnore]
        public int ZIndex { get { return LayerList.IndexOf(ActiveLayer); } }

        public CustomCanvas()
        {
        }

        public CustomCanvas(Canvas userCanvas)
        {
            Canvas = userCanvas;
        }

        public void AddLayer(Layer layer)
        {
            LayerList.Add(layer);
            Redraw();
        }

        public void InsertLayer(Layer layer, int index)
        {
            LayerList.Insert(index, layer);
            SetLayer(index);
            Redraw();
        }

        public void SetLayer(int index)
        {
            ActiveLayer = LayerList[index];
        }

        public void DeleteLayer(int index)
        {
            LayerList.RemoveAt(index);
            Redraw();
        }

        public void Add(Drawer drawer)
        {
            ActiveLayer.DrawerList.Add(drawer);
            Redraw();
        }

        public void Add(Drawer drawer, int index)
        {
            if (index == ActiveLayer.DrawerList.Count)
            {
                ActiveLayer.DrawerList.Add(drawer);
            }
            else
            {
                ActiveLayer.DrawerList.Insert(index, drawer);
            }

            Redraw();
        }

        public void Add(Drawer drawer, int layer, int index)
        {
            var list = LayerList[layer].DrawerList;
            if (index == list.Count)
            {
                list.Add(drawer);
            }
            else
            {
                list.Insert(index, drawer);
            }

            Redraw();
        }

        // TODO: move FindDrawerByPoint, Delete and etc to Layer class.
        public Drawer FindDrawerByPoint(Point point)
        {
            Drawer deleted = ActiveLayer.DrawerList.FindLast(delegate (Drawer drawer)
            {
                return (drawer.Figure.IncludePoint(point));
            });

            if (deleted != null)
            {
                return deleted;
            }

            return null;
        }

        public void Delete(Drawer drawer)
        {
            ActiveLayer.DrawerList.Remove(drawer);
            Redraw();
        }

        public void Delete(Drawer drawer, int layer)
        {
            if (layer < LayerList.Count)
            {
                LayerList[layer].DrawerList.Remove(drawer);
            }

            Redraw();
        }

        public void Redraw()
        {
            Canvas.Children.Clear();
            LayerList.ForEach(layer =>
           {
               layer.DrawerList.ForEach(drawer =>
               {
                   drawer.CustomCanvas = this;
                   drawer.Errase();
                   drawer.Show();
               });
           });
        }

        public int IndexOf(Drawer drawer)
        {
            return ActiveLayer.DrawerList.IndexOf(drawer);
        }
    }
}
