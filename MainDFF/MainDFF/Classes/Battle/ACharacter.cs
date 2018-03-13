using MainDFF.Classes.Battle.AttackBehaviors;
using MainDFF.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MainDFF.Classes.Battle
{
    public abstract class ACharacter
    {
        public string CharacterID { get; set; }
        public string Name { get; set; }
        public CharacterStats CharacterStats { get; set; }
        [JsonIgnore]
        public CharacterStatus CharacterStatus { get; set; }
        [JsonIgnore]
        public int CurrentAnimation = -1;
        [JsonIgnore]
        public CharacterBuffsDebuffs CharacterBuffsDebuff = new CharacterBuffsDebuffs();
        /// <summary>
        /// CharacterAnimation ID
        /// 0 - idle
        /// 1 - move
        /// 2 - attack
        /// 3 - magic
        /// 4 - limit
        /// 5 - dying
        /// 6 - dead
        /// 7 - winpose
        /// 8 - win
        /// </summary>
        public List<CharacterAnimation> CharacterAnimationList = new List<CharacterAnimation>();
        public List<IBehavior> BehaviorList = new List<IBehavior>();

        public void SwitchAnimation(Canvas canvas, int animID, string path, bool loop = true)
        {
            var image = (Image)canvas.Children[0];

            var newAnim = CharacterAnimationList[animID];
            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(path + newAnim.SpriteFileName);
            source.EndInit();

            image.Source = source;
            image.Width = source.Width;
            image.Height = source.Height;

            canvas.Width = source.Width / newAnim.SpriteRowCol.X;
            canvas.Height = source.Height / newAnim.SpriteRowCol.Y;

            if(CurrentAnimation >= 0)
            {
                CharacterAnimationList[CurrentAnimation].StopSprite();
            }
            newAnim.Loop = loop;
            newAnim.CreateSprite(image, new TimeSpan(0,0,0,0,75));
            CurrentAnimation = animID;
        }
        public CharacterAnimation GetCurrentAnimation()
        {
            return CharacterAnimationList[CurrentAnimation];
        }
        public void SetMinimumStats()
        {
            /*foreach (CharacterStats stat in CharacterStats)
            {

            }*/
        }
    }
}