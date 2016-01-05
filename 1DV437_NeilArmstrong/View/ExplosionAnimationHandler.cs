using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    class ExplosionAnimationHandler
    {
        List<ExplosionAnimation> explosionList = new List<ExplosionAnimation>();

        public ExplosionAnimationHandler()
        {

        }

        public void AddExplosion(Vector2 startPosition, Texture2D explosion, ExplosionType explosionType)
        {
            explosionList.Add(new ExplosionAnimation(startPosition, explosion, explosionType));
        }

        public void Update(float totalSeconds)
        {

            for (int i = 0; i < explosionList.Count; i++)
            {
                explosionList[i].Update(totalSeconds);
            }
            ClearList();

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            for (int i = 0; i < explosionList.Count; i++)
            {
                explosionList[i].Draw(spriteBatch, camera);
            }
            ClearList();
        }

        public void ClearList()
        {
            if (explosionList.Count > 10)
            {
                explosionList.Clear();
            }
        }

    }
}

