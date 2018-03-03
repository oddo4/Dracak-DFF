using MainDFF.Classes.AttackBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainDFF.Classes.Battle
{
    public abstract class ACharacter
    {
        public int FileID { get; set; }
        public CharacterDescription CharacterDescription { get; set; }
        public CharacterStats CharacterStats { get; set; }
        public CharacterStatus CharacterStatus { get; set; }
        public SpriteAnimation CharacterAnimation { get; set; }
        public IAttackBehavior AttackBehavior = new BasicAttackBehavior();
    }
}