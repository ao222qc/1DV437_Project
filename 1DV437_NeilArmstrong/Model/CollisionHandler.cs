using _1DV437_NeilArmstrong.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Model
{
    class CollisionHandler : Observer
    {
        List<Unit> toRemove = new List<Unit>();
        List<Unit> unitList = new List<Unit>();
        List<EnemyShip> enemyList = new List<EnemyShip>();
        List<Projectile> projectileList = new List<Projectile>();
        List<PlayerShip> playerShipList = new List<PlayerShip>();
        List<Boss> bossList = new List<Boss>();
        GameView gameView;
        UnitHandler unitHandler;

        public CollisionHandler(GameView gameView, UnitHandler unitHandler)
        {
            this.gameView = gameView;
            this.unitHandler = unitHandler;
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
                else if (unit is Boss)
                {
                    bossList.Add(unit as Boss);
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
            RemoveUnits();
        }

        public void RemoveUnits()
        {
            enemyList.RemoveAll(x => toRemove.Contains(x));
            playerShipList.RemoveAll(x => toRemove.Contains(x));
            projectileList.RemoveAll(x => toRemove.Contains(x));
            unitHandler.RemoveUnit(toRemove);
            toRemove.Clear();
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
                        if (enemy.IsDead)
                        {
                            toRemove.Add(enemy);
                        }
                        toRemove.Add(projectile);
                        break;
                    }
                }

                foreach (Boss boss in bossList)
                {
                    float bossPosX = boss.GetPosition().X;
                    float bossPosY = boss.GetPosition().Y;

                    if (pPositionX - projectile.Radius >= bossPosX - boss.Radius && pPositionX + projectile.Radius <= bossPosX + boss.Radius
                        && pPositionY - projectile.Radius >= bossPosY - boss.Radius && pPositionY + projectile.Radius <= bossPosY + boss.Radius)
                    {
                        Console.WriteLine("boss hit");
                        boss.Hit();
                        if (boss.IsDead)
                        {
                            toRemove.Add(boss);
                        }
                        toRemove.Add(projectile);
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

                    if (pPositionX - projectile.Radius >= playerPosX - player.Radius && pPositionX + projectile.Radius <= playerPosX + player.Radius
                        && pPositionY >= playerPosY - player.Radius && pPositionY <= playerPosY + player.Radius)
                    {
                        toRemove.Add(projectile);
                        player.Hit();
                        if (player.IsDead)
                        {
                            toRemove.Add(player);
                        }
                        Console.WriteLine("Player hit!");
                        break;
                    }
                }
            }
            
        }
    }
}
