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
    class PlayerController : Controller
    {
        Texture2D playerShipTexture;
        PlayerShip playerShip;
        float playerMovementInput;


        public PlayerController(ContentManager content, Camera camera, GraphicsDevice graphics)
        {
            playerShipTexture = content.Load<Texture2D>("spaceship");
            playerShip = new PlayerShip();
            this.camera = camera;            
        }

        public override void Update(float totalSeconds)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (playerMovementInput > playerShip.GetShipSpeed() * -1)
                {
                    playerMovementInput -= 0.02f;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (playerMovementInput < playerShip.GetShipSpeed())
                {
                    playerMovementInput += 0.02f;
                }
            }
            else
            {
                if (playerMovementInput > 0.00f)
                {
                    if (playerMovementInput < 0.01f)
                    {
                        playerMovementInput = 0f;
                    }
                    else
                    {
                        playerMovementInput -= 0.01f;
                    }
                }
                else if (playerMovementInput < 0.00f)
                {
                    playerMovementInput += 0.01f;
                }
            }
            playerShip.Move(totalSeconds, playerMovementInput);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playerShipTexture, camera.scaleVisualPosition(playerShip.GetShipPosition()),
            null, Color.White, 0f, new Vector2(playerShipTexture.Bounds.Width / 2,
            playerShipTexture.Bounds.Height / 2), 0.25f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
