using _1DV437_NeilArmstrong.View;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Controller
{
    class MenuController : MasterController
    {
        ContentManager content;
        Camera camera;
        GraphicsDevice graphics;
        GameView gameView;
        bool showMenu;

        public MenuController(ContentManager content, Camera camera, GraphicsDevice graphics, GameView gameView)
        {
            showMenu = true;
            this.content = content;
            this.camera = camera;
            this.graphics = graphics;
            this.gameView = gameView;
        }

        public override void Update(float totalSeconds)
        {
            
        }

        public bool CheckIfUserWantsToPlay()
        {
            if (gameView.UserClicksPlay() )//&& showMenu)
            {
                //showMenu = false;
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            gameView.DrawMenu(spriteBatch);
            spriteBatch.End();
        }
    }
}
