using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienInvaders
{
    class Background
    {
        public Texture2D stars;
        public Rectangle rectangle;

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(stars, rectangle, Color.White);
        }
    }
}
