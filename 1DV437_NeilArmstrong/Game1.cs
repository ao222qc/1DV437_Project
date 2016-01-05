using _1DV437_NeilArmstrong.Controller;
using _1DV437_NeilArmstrong.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace _1DV437_NeilArmstrong
{
    public enum GameState
    {
        MenuScreen,
        Game,
        Paused
    };


    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        GameController gameController;
        MenuController menuController;
        public GameState gameState;
        bool pauseKeyDown;
        bool pauseKeyDownThisFrame;


        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 541;
            Content.RootDirectory = "Content";
            gameState = GameState.MenuScreen;
            IsMouseVisible = true;
            pauseKeyDown = false;
            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
    
            // TODO: use this.Content to load your game content here
            //controllerList = new List<Controller>();
            camera = new Camera(graphics.GraphicsDevice.Viewport);

            GameView gameView = new GameView();

            // TODO: load in controllers
            gameController = new GameController(Content, camera, graphics.GraphicsDevice, gameView);
            menuController = new MenuController(Content, camera, graphics.GraphicsDevice, gameView);

            //if something is true
            if (gameState == GameState.Game)
            {
                //int amountOfEnemies = 3;
                //gameController.InitiateEnemyWave(amountOfEnemies);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
               Exit();

            switch (gameState)
            {
                case GameState.MenuScreen:
                    if (menuController.CheckIfUserWantsToPlay())
                    {
                        gameState = GameState.Game;
                    }
                    break;
                case GameState.Game:
                    pauseKeyDownThisFrame = (Keyboard.GetState().IsKeyDown(Keys.P));
                    if (!pauseKeyDown && pauseKeyDownThisFrame)
                    {
                        gameState = GameState.Paused;
                    }
                    pauseKeyDown = pauseKeyDownThisFrame;
                    gameController.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                case GameState.Paused:
                    pauseKeyDownThisFrame = (Keyboard.GetState().IsKeyDown(Keys.P));
                    if (!pauseKeyDown && pauseKeyDownThisFrame)
                    {
                        gameState = GameState.Game;
                    }
                    pauseKeyDown = pauseKeyDownThisFrame;
                    break;
                default:
                    break;
            }
                      
            base.Update(gameTime);
        }

      

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            switch (gameState)
            {
                case GameState.MenuScreen:
                    menuController.Draw(spriteBatch);
                    break;
                case GameState.Game:
                    gameController.Draw(spriteBatch);
                    break;
                case GameState.Paused:
                    gameController.Draw(spriteBatch);
                    break;
                default:
                    break;
            }
          
            base.Draw(gameTime);
        }
    }
}
