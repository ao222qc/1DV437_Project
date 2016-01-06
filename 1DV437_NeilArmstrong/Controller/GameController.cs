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
using System.Threading;

namespace _1DV437_NeilArmstrong.Controller
{
    public enum GameState
    {
        ShowCurrentWave,
        EnemyWave,
        Bossfight,
        GameOver,
        GameFinished
    }


    class GameController : MasterController
    {
        ShowStats showStats;
        ExplosionAnimationHandler explosionHandler;
        ParticleAnimationHandler particleHandler;
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
        int level = 1;
        float time;
        Game1 game1;

        public void ClearShit()
        {
            enemyShipList.Clear();
            unitHandler.ClearList();

        }

        public GameController(ContentManager content, Camera camera, GraphicsDevice graphics, GameView gameView, Game1 game1)
        {
            particleHandler = new ParticleAnimationHandler(content);
            this.game1 = game1;
            explosionHandler = new ExplosionAnimationHandler();
            gameState = GameState.ShowCurrentWave;
            bossList = new List<Boss>();
            enemyShipList = new List<EnemyShip>();
            rand = new Random();
            this.gameView = gameView;
            gameView.Initiate(content, camera, graphics, explosionHandler, particleHandler);
            unitHandler = new UnitHandler();
            playerShip = new PlayerShip();
            collisionHandler = new CollisionHandler(gameView, unitHandler);
            amountOfEnemies = 0;

            showStats = new ShowStats(playerShip, camera, this, collisionHandler);
            showStats.LoadContent(content);

            playerController = new PlayerController(unitHandler, gameView);
            enemyController = new EnemyController(unitHandler, gameView);
            controllerList = new List<MasterController>();

            unitHandler.AddUnit(playerShip, 1);
            controllerList.Add(playerController);
            controllerList.Add(enemyController);

            unitHandler.AddObserver(gameView);
            unitHandler.AddObserver(collisionHandler);
        }

       

        /*
         * Updates player, enemies, projectiles
         * Checks collision for each frame
         * Updates animations foreach frame
         * Keeps track of game progress by
         * using UnitHandler class
         */
        public override void Update(float totalSeconds)
        {          
            time += totalSeconds;

            if (gameState == GameState.ShowCurrentWave && time > 2.5f)
            {
                gameState = GameState.EnemyWave;
                time = 0;
            }
            else if (gameState == GameState.EnemyWave || gameState == GameState.Bossfight)
            {
                particleHandler.Update(totalSeconds);
                explosionHandler.Update(totalSeconds);
                collisionHandler.Collision();

                for (int i = 0; i < controllerList.Count; i++)
                {
                    if (controllerList[i] is PlayerController)
                    {
                        (controllerList[i] as PlayerController).Update(totalSeconds, playerShip);
                        continue;
                    }
                    else if (controllerList[i] is EnemyController)
                    {
                        for (int j = 0; j < enemyShipList.Count; j++)
                        {
                            (controllerList[i] as EnemyController).Update(totalSeconds, enemyShipList[j]);
                        }

                        for (int k = 0; k < bossList.Count; k++)
                        {
                            (controllerList[i] as EnemyController).UpdateBoss(totalSeconds, bossList[k]);
                        }

                        if (gameState == GameState.Bossfight && unitHandler.EnemiesDead())
                        {
                            gameState = GameState.GameFinished;
                        }

                        if (playerShip.PlayerDead())
                        {
                            gameState = GameState.GameOver;
                            
                        }

                        if (unitHandler.EnemiesDead() && gameState == GameState.EnemyWave)
                        {                           
                            Sleep();
                            wave += 1;
                            amountOfEnemies += 1;

                            if (amountOfEnemies == 7)
                            {
                                amountOfEnemies = 1;
                                level += 1;
                                gameState = GameState.ShowCurrentWave;
                                time = 0;
                            }
                            if (level == 4)
                            {
                                gameState = GameState.Bossfight;
                                InitiateEnemyBoss();
                            }

                            InitiateEnemyWave(amountOfEnemies);
                        }
                    }
                }
            }
        }

        public GameState GetGameState()
        {
            return gameState;
        }

        /*
         * Short sleep called between waves
         * to be able to clear lists etc
         * without problems
         */
        public void Sleep()
        {
            Thread.Sleep(1);
        }
        /*
        * Clears enemies from list and initiates
        * new enemy ship to the UnitHandler
        * and adds them to local list
        * for updating etc
        */
        public void InitiateEnemyWave(int amount)
        {
            //ClearShit();
            //unitHandler.AddUnit(playerShip, 1);
            Sleep();
            enemyShipList.Clear();
            for (int i = 1; i < amount; i++)
            {
                enemyShipList.Add(new EnemyShip(rand, level));
            }

            for (int i = 0; i < enemyShipList.Count; i++)
            {
                unitHandler.AddUnit(enemyShipList[i], 1);
            }
        }

        public void InitiateEnemyBoss()
        {
            Sleep();
            enemyShipList.Clear();

            //unitHandler.AddUnit(playerShip, 1);

            bossList.Add(new Boss(rand));

            for (int i = 0; i < bossList.Count; i++)
            {
                unitHandler.AddUnit(bossList[i], 1);
            }

        }

     

        public void DrawGameOverScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            showStats.ShowGameOver(spriteBatch);
            spriteBatch.End();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            gameView.Draw(spriteBatch);
            showStats.Draw(spriteBatch);

            if (gameState == GameState.ShowCurrentWave)
            {
                showStats.ShowLevel(spriteBatch, level);
            }

            spriteBatch.End();
        }
    }
}
