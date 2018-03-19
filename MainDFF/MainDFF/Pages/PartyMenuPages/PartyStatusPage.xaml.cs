using MainDFF.Classes.ControlActions.MenuActions;
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

namespace MainDFF.Pages.PartyMenuPages
{
    /// <summary>
    /// Interakční logika pro PartyStatusPage.xaml
    /// </summary>
    public partial class PartyStatusPage : Page
    {
        PartyStatusPageAction menuAction = new PartyStatusPageAction();
        public PartyStatusPage()
        {
            InitializeComponent();
        }
        public PartyStatusPage(int index)
        {
            InitializeComponent();
            App.charactersLists.PlayerList = App.dataFileLists.AssemblePartyCharacter();
            SetCharacterStatus(index);
        }

        private void SetCharacterStatus(int index)
        {
            var player = App.charactersLists.PlayerList[index];
            var stats = player.CharacterStats;
            var status = player.CharacterStatus;

            SetCharacterImage(player.CharacterID);

            FullName.Text = player.FullName;
            ClassValue.Text = player.CharacterClass.Name;

            LvlValue.Text = stats.LVL.ToString();

            HpValue.Text = status.CurrentHP.ToString() + "/" + stats.HP.ToString();
            HpBarValue.Maximum = stats.HP;
            HpBarValue.Value = status.CurrentHP;

            MpValue.Text = status.CurrentMP.ToString() + "/" + stats.MP.ToString();
            MpBarValue.Maximum = stats.MP;
            MpBarValue.Value = status.CurrentMP;

            SpBarValue.Maximum = stats.SP;
            SpBarValue.Value = status.CurrentSP;

            ExpValue.Text = stats.CurrentEXP.ToString();
            NextLvlValue.Text = (stats.NextEXP - stats.CurrentEXP).ToString();

            AtkValue.Text = stats.ATK.ToString();
            DefValue.Text = stats.DEF.ToString();
            MagAtkValue.Text = stats.MAGATK.ToString();
            MagDefValue.Text = stats.MAGDEF.ToString();
            SpdValue.Text = stats.SPD.ToString();
            FireResValue.Text = stats.RESFIRE.ToString();
            IceResValue.Text = stats.RESICE.ToString();
            ThunderResValue.Text = stats.RESTHUNDER.ToString();
            WaterResValue.Text = stats.RESWATER.ToString();


        }

        private void SetCharacterImage(string characterID)
        {
            BitmapImage source = new BitmapImage();

            source.BeginInit();
            source.UriSource = new Uri(App.resourcePaths.GetPlayerImagePath(characterID));
            source.EndInit();

            ImagePanel.Source = source;
        }

        private void MenuKey_Loaded(object sender, RoutedEventArgs e)
        {
            App.window.KeyDown += MenuKeyDown;
        }
        private void MenuKeyDown(object sender, KeyEventArgs e)
        {
            var max = App.dataFileLists.playerCurrentPartyIDList.Count - 1;
            var selected = menuAction.GetDirection(e.Key, max);
            if (selected > -1)
            {
                SetCharacterStatus(selected);
                menuAction.CurrentIndex = selected;
            }
            else if (selected == -1)
            {
                Debug.WriteLine("E0001");
            }
            else if (selected == -3)
            {
                App.MainFrame.NavigationService.GoBack();
                ResetEvent();
            }
        }
        private void ResetEvent()
        {
            App.window.KeyDown -= MenuKeyDown;
            menuAction = new PartyStatusPageAction();
        }
    }
}
