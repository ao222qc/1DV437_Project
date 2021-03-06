﻿using _1DV437_NeilArmstrong.View;
using Microsoft.Xna.Framework;
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
            for (int i = 0; i < this.unitList.Count; i++)
            {
                if (unitList[i] is EnemyShip)
                {
                    enemyList.Add(unitList[i] as EnemyShip);
                }
                else if (unitList[i] is Projectile)
                {
                    projectileList.Add((unitList[unitList.Count - 1] as Projectile));
                    
                }
                else if (unitList[i] is PlayerShip)
                {
                    playerShipList.Add(unitList[i] as PlayerShip);
                }
                else if (unitList[i] is Boss)
                {
                    bossList.Add(unitList[i] as Boss);
                }
            }
        }

        public void Collision()
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i] is PlayerShip)
                {
                    PlayerWallCollision((unitList[i] as PlayerShip));
                }
                else if (unitList[i] is Projectile)
                {
                    if (unitList[i].GetPosition().Y < 0 || unitList[i].GetPosition().Y > 1)
                    {
                        unitList[i].isDead = true;
                        toRemove.Add(unitList[i] as Projectile);
                    }
                    else
                    {
                        ProjectileHitCollision((unitList[i] as Projectile));
                    }
                }
                else if (unitList[i] is EnemyShip)
                {
                    EnemyOutOfBounds(unitList[i] as EnemyShip);
                }
            }
            RemoveUnits();
        }

        public void RemoveUnits()
        {
            enemyList.RemoveAll(x => toRemove.Contains(x));
            playerShipList.RemoveAll(x => toRemove.Contains(x));
            projectileList.RemoveAll(x => toRemove.Contains(x));
            bossList.RemoveAll(x => toRemove.Contains(x));
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

        /*
         * If enemy reaches lowest end of screen
         * it dies and does 2 damage to player
         */
        public void EnemyOutOfBounds(EnemyShip enemyShip)
        {                     
            if (enemyShip.GetPosition().Y > 1)
            {
                gameView.PlayShipExplodingSound();
                gameView.DrawOnDeathAnimation(enemyShip.GetPosition());
                toRemove.Add(enemyShip);
                playerShipList[0].Hit();
                playerShipList[0].Hit();
            }
        }

        public void ProjectileHitCollision(Projectile projectile)
        {
            float pPositionX = projectile.GetPosition().X;
            float pPositionY = projectile.GetPosition().Y;

            if (projectile.ProjectileType == Model.ProjectileType.Player)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    float enemyPosX = enemyList[i].GetPosition().X;
                    float enemyPosY = enemyList[i].GetPosition().Y;

                    if (pPositionX - projectile.Radius >= enemyPosX - enemyList[i].Radius && pPositionX + projectile.Radius <= enemyPosX + enemyList[i].Radius
                        && pPositionY - projectile.Radius >= enemyPosY - enemyList[i].Radius && pPositionY + projectile.Radius <= enemyPosY + enemyList[i].Radius)
                    {
                        enemyList[i].Hit();
                        if (enemyList[i].IsDead)
                        {
                            gameView.PlayShipExplodingSound();
                            gameView.DrawOnDeathAnimation(enemyList[i].GetPosition());
                            toRemove.Add(enemyList[i]);
                        }
                        toRemove.Add(projectile);
                        break;
                    }
                }

                for (int i = 0; i < bossList.Count; i++)
                {
                    float bossPosX = bossList[i].GetPosition().X;
                    float bossPosY = bossList[i].GetPosition().Y;

                    if (pPositionX - projectile.Radius >= bossPosX - bossList[i].Radius && pPositionX + projectile.Radius <= bossPosX + bossList[i].Radius
                        && pPositionY - projectile.Radius >= bossPosY - bossList[i].Radius && pPositionY + projectile.Radius <= bossPosY + bossList[i].Radius)
                    {
                        bossList[i].Hit();
                        if (bossList[i].IsDead)
                        {
                            gameView.DrawOnDeathAnimation(bossList[i].GetPosition());
                            gameView.PlayShipExplodingSound();
                            toRemove.Add(bossList[i]);
                        }
                        toRemove.Add(projectile);
                        break;
                    }
                }
            }
            else if (projectile.ProjectileType == Model.ProjectileType.Enemy)
            {
                for (int i = 0; i < playerShipList.Count; i++)
                {
                    float playerPosX = playerShipList[i].GetPosition().X;
                    float playerPosY = playerShipList[i].GetPosition().Y;

                    if (pPositionX - projectile.Radius >= playerPosX - playerShipList[i].Radius && pPositionX + projectile.Radius <= playerPosX + playerShipList[i].Radius
                        && pPositionY >= playerPosY - playerShipList[i].Radius && pPositionY <= playerPosY + playerShipList[i].Radius)
                    {
                        toRemove.Add(projectile);
                        playerShipList[i].Hit();
                        if (playerShipList[i].IsDead)
                        {
                            gameView.DrawOnDeathAnimation(playerShipList[i].GetPosition());
                            gameView.PlayShipExplodingSound();
                            toRemove.Add(playerShipList[i]);
                        }
                        break;
                    }
                }
            }
        }
    }
}
