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
    class PlayerController : MasterController
    {
        float playerMovementInput;
        float rotation;
        UnitHandler unitHandler;
        List<Projectile> projectileList;

        public PlayerController(UnitHandler unitHandler)
        {
            this.unitHandler = unitHandler;
            projectileList = new List<Projectile>();
        }

        /**
         * Param 1: Total gametime passed in seconds
         * Param 2: PlayerShip object
         * Handles: Movement and shooting by keeping
         *          track of projectiles in a list
         *          Updates each Projectile created
         *          by the PlayerShip             
         **/

        public void Update(float totalSeconds, PlayerShip playerShip)
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

            rotation = playerMovementInput / 3;
            playerShip.Rotation = rotation;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                projectileList.Add(new Projectile(playerShip.GetPosition(), playerShip.Rotation, this));
                playerShip.Shoot(unitHandler, projectileList[projectileList.Count-1], totalSeconds);
            }

            foreach (Projectile p in projectileList)
            {
                p.MoveProjectile(totalSeconds, 0f);
            }
             
             playerShip.Move(totalSeconds, playerMovementInput);
            
        }


        public override void Update(float totalSeconds)
        {
            
        }
    }
}
