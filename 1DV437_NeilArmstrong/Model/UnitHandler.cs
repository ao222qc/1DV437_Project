using _1DV437_NeilArmstrong.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class UnitHandler : ObservableUnitHandler
    {

        List<Unit> toRemove = new List<Unit>();
        public UnitHandler()
        {

        }

        /* *
         * Param 1: Unit (abstract class) implemented by EnemyShip, PlayerShip, Projectile
         * Param 2: Amount of units to add
         * Handles: Adding Units at runtime
         * */
        public override void AddUnit(Unit unit, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                unitList.Add(unit);
            }
            NotifyObservers();
        }

        public bool EnemiesDead()
        {
            List<Projectile> projectilesToRemove = new List<Projectile>();
            int index = 0;
            int playerIndex = 0;

            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i] is EnemyShip)
                {
                    index++;
                }
                else if (unitList[i] is PlayerShip)
                {
                    playerIndex++;
                }
                else if(unitList[i] is Boss)
                {
                    index++;
                }
            }

            if (index == 0)
            {               
                return true;
            }
            return false;
        }

        public void ClearProjectiles()
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i] is Projectile)
                {
                    if (unitList[i].GetPosition().Y > 1 || unitList[i].GetPosition().Y < 0)
                    {
                        toRemove.Add(unitList[i]);
                        Console.WriteLine("togs bort");
                        break;
                    }
                }
            }
        }

        public void ClearList()
        {
            unitList.Clear();

            toRemove.Clear();

            NotifyObservers();
        }

        public void RemoveUnit(List<Unit> unitsToRemove)
        {
            unitList.RemoveAll(x => unitsToRemove.Contains(x));

            NotifyObservers();
        }

        public override void AddObserver(Observer obs)
        {
            observers.Add(obs);
        }

        public override void NotifyObservers()
        {
            //foreach (Observer obs in observers)
           // {
                for (int i = 0; i < observers.Count; i++)
                {
                    observers[i].UpdateList(unitList);
                }
                //obs.UpdateList(unitList);
           // }


        }
    }
}
