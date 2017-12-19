using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DFF.Pages
{
    /// <summary>
    /// Interakční logika pro MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        Window window;

        public MainMenu()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += LabelBlink;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private void MainMenuKeyDown(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            window.KeyDown += MainMenuOptions;
        }

        private void MainMenuOptions(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    window.KeyDown -= MainMenuOptions;
                    MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    mainWindow.MainFrame.Content = new LevelExplore();
                    break;
                default:
                    break;
            }
        }

        private bool BlinkOn = false;
        private void LabelBlink(object sender, EventArgs e)
        {
            if (BlinkOn)
            {
                LabelStart.Visibility = Visibility.Visible;
            }
            else
            {
                LabelStart.Visibility = Visibility.Hidden;
            }
            BlinkOn = !BlinkOn;
        }
    }
}
