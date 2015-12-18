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
        float timer;
        float direction;
        float scale = 0.25f;
        float radius;
       
        public PlayerShip()
        {
            radius = scale / 2;
            position = new Vector2(0.5f, 0.9f);
            speed = 0.8f;
            fireDelay = 0.3f;
            hitPoints = 5;
        }

        public float GetShipSpeed()
        {
            return speed;
        }
        public void Hit()
        {
            hitPoints -= 1;
            if (hitPoints <= 0)
            {
                Kill();
            }
        }

        public float Scale
        {
            get { return scale; }
        }
        public float Radius
        {
            get { return radius; }
        }

        public override void Kill()
        {
            speed = 0f ;
        }

        public override void Move(float totalSeconds, float direction)
        {
            this.direction = direction;
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

        public void Bounce()
        {
            position.Y = 0.2f;
        }

        public void Shoot(UnitHandler unitHandler, Projectile p, float totalSeconds)
        {
            timer += totalSeconds;

            if (timer >= fireDelay)
            {
                unitHandler.AddUnit(p, 1);
                timer = 0;
            }
        }

    }
}
