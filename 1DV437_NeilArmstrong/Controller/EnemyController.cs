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

        public EnemyController(UnitHandler unitHandler, GameView gameView)
        {
            projectileList = new List<Projectile>();
            this.unitHandler = unitHandler;
            this.gameView = gameView;
        }
        public void Update(float totalSeconds, EnemyShip enemyShip)
        {
            if (enemyShip.IsDead == false)
            {
                if (enemyShip.AbleToShoot(totalSeconds))
                {
                    projectileList.Add(new Projectile(enemyShip.GetPosition(), 0f, this));
                    enemyShip.Shoot(unitHandler, projectileList[projectileList.Count - 1]);
                    gameView.PlayEnemyFireSound();
                }
            }

            for (int i = 0; i < projectileList.Count; i++)
            {
                projectileList[i].MoveProjectile(totalSeconds, 0f);
            }

            enemyShip.Move(totalSeconds, 0f);
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
