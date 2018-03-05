using MainDFF.Classes.Battle.CharacterClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle
{
    public class PlayerCharacter : ACharacter
    {
        public ACharacterClass CharacterClass { get; set; }

        public ACharacterClass SetClass()
        {
            switch (ClassID)
            {
                case 0:
                    return new ClassVanguard();
                case 1:
                    return new ClassAssassin();
                case 2:
                    return new ClassMarksman();
                case 3:
                    return new ClassSpecialist();
            }
            return null;
        }
    }
}
