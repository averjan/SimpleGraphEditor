using lab1OOP.DrawerClass;

namespace lab1OOP.HistoryClass
{
    class AddItem : HistoryItem
    {
        private readonly Drawer Drawer;
        private readonly int Order;
        private readonly int Layer;

        public AddItem(Drawer drawer, int layer, int order)
        {
            Drawer = drawer;
            Layer = layer;
            Order = order;
        }

        public override void Redo()
        {
            Drawer.CustomCanvas.Add(Drawer, Layer, Order);
        }

        public override void Undo()
        {
            Drawer.CustomCanvas.Delete(Drawer, Layer);
        }
    }
}
