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
        ContentManager content;
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
            //spriteBatch.Draw(background, position1,
            //   camera.GetGameWindow(), Color.White, 0f, Vector2.Zero,
            //   1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(background, position2,
            //   camera.GetGameWindow(), Color.White, 0f, Vector2.Zero,
            //   1f, SpriteEffects.None, 0f);
            //Vector2 vec = new Vector2(background.Bounds.Width / 2, background.Bounds.Height / 2);
              //spriteBatch.Draw(background, position1, null, camera.GetGameWindow(), Vector2.Zero, 0f, new Vector2(1.2f, 1f), Color.White, SpriteEffects.None, 0f);
             // spriteBatch.Draw(background, position2, null, camera.GetGameWindow(), Vector2.Zero, 0f, new Vector2(1.2f, 1f), Color.White, SpriteEffects.None, 0f);

            spriteBatch.Draw(background,position1, Color.White);
            spriteBatch.Draw(background, position2, Color.White);
        }

    }
}
