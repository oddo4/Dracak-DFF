using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Battle
{
    public class EnemyCharacter : ACharacter
    {
        public EnemyCharacter()
        {

        }
        public EnemyCharacter(EnemyCharacter enemy)
        {
            this.Name = enemy.Name;
            this.CharacterID = enemy.CharacterID;
            this.CharacterStats = enemy.CharacterStats;
            this.BehaviorList = enemy.BehaviorList;
            foreach (CharacterAnimation anim in enemy.CharacterAnimationList)
            {
                var copy = new CharacterAnimation() { SpriteFileName = anim.SpriteFileName, SpriteRowCol = anim.SpriteRowCol, SpriteFramesCount = anim.SpriteFramesCount };
                this.CharacterAnimationList.Add(copy);
            }
        }
    }
}
