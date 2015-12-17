using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class PlayerShip : Unit
    {
        float rotation;
       
        public PlayerShip()
        {
            position = new Vector2(0.5f, 0.8f);
            speed = 0.8f;
            fireRate = 0.0f;
            hitPoints = 3;
        }

        public float GetShipSpeed()
        {
            return speed;
        }

        public override void Kill()
        {
            throw new NotImplementedException();
        }

        public override void Move(float totalSeconds, float direction)
        {
            position.X += direction * speed * totalSeconds;
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public override Vector2 GetPosition()
        {
            return position;
        }

        public override void Shoot()
        {
            
        }


    }
}
