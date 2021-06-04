using lab1OOP.DrawerClass;

namespace lab1OOP.HistoryClass
{
    class DeleteItem : HistoryItem
    {
        private readonly Drawer Drawer;
        private readonly int Layer;
        private readonly int Order;

        public DeleteItem(Drawer drawer, int layout, int order)
        {
            Drawer = drawer;
            Layer = layout;
            Order = order;
        }

        public override void Redo()
        {
            Drawer.CustomCanvas.Delete(Drawer, Layer);
        }

        public override void Undo()
        {
            Drawer.CustomCanvas.Add(Drawer, Layer, Order);
        }
    }
}
