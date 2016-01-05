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
        
        public ShowStats(PlayerShip playerShip, Camera camera, GameController gameController)
        {
            this.playerShip = playerShip;
            this.camera = camera;
            this.gameController = gameController;
        }

        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("MyFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "HP:" + playerShip.GetHitPoints(), new Vector2(14f, 12f), Color.Red);
        }

        public void ShowLevel(SpriteBatch spriteBatch, int wave)
        {
            spriteBatch.DrawString(spriteFont, "Wave: " + wave, new Vector2(320f, 300f), Color.Red);
        }
    }
}
