using _1DV437_NeilArmstrong.Model;
using _1DV437_NeilArmstrong.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Controller
{
    class EnemyController : MasterController
    {

        UnitHandler unitHandler;
        List<Projectile> projectileList;
        GameView gameView;
        float time;
        int tick;
        EnemyShip ship;
        bool ableToShoot;

        public EnemyController(UnitHandler unitHandler, GameView gameView)
        {
            ship = new EnemyShip(new Random(), 1);
            projectileList = new List<Projectile>();
            this.unitHandler = unitHandler;
            this.gameView = gameView;
        }
        public void Update(float totalSeconds, List<EnemyShip> enemyShipList)
        {
            foreach (EnemyShip enemyShip in enemyShipList)
            {
                if (enemyShip.IsDead == false)
                {
                    if (enemyShip.AbleToShoot(totalSeconds))
                    {

                        projectileList.Add(new Projectile(enemyShip.GetPosition(), 0f, this));
                        enemyShip.Shoot(unitHandler, projectileList[projectileList.Count - 1]);
                        gameView.PlayEnemyFireSound();
                    }
                    
                    enemyShip.Move(totalSeconds, 0f);
                }
            }


            for (int i = projectileList.Count - 1; i >= 0; i--)
            {
                projectileList[i].MoveProjectile(totalSeconds, 0f);
                if (projectileList[i].isDead)
                    projectileList.RemoveAt(i);
            }


        }

        public void UpdateBoss(float totalSeconds, Boss boss)
        {

            if (boss.IsDead == false)
            {
                if (boss.AbleToShoot(totalSeconds))
                {
                    projectileList.Add(new Projectile(boss.GetPosition(), 0f, this));
                    boss.Shoot(unitHandler, projectileList[projectileList.Count - 1]);
                    gameView.PlayEnemyFireSound();
                }
            }

            for (int i = 0; i < projectileList.Count; i++)
            {
                projectileList[i].MoveProjectile(totalSeconds, 0f);
            }

            boss.Move(totalSeconds, 0f);
        }

        public override void Update(float totalSeconds)
        {
            throw new NotImplementedException();
        }
    }
}
