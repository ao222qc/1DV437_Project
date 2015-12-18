
using _1DV437_NeilArmstrong.Controller;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    public enum ProjectileType
    {
        Player,
        Enemy
    };

    class Projectile : Unit
    {
        float angle;
        float projectileSpeed;
        float scale = 0.2f;
        float radius;
        ProjectileType projectileType;



        public Projectile(Vector2 startPosition, float angle, MasterController controller)
        {
            //When created - decide which controller created it
            //if by player, won't hurt player (collision)
            //also helps set movement logic

            if (controller is PlayerController)
            {
                projectileType = Model.ProjectileType.Player;
            }
            else if (controller is EnemyController)
            {
                projectileType = Model.ProjectileType.Enemy;
            }
           
            radius = scale / 2;
            position = startPosition;
            this.angle = angle;
            projectileSpeed = -0.4f;
            hitPoints = 3;
        }

        public ProjectileType ProjectileType
        {
            get {return this.projectileType; }
        }

        public float Scale
        {
            get { return scale; }
        }
        public float Radius
        {
            get { return radius; }
        }

        public void MoveProjectile(float totalSeconds, float direction )
        {

            if (this.projectileType == Model.ProjectileType.Player)
            {
                position.Y += projectileSpeed * totalSeconds;
            }
            else if (this.projectileType == Model.ProjectileType.Enemy)
            {
                position.Y -= projectileSpeed * totalSeconds;
            }
            direction = angle;
           // position.Y += projectileSpeed * totalSeconds;
            position.X += direction * totalSeconds;          
        }

        public override Microsoft.Xna.Framework.Vector2 GetPosition()
        {
            return position;
        }

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public override void Kill()
        {
            this.position = new Vector2(1f, 1f);
        }

        public override void Move(float totalSeconds, float direction)
        {
            throw new NotImplementedException();
        }
    }
}
