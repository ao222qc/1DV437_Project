using _1DV437_NeilArmstrong.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class CollisionHandler : Observer
    {
        List<Unit> unitList = new List<Unit>();
        List<EnemyShip> enemyList = new List<EnemyShip>();
        List<Projectile> projectileList = new List<Projectile>();
        List<PlayerShip> playerShipList = new List<PlayerShip>();
        GameView gameView;

        public CollisionHandler(GameView gameView)
        {
            this.gameView = gameView;
        }

        public override void UpdateList(List<Unit> unitList)
        {
            this.unitList = unitList;

            foreach (Unit unit in this.unitList)
            {
                if (unit is EnemyShip)
                {
                    enemyList.Add(unit as EnemyShip);
                }
                else if (unit is Projectile)
                {
                    projectileList.Add((unitList[unitList.Count - 1] as Projectile));
                }
                else if (unit is PlayerShip)
                {
                    playerShipList.Add(unit as PlayerShip);
                }
            }
        }

        public void Collision()
        {
            foreach (Unit unit in unitList)
            {
                if (unit is PlayerShip)
                {
                    PlayerWallCollision((unit as PlayerShip));
                }
                else if (unit is Projectile)
                {
                    ProjectileHitCollision((unit as Projectile));
                }
            }
        }


        public void PlayerWallCollision(PlayerShip playerShip)
        {
            if (playerShip.GetPosition().X >= 1 || playerShip.GetPosition().X <= 0)
            {
                playerShip.Bounce();
            }
        }

        public void ProjectileHitCollision(Projectile projectile)
        {
            float pPositionX = projectile.GetPosition().X;
            float pPositionY = projectile.GetPosition().Y;

            if (projectile.ProjectileType == Model.ProjectileType.Player)
            {
                foreach (EnemyShip enemy in enemyList)
                {
                    float enemyPosX = enemy.GetPosition().X;
                    float enemyPosY = enemy.GetPosition().Y;

                    if (pPositionX - projectile.Radius >= enemyPosX - enemy.Radius && pPositionX + projectile.Radius <= enemyPosX + enemy.Radius
                        && pPositionY - projectile.Radius >= enemyPosY - enemy.Radius && pPositionY + projectile.Radius <= enemyPosY + enemy.Radius)
                    {
                        Console.WriteLine("Enemy hit!");
                        enemy.Hit();
                        projectile.Kill();
                        break;
                    }
                }
            }
            else if (projectile.ProjectileType == Model.ProjectileType.Enemy)
            {              
                foreach (PlayerShip player in playerShipList)
                {                   
                    float playerPosX = player.GetPosition().X;
                    float playerPosY = player.GetPosition().Y;

                    if(pPositionX - projectile.Radius >= playerPosX - player.Radius && pPositionX + projectile.Radius <= playerPosX + player.Radius
                        && pPositionY >= playerPosY - player.Radius && pPositionY <= playerPosY + player.Radius)                 
                    {
                        projectile.Kill();
                        player.Hit();
                        Console.WriteLine("Player hit!");
                        break;
                    }
                    


                }
                

                
            }
            
          

        }
    }
}
