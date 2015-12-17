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
    class EnemyController : Controller
    {
        Texture2D enemyShipTexture;
       
       // EnemyShip enemyShip;

        public EnemyController()
        {
            //enemyShipTexture = content.Load<Texture2D>("enemyspaceship");
            //enemyShip = new EnemyShip();
            //shipHandler.AddUnit(enemyShip, 1);

           // this.camera = camera;
        }
        public void Update(float totalSeconds, EnemyShip enemyShip)
        {
            enemyShip.Move(totalSeconds, 0f);
        }

        public override void Update(float totalSeconds)
        {
            throw new NotImplementedException();
        }
    }
}
