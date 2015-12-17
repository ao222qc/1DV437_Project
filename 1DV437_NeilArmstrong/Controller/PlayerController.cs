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
        float playerMovementInput;
        float rotation;

        public PlayerController()
        {           
            //this.camera = camera;            
        }

        public void Update(float totalSeconds, PlayerShip playerShip)
        {
            
            // Handles movement input from the keyboard
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
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                playerShip.Shoot();
            }

             playerShip.Move(totalSeconds, playerMovementInput);
             rotation = playerMovementInput/3;
             playerShip.Rotation = rotation;
        }


        public override void Update(float totalSeconds)
        {
            
        }
    }
}
