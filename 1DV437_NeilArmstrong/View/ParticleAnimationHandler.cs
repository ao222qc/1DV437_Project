using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    class ParticleAnimationHandler
    {
        //public ParticleAnimation[] sparkParticles;
        List<ParticleAnimation> particleAnimationList; 
        private const int MaxParticles = 20;
        List<ParticleAnimation> toRemove;


        public ParticleAnimationHandler(ContentManager content)
        {
            toRemove = new List<ParticleAnimation>();
            particleAnimationList = new List<ParticleAnimation>();
        }

        public void AddParticle(Vector2 startPosition)
        {
            Console.WriteLine(particleAnimationList.Count);
            for (int i = 0; i < MaxParticles; i++)
            {
                particleAnimationList.Add(new ParticleAnimation(i, startPosition));
            }
        }

        public void ClearParticles()
        {
            particleAnimationList.Clear();
        }

        public void CheckIfDone()
        {
            for (int i = 0; i < MaxParticles; i++)
            {
                if (particleAnimationList[i].CheckIfParticlesGone())
                {
                    //return true;
                }
            }
           // return false;
        }

        public void Update(float elapsedSeconds) //float elapsedtime
        {
            foreach (ParticleAnimation pa in particleAnimationList)
            {
                pa.Update(elapsedSeconds);//float elapsedtime

                if(pa.GetPosition().X > 1 || pa.GetPosition().X < 0
                    || pa.GetPosition().Y > 1 || pa.GetPosition().Y < 0)
                {
                    toRemove.Add(pa);
                }
            }

            particleAnimationList.RemoveAll(x => toRemove.Contains(x));
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam, Texture2D texture)
        {
            foreach (ParticleAnimation pa in particleAnimationList)
            {
                pa.Draw(spriteBatch, cam, texture);
            }
        }
    }
}
