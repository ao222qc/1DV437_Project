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
        Texture2D gameWindow;
        //Camera camera;
        Rectangle gameScreen;

        PlayerShip playerShip;

        //Abstract klass/interface som alla fiender/spelare ärver utav
        //Foreach -> update && Draw

        public GameController(ContentManager content, Camera camera, GraphicsDevice graphics)
        {
            
            gameWindow = new Texture2D(graphics, 1, 1);
            gameWindow.SetData<Color>(new Color[]
                {
                    Color.White
                }); 
            this.camera = camera;
            gameScreen = camera.GetGameWindow();
            playerShip = new PlayerShip();
        }

        public override void Update(float totalSeconds)
        {
          
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 vec = new Vector2(0.5f, 0.85f);

              spriteBatch.Begin();
              spriteBatch.Draw(gameWindow, gameScreen, Color.Black);

              spriteBatch.End();

        }

    }
}
