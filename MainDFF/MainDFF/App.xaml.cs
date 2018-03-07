using MainDFF.Classes.Battle;
using MainDFF.Classes.FileHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MainDFF
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        static public Window window;
        static public DataFileLists dataFileLists = new DataFileLists();
        static public FileHelper fileHelper = new FileHelper();
    }
}
