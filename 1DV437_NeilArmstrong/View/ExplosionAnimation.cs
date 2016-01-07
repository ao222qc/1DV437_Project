using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{

    public enum ExplosionType
    {
        Hit,
        Death
    }
    class ExplosionAnimation
    {
        const int frames = 40;
        float frameTime = 0.010f;
        int frameHeight = 128;
        int frameWidth = 128;
        int frameX = 4;
        int x;
        int y;
        Vector2 position;
        Vector2 origin;
        Rectangle rect;
        int frameIndex;
        float time;
        int frame;
        Texture2D explosion;
        ExplosionType explosionType;

        public ExplosionAnimation(Vector2 startPosition, Texture2D explosion, ExplosionType explosionType)
        {
            this.explosionType = explosionType;
            this.explosion = explosion;
            origin = new Vector2(frameWidth / 2.0f, frameHeight / 2);
            this.position = startPosition;
        }

        public void Update(float totalSeconds)
        {
            time += totalSeconds;

            float percentAnimated = time / frameTime;
            frame = (int)(percentAnimated * frames);

            while (time > frameTime)
            {
                frameIndex++;
                time = 0f;
            }
        }

        public Rectangle GetCurrentFrame()
        {
            x = frameIndex % frameX;
            y = frameIndex / frameX;

            return new Rectangle(x * frameWidth, y * frameWidth, frameHeight, frameWidth);
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            x = frameIndex % frameX;
            y = frameIndex / frameX;
            Vector2 vec = new Vector2(position.X, position.Y);
            rect = new Rectangle(x * frameWidth, y * frameWidth, frameHeight, frameWidth);

            if (this.explosionType == ExplosionType.Hit)
            {
                spriteBatch.Draw(explosion, camera.scaleVisualPosition(vec), rect, Color.White,
                0.0f, origin, 0.25f, SpriteEffects.None, 1f);
            }
            else if (this.explosionType == ExplosionType.Death)
            {
                spriteBatch.Draw(explosion, camera.scaleVisualPosition(vec), rect, Color.White,
                0.0f, origin, 0.6f, SpriteEffects.None, 1f);
            }
        }
    }
}

