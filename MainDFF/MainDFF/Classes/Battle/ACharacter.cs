using MainDFF.Classes.AttackBehaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainDFF.Classes.Battle
{
    public abstract class ACharacter
    {
        public string CharacterID { get; set; }
        public int ClassID { get; set; }
        public string Name { get; set; }
        public CharacterStats CharacterStats { get; set; }
        public CharacterStatus CharacterStatus { get; set; }
        public List<CharacterAnimation> CharacterAnimationList = new List<CharacterAnimation>();
        public IAttackBehavior AttackBehavior = new BasicAttackBehavior();
    }
}