using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    class ParticleAnimation
    {
            Vector2 randomDirection;
            private float radius = 0.06f;
            private int seed;
            Vector2 startPosition;
            Vector2 position;
            Vector2 velocity;
            Vector2 acceleration;
            Random rand;

            public ParticleAnimation(int seed, Vector2 systemStartPosition)
            {
                rand = new Random(seed);
                randomDirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
                randomDirection.Normalize();
                randomDirection = randomDirection * ((float)rand.NextDouble() * 0.5f);
                this.seed = seed;
                startPosition = systemStartPosition;
                position = new Vector2(startPosition.X, StartPosition.Y);
                velocity = randomDirection;
                acceleration = new Vector2(0f, 0.7f);
            }

            public bool CheckIfParticlesGone()
            {
                return this.position.Y > 1;
            }
            public Vector2 StartPosition
            {
                get { return startPosition; }
            }
            public Vector2 Velocity
            {
                get { return velocity; }
            }

            public Vector2 GetPosition()
            {
                return position;
            }

            public void Update(float elapsedSeconds)
            {
                position = position + velocity * elapsedSeconds;
                velocity = velocity + acceleration * elapsedSeconds;
                if (position.X >= 1 || position.X <= 0)
                {
                    velocity.X = -velocity.X;
                }
                if (position.Y <= 0)
                {
                    velocity.Y = -velocity.Y;
                }
            }

            public void Draw(SpriteBatch spriteBatch, Camera cam, Texture2D texture)
            {
                Vector2 vec = cam.scaleVisualPosition(new Vector2(position.X, position.Y));
                spriteBatch.Draw(texture, vec,
                null, Color.White, 0f, Vector2.Zero, this.radius, SpriteEffects.None, 0f);
            }
    }
}
