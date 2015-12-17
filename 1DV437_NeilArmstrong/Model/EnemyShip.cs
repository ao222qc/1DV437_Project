using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class EnemyShip : Unit
    {
        Vector2 randomDirection;
        Vector2 gravity;
        Random rand;
        float time;

        public EnemyShip(Random rand)
        {
            this.rand = rand;
            position = new Vector2(0.5f, 0.1f);
            gravity = new Vector2(0f, 0.05f);
        }
        public override void Move(float totalSeconds, float direction)
        {
            time += totalSeconds;

            position.X += speed * totalSeconds;
            position.Y += gravity.Y * totalSeconds;
        }

        public override Vector2 GetPosition()
        {
            return position;
        }

        public override void Kill()
        {

        }

        public override void Shoot()
        {

        }

    }
}
