using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class Boss : Unit
    {
        Vector2 gravity;
        Random rand;
        float time;
        float scale = 0.3f;
        float radius;
        float movement;
        bool bossGotHit;

        public Boss(Random rand)
        {
            IsDead = false;
            radius = scale / 2;
            this.rand = rand;
            position = new Vector2(0.5f, 0.1f);
            movement = 0.2f;
            gravity = new Vector2(0f, 0.02f);
            hitPoints = 15;
            fireDelay = 0.8f;
        }
        public override void Move(float totalSeconds, float direction)
        {
            position.X += movement * totalSeconds;
            if (position.X > 0.9f || position.X < 0.1f)
            {
                movement = -movement;
            }
            position.Y += gravity.Y * totalSeconds;
        }
        public float Radius
        {
            get { return radius; }
        }
        public bool BossGotHit()
        {
            if (bossGotHit)
            {
                bossGotHit = false;
                return true;
            }
            return false;
        }

        public float Scale
        {
            get { return scale; }
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
            bossGotHit = true;
            if (this.hitPoints <= 0)
            {
                this.Kill();
                IsDead = true;
            }
        }

        public bool IsDead
        {
            get { return this.isDead; }
            set { this.isDead = value; }
        }

        public override void Kill()
        {
            gravity.Y = 0f;
            movement = 0;
        }
    }
}
