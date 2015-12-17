using _1DV437_NeilArmstrong.Model;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    abstract class Observer
    {
        public abstract void UpdateList(List<Unit> unitList);
    }
}
