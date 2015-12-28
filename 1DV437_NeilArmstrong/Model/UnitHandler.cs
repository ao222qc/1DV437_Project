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
            int index = 0;
            foreach (Unit unit in unitList)
            {
                if (unit is EnemyShip)
                {
                    index++;
                }
            }
            if (index == 0)
            {
                ClearList();
                return true;
            }
            return false;
        }

        public void ClearList()
        {

            unitList.Clear();
            /*foreach (Unit unit in unitList)
            {
                if (unit is EnemyShip)
                {
                    toRemove.Add(unit as EnemyShip);
                }
                else if (unit is Projectile)
                {
                    toRemove.Add(unit as Projectile);
                }               
            }
            unitList.RemoveAll(x => toRemove.Contains(x));*/
            toRemove.Clear();
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
            foreach (Observer obs in observers)
            {
                obs.UpdateList(unitList);
            }
        }
    }
}
