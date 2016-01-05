using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    class MovingBackground
    {
        Texture2D background;
        Camera camera;
        Vector2 position1;
        Vector2 position2;
        float speed = 2.5f;

        public MovingBackground(ContentManager content, Camera camera)
        {
            background = content.Load<Texture2D>("outerspace");
            this.camera = camera;
            position1 = new Vector2(0, 0);
            position2 = new Vector2(0, -camera.GetScaledHeight());
        }
        public void Update()
        {
            position1.Y = position1.Y + this.speed;
            position2.Y = position2.Y + this.speed;

            if (position1.Y >= camera.GetScaledHeight())
            {
                position1.Y = 0;
                position2.Y = -camera.GetScaledHeight();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background,position1, Color.White);
            spriteBatch.Draw(background, position2, Color.White);
        }

    }
}
