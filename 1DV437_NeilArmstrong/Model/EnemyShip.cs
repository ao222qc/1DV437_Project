using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class EnemyShip : Unit
    {
        Vector2 gravity;
        Random rand;
        float time;
        float scale = 0.3f;
        float radius;
        float randomMovement;

        public EnemyShip(Random rand)
        {
            radius = scale / 2;
            this.rand = rand;
            position = new Vector2(0.5f, 0.1f);
            randomMovement = (float)rand.NextDouble() - 0.5f;
            randomMovement = randomMovement * 0.25f;
            gravity = new Vector2(0f, 0.03f);
            hitPoints = 3;
            fireDelay = 1.4f;
        }

        public bool IsDead
        {
            get { return this.isDead; }
            set { this.isDead = value; }
        }

        
        public override void Move(float totalSeconds, float direction)
        {       
            position.X += randomMovement * totalSeconds;
            if (position.X > 0.95f || position.X < 0.05f)
            {
                randomMovement = -randomMovement;
            }
            position.Y += gravity.Y * totalSeconds;
        }

        public float Scale
        {
            get { return scale; }
        }

        public float Radius    
        {
            get { return radius; }
        }

        public bool AbleToShoot(float totalSeconds)
        {
            time += totalSeconds;

            if (time >= fireDelay)
            {
                time = 0;
                return true;             
            }
            return false;
        }

        public void Shoot(UnitHandler unitHandler, Projectile p)
        {
            unitHandler.AddUnit(p, 1);
        }

        public override Vector2 GetPosition()
        {
            return position;
        }
        public void Hit()
        {
            this.hitPoints = this.hitPoints - 1;
            if (this.hitPoints <= 0)
            {
                this.Kill();
                IsDead = true;
            }
        }
        public override void Kill()
        {
            gravity.Y = 0f;
            randomMovement = 0;
            
        }
    }
}
