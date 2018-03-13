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
        public int GetPlayerRow(PlayerCharacter player)
        {
            var row = 0;
            foreach (PlayerCharacter p in PlayerList)
            {
                
                if (player == p)
                {
                    return row;
                }
                row++;
            }

            return -1;
        }
        public int GetEnemyRow(EnemyCharacter enemy)
        {
            var row = 0;
            foreach (EnemyCharacter e in EnemyList)
            {

                if (enemy == e)
                {
                    return row;
                }
                row++;
            }

            return -1;
        }
        public PlayerCharacter GetPlayerTarget(Random rand)
        {
            var alive = PlayerList.Where(p => p.CharacterStatus.CurrentHP > 0).ToList();

            return alive[rand.Next(0, alive.Count)];
        }
        public List<PlayerCharacter> GetPlayerAlive()
        {
            var alive = PlayerList.Where(p => p.CharacterStatus.CurrentHP > 0).ToList();

            return alive;
        }
        public bool CheckEnemyAlive()
        {
            var alive = EnemyList.Where(e => e.CharacterStatus.CurrentHP > 0).ToList();

            if(alive.Any())
            {
                return true;
            }

            return false;
        }
        public void ResetLists()
        {
            EnemyList = new List<EnemyCharacter>();
            CharacterOrder = new List<ACharacter>();
        }
    }
}
