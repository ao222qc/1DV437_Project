using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class PlayerShip
    {
        Vector2 position;
        Vector2 speed;

       /*       
        float damage;
        float fireRate;
        float speed;
         */

        public PlayerShip()
        {
            position = new Vector2(0.5f, 0.8f);
            speed = new Vector2(0.8f, 1f);
        }

        public float GetShipSpeed()
        {
            return speed.X;
        }

        public void Move(float totalSeconds, float direction)
        {
            position.X += direction * speed.X * totalSeconds;
        }

        public Vector2 GetShipPosition()
        {
            return position;
        }

    }
}
