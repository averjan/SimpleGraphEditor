using lab1OOP.Creator;
using lab1OOP.DialogService;
using lab1OOP.DrawerClass;
using lab1OOP.HistoryClass;
using lab1OOP.PluginManagerClass;
using lab1OOP.SerializeClass;
using lab1OOP.Shapes;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace lab1OOP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public History UserHistory;
        public DefaultDialogService dialogService;
        public Drawer Drawer;
        private bool IsMouseDown = false;

        public CustomCanvas UserCanvas { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((ListBoxLayers.SelectedIndex != -1) && (Drawer != null))
            {
                ((IInputElement)sender).CaptureMouse();
                Point p = Mouse.GetPosition(Canvas_DrawField);
                Drawer.BasePoint = new Point(p.X, p.Y);
                Drawer.Figure.Resize(p.X, p.Y, p.X, p.Y);
                Drawer.Show();
                IsMouseDown = true;
            }
        }

        private void Canv_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseDown)
            {
                ((IInputElement)sender).ReleaseMouseCapture();
                IsMouseDown = false;

                // Add "Create shape" operation to history and add
                // new shape to UserCanvas(inside AddItem.Redo())
                HistoryItem item = new AddItem(Drawer, UserCanvas.ZIndex, UserCanvas.ActiveLayer.Count);
                UserHistory.Add(item);
                item.Redo();

                // Creating of copy instance of Drawer
                Drawer = new Drawer(
                    UserCanvas,
                    Activator.CreateInstance(Drawer.Figure.GetType())
                    as CustomShape)
                {
                    Fill = new SolidColorBrush(Colors.Black),
                    Contour = Brushes.Black
                };
            }
        }

        private void Canv_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown &&
                (MouseButtonState.Pressed == Mouse.LeftButton))
            {
                Point p = Mouse.GetPosition(Canvas_DrawField);
                Drawer.EditShape(p);
            }
        }

        private void Canv_Initialized(object sender, EventArgs e)
        {
            UserCanvas = new CustomCanvas(Canvas_DrawField);
            Drawer = new Drawer(UserCanvas, null)
            {
                Fill = new SolidColorBrush(Colors.Black),
                Contour = Brushes.Black
            };

            dialogService = new DefaultDialogService();
            UserHistory = new History();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string radioButtonName = (sender as RadioButton).Name;
            Drawer.Figure = (new ShapeCreator()).CreateShape(radioButtonName);
        }

        private void Canv_MouseRightButtonDown(
            object sender,
            MouseButtonEventArgs e)
        {
            if (!IsMouseDown)
            {
                var deleted = UserCanvas.FindDrawerByPoint(
                    Mouse.GetPosition(Canvas_DrawField));
                if (deleted != null)
                {
                    // Add "Delete shape" operation to history and delete
                    // shape from UserCanvas(inside DeleteItem.Redo)
                    var item = new DeleteItem(
                        deleted,
                        UserCanvas.ZIndex,
                        UserCanvas.IndexOf(deleted));
                    UserHistory.Add(item);
                    item.Redo();
                }
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            // Check for Ctrl+Z
            if ((e.Key == Key.Z) &&
                (Keyboard.IsKeyDown(Key.LeftCtrl) ||
                Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                UserHistory.Undo();
            }

            // Check for Ctrl+R
            if ((e.Key == Key.R) &&
                (Keyboard.IsKeyDown(Key.LeftCtrl) ||
                Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                UserHistory.Redo();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var serializeCommand = new SerializeCommand(this, UserCanvas);
            serializeCommand.Redo();
        }

        private void ButtonGet_Click(object sender, RoutedEventArgs e)
        {
            var deserializeCommand = new DeserializeCommand(this, UserCanvas);
            deserializeCommand.Redo();
        }

        private void ButtonAddLayer_Click(object sender, RoutedEventArgs e)
        {
            var historyItem = new AddLayer(UserCanvas, this);
            UserHistory.Add(historyItem);
            historyItem.Redo();
        }

        private void ButtonDeleteLayer_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxLayers.SelectedIndex != -1)
            {
                var historyItem = new DeleteLayer(UserCanvas, this);
                UserHistory.Add(historyItem);
                historyItem.Redo();
            }
        }

        private void ListBoxLayers_Selected(object sender, RoutedEventArgs e)
        {
            if (ListBoxLayers.SelectedIndex != -1)
            {
                UserCanvas.SetLayer(ListBoxLayers.SelectedIndex);
            }
        }

        private void ListBoxLayers_Initialized(object sender, EventArgs e)
        {
            var historyItem = new AddLayer(UserCanvas, this);
            historyItem.Redo();
        }

        private void ButtonPlugin_Click(object sender, RoutedEventArgs e)
        {
            var loadPluginCommand = new LoadPluginCommand(this);
            loadPluginCommand.Redo();
        }
    }
}
