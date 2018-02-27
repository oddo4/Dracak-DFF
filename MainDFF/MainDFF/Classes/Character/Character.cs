using MainDFF.Classes.AttackBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainDFF.Classes.Character
{
    public class Character
    {
        public CharacterDescription CharacterDescription { get; set; }
        public CharacterStats CharacterStats { get; set; }
        public CharacterAnimation CharacterAnimation { get; set; }
        public IAttackBehavior AttackBehavior = new BasicAttackBehavior();
    }
}