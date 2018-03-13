using MainDFF.Classes.Battle.AttackBehaviors;
using MainDFF.Classes.ControlActions.MenuActions;
using MainDFF.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MainDFF.Classes.Battle
{
    public class SwitchMenuAction
    {
        public AMenuSelectAction GetMenuAction(int CurrentIndex, Image EnemyMenuCursor, Grid SubMenuGrid, List<IBehavior> behaviorList, List<string> itemsList, ACharacter player)
        {
            var listBox = (ListBox)SubMenuGrid.Children[2];

            switch (CurrentIndex)
            {
                case 0:
                    EnemyMenuCursor.Visibility = Visibility.Visible;
                    return new BattlePageTargetMenuAction();
                case 1:
                    SubMenuGrid.Visibility = Visibility.Visible;
                    var behavior = behaviorList.GetRange(2, behaviorList.Count - 2);

                    foreach (IBehavior b in behavior)
                    {
                        if (player.CharacterStatus.CurrentMP >= b.Cost)
                        {
                            b.IsUsable = true;
                        }
                        else
                        {
                            b.IsUsable = false;
                        }
                    }

                    listBox.ItemsSource = behavior;

                    return new BattlePageSubMenuAction();
                case 2:
                    return null;
                case 3:
                    SubMenuGrid.Visibility = Visibility.Visible;
                    listBox.ItemsSource = itemsList;

                    return new BattlePageSubMenuAction();
                case 4:
                    EnemyMenuCursor.Visibility = Visibility.Visible;
                    return new BattlePageTargetMenuAction();
            }

            return null;
        }
        public AMenuSelectAction GetSubMenuAction(IBehavior Behavior, Image EnemyMenuCursor, Image PlayerMenuCursor)
        {
            if (Behavior is IAttackBehavior)
            {
                EnemyMenuCursor.Visibility = Visibility.Visible;
                return new BattlePageTargetMenuAction();
            }
            else if (Behavior is IBuffBehavior)
            {
                PlayerMenuCursor.Visibility = Visibility.Visible;
                return new BattlePagePlayerMenuAction();
            }
            else if (Behavior is IDebuffBehavior)
            {
                EnemyMenuCursor.Visibility = Visibility.Visible;
                return new BattlePageTargetMenuAction();
            }
            else if (Behavior is IUseItemBehavior)
            {
                PlayerMenuCursor.Visibility = Visibility.Visible;
                return new BattlePagePlayerMenuAction();
            }

            return null;
        }
    }
}
