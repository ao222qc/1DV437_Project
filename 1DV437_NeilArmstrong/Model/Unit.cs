using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    abstract class Unit
    {
        protected Vector2 position;
        protected int hitPoints;
        protected float speed;
        protected float fireDelay;
        public bool isDead;

        public abstract void Move(float totalSeconds, float direction);

        public abstract Vector2 GetPosition();

        public abstract void Kill();

    }
}
