using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienInvaders
{
    public class GameStates : DrawableGameComponent
    {
        SpriteBatch sb;
        public Texture2D imageEnd;
        public Texture2D imageWin;
        public Vector2 endScreenLose_Loc;
        public Vector2 endScreenWin_Loc;
        public static int intGameState;
        public string maxTimesLabel;
        Vector2 textPos_1;
        Vector2 textPos_2;



        public GameStates(Game game) : base(game)
        {
            
        }

        public override void Initialize()
        {
            intGameState = 0;
            maxTimesLabel = "Your Time:";
            textPos_1 = new Vector2(385, 270);
            textPos_2 = new Vector2(385, 305);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            imageEnd = this.Game.Content.Load<Texture2D>("imageEnd");
            imageWin = this.Game.Content.Load<Texture2D>("imageWin");
            endScreenLose_Loc = Vector2.Zero;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            switch (intGameState)
            {
                case 1:
                    sb.Draw(imageEnd, endScreenLose_Loc, Color.White);
                    break;
                case 2:
                    sb.Draw(imageWin, endScreenWin_Loc, Color.White);
                    if (ScoreManager.maxTimeRun)
                    {
                        sb.DrawString(ScoreManager.Font, maxTimesLabel, textPos_1, Color.White);
                        sb.DrawString(ScoreManager.Font, ScoreManager.textMaxTime_2[0], textPos_2, Color.White);
                    }
                    break;
            }
            sb.End();
            base.Draw(gameTime);
        }
    }
}
