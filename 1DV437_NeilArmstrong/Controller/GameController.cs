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
    public enum GameState
    {
        EnemyWave,
        Bossfight
    }


    class GameController : MasterController
    {
        GameState gameState;
        List<EnemyShip> enemyShipList;
        List<Boss> bossList;
        CollisionHandler collisionHandler;
        List<MasterController> controllerList;
        PlayerController playerController;
        EnemyController enemyController;
        GameView gameView;
        UnitHandler unitHandler;
        PlayerShip playerShip;
        Random rand;
        int amountOfEnemies;
        int wave = 0;

        public GameController(ContentManager content, Camera camera, GraphicsDevice graphics, GameView gameView)
        {
            gameState = GameState.EnemyWave;
            bossList = new List<Boss>();
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

        public void InitiateEnemyWave(int amountOfEnemies)
        {
            unitHandler.AddUnit(playerShip, 1);
            enemyShipList.Clear();
            for (int i = 1; i < amountOfEnemies; i++)
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
                    foreach (Boss b in bossList)
                    {
                        (c as EnemyController).UpdateBoss(totalSeconds, b);
                    }
                    if (unitHandler.EnemiesDead() && gameState == GameState.EnemyWave)
                    {
                        wave += 1;
                        amountOfEnemies += 1;
                        InitiateEnemyWave(amountOfEnemies);
                    }
                    if (wave == 2 && gameState != GameState.Bossfight)
                    {
                        gameState = GameState.Bossfight;
                        InitiateEnemyBoss();
                    }
                }
            }
        }

        public void InitiateEnemyBoss()
        {           

            bossList.Add(new Boss(rand));

            foreach (Boss b in bossList)
            {
                unitHandler.AddUnit(b, 1);
            }
            enemyShipList.Clear();
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
