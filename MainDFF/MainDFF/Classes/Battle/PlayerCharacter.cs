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
    }
}
