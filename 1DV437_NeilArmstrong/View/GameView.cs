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
    class GameView : Observer
    {
        List<Unit> myList;
        Texture2D playerShip;
        Texture2D enemyShip;
        Texture2D gameScreen;
        Camera camera;

        public GameView()
        {
            myList = new List<Unit>();
        }

        public void Initiate(ContentManager content, Camera camera, GraphicsDevice graphics)
        {
            playerShip = content.Load<Texture2D>("spaceship");
            enemyShip = content.Load<Texture2D>("enemyspaceship");
            this.camera = camera;
            gameScreen = gameScreen = new Texture2D(graphics, 1, 1);
            gameScreen.SetData<Color>(new Color[]
                {
                    Color.White
                });
        }

        //Called upon unit being added
        //Updates list of Units to be drawn
        //New units can be added in runtime
        public override void UpdateList(List<Unit> unitList)
        {     
            myList = unitList;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameScreen, camera.GetGameWindow(), Color.Black);

            foreach(Unit unit in myList)
            {
                if (unit is EnemyShip)
                {
                    spriteBatch.Draw(enemyShip, camera.scaleVisualPosition(unit.GetPosition()),
                    null, Color.White, 0f, new Vector2(enemyShip.Bounds.Width/2, enemyShip.Bounds.Height/2),
                    0.5f, SpriteEffects.None, 0f);
                }
                else if (unit is PlayerShip)
                {
                    spriteBatch.Draw(playerShip, camera.scaleVisualPosition(unit.GetPosition()),
                    null, Color.White, (unit as PlayerShip).Rotation, new Vector2(playerShip.Bounds.Width / 2, playerShip.Bounds.Height / 2),
                    0.25f, SpriteEffects.None, 0f);
                }
            }           
        }
    }
}
