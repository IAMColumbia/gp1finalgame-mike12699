using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienInvaders
{
    class Scrolling : Background
    {
        public Scrolling(Texture2D newStars, Rectangle newRectangle)
        {
            stars = newStars;
            rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.Y += 3;
        }
    }
}
