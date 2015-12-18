using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV437_NeilArmstrong.View
{
    class Camera
    {
        int borderSize = 15;
        int scaleWidth;
        int scaleHeight;

     
        public Camera(Viewport graphics)
        {
            scaleWidth = graphics.Width - borderSize * 2;
            scaleHeight = graphics.Height - borderSize * 2;
        }

        public Rectangle GetGameWindow()
        {
            return new Rectangle(borderSize, borderSize, scaleWidth, scaleHeight);
        }

        public Vector2 scaleVisualPosition(Vector2 logicalCoordinates)
        {
            float visualX = logicalCoordinates.X * scaleWidth;
            float visualY = logicalCoordinates.Y * scaleHeight;

            return new Vector2(visualX, visualY);
        }

        public Vector2 scaleProjectilePosition(Vector2 logicalCoordinates)
        {
            float visualX = logicalCoordinates.X * scaleWidth + borderSize * 2;
            float visualY = logicalCoordinates.Y * scaleHeight + borderSize * 2;

            return new Vector2(visualX, visualY);
        }
    }
}
