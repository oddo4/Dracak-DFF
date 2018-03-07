using MainDFF.Classes.Battle.AttackBehaviors;
using MainDFF.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainDFF.Classes.Battle
{
    public abstract class ACharacter
    {
        public string CharacterID { get; set; }
        public string Name { get; set; }
        public CharacterStats CharacterStats { get; set; }
        [JsonIgnore]
        public CharacterStatus CharacterStatus { get; set; }
        public List<CharacterAnimation> CharacterAnimationList = new List<CharacterAnimation>();
        public List<IAttackBehavior> BehaviorList = new List<IAttackBehavior>();
        [JsonIgnore]
        public IAttackBehavior AttackBehavior = new BasicAttackBehavior("Basic Attack");
    }
}