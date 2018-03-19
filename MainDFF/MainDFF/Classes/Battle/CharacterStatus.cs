using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle
{
    public class CharacterStatus
    {
        public double CurrentHP { get; set; }
        public double CurrentMP { get; set; }
        public double CurrentSP { get; set; }
        public CharacterStatus(CharacterStats stats)
        {
            this.CurrentHP = stats.HP;
            this.CurrentMP = stats.MP;
            this.CurrentSP = 0;
        }
        public bool CheckAlive(ACharacter character, List<ACharacter> characterOrder)
        {
            if (CurrentHP <= 0)
            {
                CurrentHP = 0;
                CurrentSP = 0;
                characterOrder.Remove(character);
                return true;
            }
            return false;
        }
        public bool CheckDying(ACharacter character)
        {
            if (CurrentHP <= character.CharacterStats.HP * 0.3)
            {
                return true;
            }
            return false;
        }
        public void AddSP(double max, int damage)
        {
            var valueSP = damage;
            if(CurrentSP + valueSP < max)
            {
                CurrentSP += valueSP;
            }
            else
            {
                CurrentSP = max;
            }
        }

        public void ResetSP()
        {
            CurrentSP = 0;
        }
    }
}
