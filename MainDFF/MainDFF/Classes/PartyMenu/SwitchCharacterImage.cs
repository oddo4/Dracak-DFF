using MainDFF.Classes.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MainDFF.Classes.PartyMenu
{
    public class SwitchCharacterImage
    {
        public void SetCharacterImage(Grid gridMember, PlayerCharacter player)
        {
            var canvas = (Canvas)gridMember.Children[0];
            var image = (Image)canvas.Children[0];
            var name = (TextBlock)gridMember.Children[1];
            var className = (TextBlock)gridMember.Children[2];

            BitmapImage source = new BitmapImage();

            source.BeginInit();
            source.UriSource = new Uri("pack://application:,,,/Resources/CharacterPanels/" + player.CharacterID + ".png");
            source.EndInit();

            image.Source = source;
            name.Text = player.Name;
            className.Text = player.CharacterClass.Name; 
        }

        public void LoadCharacters(StackPanel PartyMembers, int type)
        {
            switch (type)
            {
                case 0:
                    for (int i = 0; i < PartyMembers.Children.Count; i++)
                    {
                        var grid = (Grid)PartyMembers.Children[i];
                        var charID = int.Parse(App.dataFileLists.playerCurrentPartyIDList[i]);
                        var player = App.dataFileLists.playerBasicData[charID];

                        SetCharacterImage(grid, player);
                    }
                    break;
                case 1:
                    for (int i = 0; i < PartyMembers.Children.Count; i++)
                    {
                        var grid = (Grid)PartyMembers.Children[i];
                        var charID = int.Parse(App.dataFileLists.playerCurrentPartyIDList[i]);
                        var player = App.dataFileLists.playerBasicData[charID];

                        SetCharacterImage(grid, player);
                        SetCharacterStatus(grid, player);
                    }
                    break;
            }
        }

        public void SetCharacterStatus(Grid gridMember, PlayerCharacter player)
        {
            var hpStack = (StackPanel)gridMember.Children[3];
            var hpValueStack = (StackPanel)hpStack.Children[0];
            var hpValue = (TextBlock)hpValueStack.Children[1];
            hpValue.Text = player.CharacterStatus.CurrentHP.ToString();
            var hpBar = (ProgressBar)hpStack.Children[1];
            hpBar.Maximum = player.CharacterStats.HP;
            hpBar.Value = player.CharacterStatus.CurrentHP;

            var mpSpGrid = (Grid)gridMember.Children[4];
            var mpStack = (StackPanel)mpSpGrid.Children[0];
            var mpValueStack = (StackPanel)hpStack.Children[0];
            var mpValue = (TextBlock)hpValueStack.Children[1];
            mpValue.Text = player.CharacterStatus.CurrentMP.ToString();
            var mpBar = (ProgressBar)mpStack.Children[1];
            mpBar.Maximum = player.CharacterStats.MP;
            mpBar.Value = player.CharacterStatus.CurrentMP;

            var spStack = (StackPanel)mpSpGrid.Children[1];
            var spBar = (ProgressBar)spStack.Children[1];
            spBar.Maximum = player.CharacterStats.SP;
            spBar.Value = player.CharacterStatus.CurrentSP;
        }
    }
}
