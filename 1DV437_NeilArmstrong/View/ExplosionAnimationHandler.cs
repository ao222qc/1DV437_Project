using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    class ExplosionAnimationHandler
    {
        List<ExplosionAnimation> explosionList = new List<ExplosionAnimation>();

        public ExplosionAnimationHandler()
        {

        }

        public void AddExplosion(Vector2 startPosition)
        {
            explosionList.Add(new ExplosionAnimation(startPosition));
        }

        public void Update(float totalSeconds)
        {
            foreach (ExplosionAnimation ea in explosionList)
            {
                ea.Update(totalSeconds);
            }
        }

    }
}

