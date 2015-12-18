﻿using _1DV437_NeilArmstrong.Model;
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

        public EnemyController(UnitHandler unitHandler)
        {
            projectileList = new List<Projectile>();
            this.unitHandler = unitHandler;
        }
        public void Update(float totalSeconds, EnemyShip enemyShip)
        {
            if (enemyShip.AbleToShoot(totalSeconds))
            {
                projectileList.Add(new Projectile(enemyShip.GetPosition(), 0f, this));
                enemyShip.Shoot(unitHandler, projectileList[projectileList.Count-1]);
            }
            foreach (Projectile p in projectileList)
            {
                p.MoveProjectile(totalSeconds, 0f);
            }

            enemyShip.Move(totalSeconds, 0f);
        }

        public override void Update(float totalSeconds)
        {
            throw new NotImplementedException();
        }
    }
}
