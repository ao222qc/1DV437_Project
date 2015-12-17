using _1DV437_NeilArmstrong.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class ShipHandler : UnitHandler
    {
        public ShipHandler()
        {
            
        }

        public override void AddUnit(Unit unit, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                unitList.Add(unit);
            }
           
             NotifyObservers();
        }

        public override void AddObserver(View.Observer obs)
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
