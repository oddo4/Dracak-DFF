using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MainDFF.Classes.Battle
{
    public class CharactersLists
    {
        public List<PlayerCharacter> PlayerList = new List<PlayerCharacter>();
        public List<EnemyCharacter> EnemyList = new List<EnemyCharacter>();
        public List<ACharacter> CharacterOrder = new List<ACharacter>();

        public void CreateOrder()
        {
            CharacterOrder.AddRange(PlayerList);
            CharacterOrder.AddRange(EnemyList);
            CharacterOrder = CharacterOrder.OrderByDescending(x => x.CharacterStats.SPD).ToList();
        }

        public void ReOrder(ListBox listbox)
        {
            var character = CharacterOrder[0];
            var grid = listbox.Items[0];

            CharacterOrder.RemoveAt(0);
            listbox.Items.RemoveAt(0);

            CharacterOrder.Add(character);
            listbox.Items.Add(grid);
        }
    }
}
