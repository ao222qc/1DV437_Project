using _1DV437_NeilArmstrong.View;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.Controller
{
   abstract class Controller
    {
       protected Camera camera;

       public abstract void Update(float totalSeconds);

       public abstract void Draw(SpriteBatch spriteBatch);
    }
}
