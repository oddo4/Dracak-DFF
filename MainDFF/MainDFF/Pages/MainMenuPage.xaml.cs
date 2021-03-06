﻿using System;
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
using MainDFF.Classes.ControlActions.MenuActions;
using MainDFF.Classes.FileHelper;
using MainDFF.Pages;

namespace MainDFF.Pages
{
    /// <summary>
    /// Interakční logika pro MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        MainMenuSelectAction menuAction = new MainMenuSelectAction();
        bool newGame;
        public MainMenuPage(bool NewGame = false)
        {
            InitializeComponent();
            App.MainMenu = this;
            newGame = NewGame;

            ChangeMarginGrid(0);
        }
        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
            if (newGame)
            {
                App.MainFrame.NavigationService.RemoveBackEntry();
                newGame = false;
            }
            App.dataFileLists.CreateNewStats();
            App.charactersLists.PlayerList = App.dataFileLists.AssemblePartyCharacter();
            SetChapters();
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            var max = LowerMenu.Children.Count - 1;
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                ChangeMarginGrid(selected);
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E1001");
            }
            else
            {
                switch (selected)
                {
                    case -2:
                        if (menuAction.NavigateToPage != null)
                        {
                            App.MainFrame.NavigationService.Navigate(menuAction.NavigateToPage);
                            ResetEvent();
                        }
                        break;
                    case -3:
                        ChangeMarginGrid(4);
                        break;
                    case -4:
                        App.MainFrame.NavigationService.GoBack();
                        ResetEvent();
                        break;
                    default:
                        break;
                }
            }
        }
        private void ChangeMarginGrid(int selected)
        {
            ((Grid)LowerMenu.Children[menuAction.CurrentIndex]).Margin = new Thickness(0, 0, 0, 0);
            ((Rectangle)((Grid)LowerMenu.Children[menuAction.CurrentIndex]).Children[0]).Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF535969"));
            ((Grid)LowerMenu.Children[selected]).Margin = new Thickness(0, -4, 0, 4);
            ((Rectangle)((Grid)LowerMenu.Children[selected]).Children[0]).Stroke = Brushes.WhiteSmoke;

            menuAction.CurrentIndex = selected;
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
            ChangeMarginGrid(0);
            menuAction = new MainMenuSelectAction();
        }
    }
}
