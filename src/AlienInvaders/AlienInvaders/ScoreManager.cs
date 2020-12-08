using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienInvaders
{
    class ScoreManager : DrawableGameComponent
    {
        public static int Score;
        public static int Lives;
        public static int Level;
        public static string Help1;
        public static string Help2;
        SpriteFont Font;
        Vector2 scoreLoc, livesLoc, levelLoc, helpLoc1, helpLoc2;
        SpriteBatch sb;
        public ScoreManager(Game game) : base(game)
        {
            NewGame();
        }
        public static void NewGame()
        {
            Lives = 1;
            Score = 0;
            Level = 1;
            Help1 = "Press Left or Right to move";
            Help2 = "Press SPACE to shoot";
        }
        protected override void LoadContent()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            Font = this.Game.Content.Load<SpriteFont>("Arial");
            livesLoc = new Vector2(10, 10);
            levelLoc = new Vector2(370, 10);
            scoreLoc = new Vector2(700, 10);
            helpLoc1 = new Vector2(10, 30);
            helpLoc2 = new Vector2(10, 50);
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.DrawString(Font, "Lives: " + Lives, livesLoc, Color.White);
            sb.DrawString(Font, "Score: " + Score, scoreLoc, Color.White);
            sb.DrawString(Font, "Level: " + Level, levelLoc, Color.White);
            sb.DrawString(Font, Help1, helpLoc1, Color.Yellow);
            sb.DrawString(Font, Help2, helpLoc2, Color.Yellow);
            sb.End();
            base.Draw(gameTime);
        }
    }
}
