using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1OOP.HistoryClass
{
    public abstract class CommandItem
    {
        protected MainWindow mainWindow;
        public abstract void Redo();
    }
}
