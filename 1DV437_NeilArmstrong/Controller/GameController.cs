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
    class GameController : MasterController
    {
        List<EnemyShip> enemyShipList;
        CollisionHandler collisionHandler;
        List<MasterController> controllerList;
        PlayerController playerController;
        EnemyController enemyController;
        GameView gameView;
        UnitHandler unitHandler;
        PlayerShip playerShip;
        Random rand;
        int amountOfEnemies;

        public GameController(ContentManager content, Camera camera, GraphicsDevice graphics, GameView gameView)
        {
            enemyShipList = new List<EnemyShip>();
            rand = new Random();
            this.gameView = gameView;
            gameView.Initiate(content, camera, graphics);
            unitHandler = new UnitHandler();
            playerShip = new PlayerShip();
            collisionHandler = new CollisionHandler(gameView, unitHandler);
            amountOfEnemies = 3;

            playerController = new PlayerController(unitHandler);
            enemyController = new EnemyController(unitHandler);
            controllerList = new List<MasterController>();

            controllerList.Add(playerController);
            controllerList.Add(enemyController);

            unitHandler.AddObserver(gameView);
            unitHandler.AddObserver(collisionHandler);

        }

        public void InitiateGame(int amountOfEnemies)
        {
            unitHandler.AddUnit(playerShip, 1);
            enemyShipList.Clear();
            for (int i = 0; i < amountOfEnemies; i++)
            {
                enemyShipList.Add(new EnemyShip(rand));
            }

            foreach (EnemyShip es in enemyShipList)
            {
                unitHandler.AddUnit(es, 1);
            }
        }

        public override void Update(float totalSeconds)
        {
            collisionHandler.Collision();
            foreach (MasterController c in controllerList)
            {
                if (c is PlayerController)
                {
                    (c as PlayerController).Update(totalSeconds, playerShip);

                    continue;
                }
                else if (c is EnemyController)
                {
                    foreach (EnemyShip es in enemyShipList)
                    {
                        (c as EnemyController).Update(totalSeconds, es);
                    }
                    if (unitHandler.EnemiesDead())
                    {
                        amountOfEnemies += 1;
                        InitiateGame(amountOfEnemies);
                    }
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
