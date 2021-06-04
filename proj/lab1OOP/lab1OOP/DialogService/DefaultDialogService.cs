//using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1OOP.DialogService
{
    public class DefaultDialogService
    {
        public string FilePath { get; set; }
        public string FileExtension { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "dat";
            openFileDialog.Filter = "Binary (*.dat)|*.dat|XML (*.xml)|*.xml|Custom txt (*.cus)|*.cus|Json file (*.json)|*.json";
            openFileDialog.AddExtension = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
                FileExtension = Path.GetExtension(FilePath);
                return true;
            }

            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "dat";
            saveFileDialog.Filter = "Binary (*.dat)|*.dat|XML (*.xml)|*.xml|Custom txt (*.cus)|*.cus|Json file (*.json)|*.json";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = saveFileDialog.FileName;
                FileExtension = Path.GetExtension(FilePath);
                return true;
            }

            return false;
        }

        public bool LoadPluginDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "dll";
            openFileDialog.Filter = "DLL (*.dll)|*.dll";
            openFileDialog.AddExtension = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
                FileExtension = Path.GetExtension(FilePath);
                return true;
            }

            return false;
        }

        public bool ShowMessageBox(string message, string name)
        {
            var result = MessageBox.Show(
                message,
                name,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);

            return result == DialogResult.OK;
        }
    }
}
