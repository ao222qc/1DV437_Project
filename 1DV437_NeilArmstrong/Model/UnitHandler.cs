using _1DV437_NeilArmstrong.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    abstract class UnitHandler
    {
        protected List<Unit> unitList = new List<Unit>();

        protected List<Observer> observers = new List<Observer>();

        public abstract void AddUnit(Unit unit, int amount);

        public abstract void NotifyObservers();

        public abstract void AddObserver(Observer observer);

    }
}
