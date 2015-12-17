using _1DV437_NeilArmstrong.Model;
using _1DV437_NeilArmstrong.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Controller
{
    class GameController : Controller
    {
        List<Controller> controllerList;
        PlayerController playerController;
        EnemyController enemyController;
        GameView gameView;
        ShipHandler shipHandler;
        PlayerShip playerShip;
        EnemyShip enemyShip;
        Random rand;

        public GameController(ContentManager content, Camera camera, GraphicsDevice graphics)
        {
            rand = new Random();
            gameView = new GameView();
            shipHandler = new ShipHandler();
            playerShip = new PlayerShip();
            enemyShip = new EnemyShip(rand);
            playerController = new PlayerController();
            enemyController = new EnemyController();

            controllerList = new List<Controller>();

            controllerList.Add(playerController);
            controllerList.Add(enemyController);

            gameView.Initiate(content, camera, graphics);

            shipHandler.AddObserver(gameView);
            shipHandler.AddUnit(playerShip, 1);
            shipHandler.AddUnit(enemyShip, 1);          
        }

        public override void Update(float totalSeconds)
        {
            foreach (Controller c in controllerList)
            {
                if (c is PlayerController)
                {
                    (c as PlayerController).Update(totalSeconds, playerShip);
                    continue;
                }
                else if (c is EnemyController)
                {
                    (c as EnemyController).Update(totalSeconds, enemyShip);
                }
                 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 vec = new Vector2(0.5f, 0.85f);

            spriteBatch.Begin();
            gameView.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
