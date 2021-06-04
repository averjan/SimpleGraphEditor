using lab1OOP.DrawerClass;

namespace lab1OOP.SerializeClass
{
    interface ICustomCanvasSerializer
    {
        void Serizalize(CustomCanvas canvas, string path);

        CustomCanvas Deserialize(string path);
    }
}
