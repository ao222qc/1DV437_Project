using _1DV437_NeilArmstrong.Controller;
using _1DV437_NeilArmstrong.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    class ShowStats
    {
        PlayerShip playerShip;
        Camera camera;
        GameController gameController;
        SpriteFont spriteFont;
        CollisionHandler collisionHandler;
        
        public ShowStats(PlayerShip playerShip, Camera camera, GameController gameController, CollisionHandler collisionHandler)
        {
            this.collisionHandler = collisionHandler;
            this.playerShip = playerShip;
            this.camera = camera;
            this.gameController = gameController;
        }

        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("MyFont");
        }

        public void ShowGameOver(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "GAME OVER", new Vector2(270f, 250f), Color.Red);
            spriteBatch.DrawString(spriteFont, "Moon landing was a hoax", new Vector2(190f, 300f), Color.Red);
            spriteBatch.DrawString(spriteFont, "Press ENTER to go to menu", new Vector2(180f, 400f), Color.Red);
        }

        public void ShowGameFinished(SpriteBatch spriteBatch)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CONGRATULATIONS");
            sb.AppendLine();
            sb.Append("YOU HAVE DEFEATED THE EVIL FORCES");
            sb.AppendLine();
            sb.Append("THE WAY TO THE MOON IS NOW CLEAR");
            sb.AppendLine();
            sb.Append("Press ENTER to go to menu");

            spriteBatch.DrawString(spriteFont, sb.ToString(), new Vector2(130f, 250f), Color.Red);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "HP:" + playerShip.GetHitPoints(), new Vector2(14f, 12f), Color.Red);
        }

        public void DrawPausedScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "GAME PAUSED", new Vector2(270f, 250f), Color.Red);
            spriteBatch.DrawString(spriteFont, "PRESS P TO UNPAUSE", new Vector2(220f, 300f), Color.Red);
        }

        public void ShowLevel(SpriteBatch spriteBatch, int wave)
        {
            spriteBatch.DrawString(spriteFont, "Wave: " + wave, new Vector2(320f, 300f), Color.Red);
        }
    }
}
