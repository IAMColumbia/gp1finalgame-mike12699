using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienInvaders
{
    public class Timer : DrawableGameComponent
    {
        SpriteBatch sb;
        SpriteFont Font;
        Vector2 timerLoc;
        public static float time = 0;
        public static string timerDisplay;

        public Timer(Game game) : base(game)
        {
            NewGame();
        }

        public static void NewGame()
        {
            timerDisplay = "Timer: ";
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            Font = this.Game.Content.Load<SpriteFont>("Arial");
            timerLoc = new Vector2(700, 10);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.DrawString(Font, timerDisplay + time.ToString("0.00"), timerLoc, Color.Cyan);
            sb.End();
            base.Draw(gameTime);
        }
    }
}
