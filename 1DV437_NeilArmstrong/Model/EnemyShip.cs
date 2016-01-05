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
        float scale;
        float radius;
        float randomMovement;
        bool enemyGotHit;
        int ticks;

        public EnemyShip(Random rand, int level)
        {
            scale = 0.3f;
            radius = scale / 2;
            if (level == 1)
            {                
                hitPoints = 1;
                fireDelay = 1.4f;
            }
            else if (level == 2)
            {
                hitPoints = 2;
                fireDelay = 1.2f;
            }
            else if (level == 3)
            {
                hitPoints = 3;
                fireDelay = 1f;
            }

            this.rand = rand;
            position = new Vector2(0.5f, 0.1f);
            //1-1.5
            randomMovement = (float)rand.NextDouble() - 0.5f;
            //randomMovement = randomMovement * 0.3f;
            gravity = new Vector2(0f, 0.03f);

        }
       

        public bool EnemyGotHit()
        {
            if (enemyGotHit)
            {
                enemyGotHit = false;
                return true;
            }
            return false;
        }

        public bool IsDead
        {
            get { return this.isDead; }
            set { this.isDead = value; }
        }


        public override void Move(float totalSeconds, float direction)
        {
            ticks++;
            position.X = (0.4f * (float)Math.Sin(randomMovement * ticks * 0.5 * Math.PI/30)) + 0.51f;


            //position.X += randomMovement * totalSeconds;
            //if (position.X > 0.95f || position.X < 0.05f)
            //{
            //    randomMovement = -randomMovement;
            //}
            position.Y += gravity.Y * totalSeconds;
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public float Radius
        {
            get { return radius; }
        }

        public bool AbleToShoot(float totalSeconds)
        {
            time += totalSeconds;

            fireDelay = (float)rand.NextDouble()  +1.2f;

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
            enemyGotHit = true;
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