using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
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

        public ExplosionAnimation(Vector2 startPosition)
        {
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

        public void Draw()
        {
            x = frameIndex % frameX;
            y = frameIndex / frameX;
            Vector2 vec = new Vector2(position.X, position.Y);

            rect = new Rectangle(x * frameWidth, y * frameWidth, frameHeight, frameWidth);

            //spriteBatch.Draw(texture, camera.scaleParticle(position.X, position.Y), rect, Color.White,
            //0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
    }

