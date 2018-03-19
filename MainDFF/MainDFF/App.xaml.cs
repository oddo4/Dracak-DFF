using MainDFF.Classes;
using MainDFF.Classes.Battle;
using MainDFF.Classes.Exploration;
using MainDFF.Classes.FileHelper;
using MainDFF.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MainDFF
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        static public Window window;
        static public Frame MainFrame;
        static public Page MainMenu; 
        static public DataFileLists dataFileLists = new DataFileLists();
        static public FileHelper fileHelper = new FileHelper();
        static public CharactersLists charactersLists = new CharactersLists();
        static public ResourcePaths resourcePaths = new ResourcePaths();
        static public LevelList levelList;
    }
}
