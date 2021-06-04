using System.Collections.Generic;

namespace lab1OOP.HistoryClass
{
    public class History
    {
        private readonly List<HistoryItem> States = new List<HistoryItem>();
        private int ActiveState = -1;

        private bool CanUndo { get { return ActiveState >= 0; } }
        private bool CanRedo
        {
            get
            {
                return (States.Count > 0) && (ActiveState < States.Count - 1);
            }
        }

        public History()
        {
        }

        public void Undo()
        {
            if (!CanUndo)
            {
                return;
            }

            States[ActiveState].Undo();
            ActiveState--;
        }

        public void Redo()
        {
            if (!CanRedo)
            {
                return;
            }

            ActiveState++;
            States[ActiveState].Redo();
        }

        public void Add(HistoryItem item)
        {
            CutOffHistory();
            States.Add(item);
            ActiveState++;
        }

        void CutOffHistory()
        {
            int cutStart = ActiveState + 1;
            if (cutStart < States.Count)
                States.RemoveRange(cutStart, States.Count - cutStart);
        }
    }
}
