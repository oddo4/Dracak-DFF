using MainDFF.Classes.ControlActions.MenuActions;
using MainDFF.Classes.Exploration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MainDFF.Pages
{
    /// <summary>
    /// Interakční logika pro LevelSelectPage.xaml
    /// </summary>
    public partial class LevelSelectPage : Page
    {
        LevelSelectAction menuAction = new LevelSelectAction();
        public LevelSelectPage()
        {
            InitializeComponent();
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
            SetChapters();
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            var max = App.dataFileLists.CompletedChapters;
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                MenuSelect(selected, max);
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E2001");
            }
            else
            {
                MenuConfirm(selected);
            }
        }
        private void MenuSelect(int selected, int max)
        {
            for (int i = 0; i < max + 1; i++)
            {
                var chapterCanvas = (Canvas)LevelIcons.Children[i];
                var image = (Image)chapterCanvas.Children[0];

                if (i == selected)
                {
                    var left = Canvas.GetLeft(chapterCanvas);
                    var top = Canvas.GetTop(chapterCanvas);

                    Canvas.SetLeft(LevelSelectCursor, left);
                    Canvas.SetTop(LevelSelectCursor, top);

                    Canvas.SetLeft(image, 0);
                }
                else
                {
                    Canvas.SetLeft(image, -50);
                }
            }
            
            menuAction.CurrentIndex = selected;
        }
        private void MenuConfirm(int selected)
        {
            if(selected != -2)
            {
                App.MainFrame.NavigationService.GoBack();
                ResetEvent();
            }
            else
            {
                Random rand = new Random();
                App.levelList = new LevelList(rand, menuAction.CurrentIndex);
                App.MainFrame.NavigationService.Navigate(new LevelPlayPage(menuAction.CurrentIndex, App.levelList.LevelsList[0]));
                ResetEvent();
            }
            
        }
        private void SetChapters()
        {
            for (int i = 0; i < App.dataFileLists.CompletedChapters + 1; i++)
            {
                var chapterCanvas = (Canvas)LevelIcons.Children[i];
                chapterCanvas.Visibility = Visibility.Visible;
            }
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            menuAction = new LevelSelectAction();
        }
    }
}
