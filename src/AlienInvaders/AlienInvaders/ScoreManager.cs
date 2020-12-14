using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienInvaders
{
    class ScoreManager : DrawableGameComponent
    {
        public static FileStream FileRead;
        public static FileStream FileWrite;
        public static StreamWriter ScoreWrite;
        public static StreamReader ScoreRead;
        public static SpriteFont Font;
        public static int Score;
        public static int Lives;
        public static string Help1;
        public static string Help2;
        public static string Help3;
        public static string Help4;
        public static bool maxTimeRun = false;
        public static string[] textMaxTime_1;
        public static string[] textMaxTime_2;
        public static int maxScores = 1;
        Vector2 livesLoc, helpLoc1, helpLoc2, helpLoc3, helpLoc4;
        SpriteBatch sb;
        public ScoreManager(Game game) : base(game)
        {
            NewGame();
        }
        public static void NewGame()
        {
            Lives = 1;
            Score = 0;
            Help1 = "Press Left or Right to move";
            Help2 = "Press SPACE to shoot";
            Help3 = "Destroy the aliens as fast as you can";
            Help4 = "Press Escape to quit";
        }

        public override void Initialize()
        {
            textMaxTime_1 = new string[maxScores];
            textMaxTime_2 = new string[maxScores];
            GameStates.intGameState = 0;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            Font = this.Game.Content.Load<SpriteFont>("Arial");
            livesLoc = new Vector2(10, 10);
            helpLoc1 = new Vector2(10, 30);
            helpLoc2 = new Vector2(10, 50);
            helpLoc3 = new Vector2(280, 10);
            helpLoc4 = new Vector2(330, 30);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            switch (GameStates.intGameState)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    if (!maxTimeRun)
                    {
                        GetMaxTimes();
                    }
                    break;
            }
            base.Update(gameTime);
        }
        public static void GetMaxTimes()
        {
            bool workingFileIO = true;
            try
            {
                FileRead = new FileStream("aliemMaxTimes.txt", FileMode.OpenOrCreate, FileAccess.Read);
                ScoreRead = new StreamReader(FileRead);
                for (int i = 0; i < maxScores; i++)
                {
                    textMaxTime_1[i] = ScoreRead.ReadLine();
                    if (textMaxTime_1[i] == null)
                    {
                        textMaxTime_1[i] = "0";
                    }
                }
                ScoreRead.Close();
                FileRead.Close();
            }
            catch
            {
                workingFileIO = false;
            }
            if (workingFileIO)
            {
                int j = 0;
                for (int i = 0; i < maxScores; i++)
                {
                    if (Timer.time > Convert.ToInt32(textMaxTime_1[i]) && i == j)
                    {
                        textMaxTime_2[i] = Timer.time.ToString();
                        i++;
                        if (i < maxScores)
                        {
                            textMaxTime_2[i] = textMaxTime_1[j];
                        }
                    }
                    else
                    {
                        textMaxTime_2[i] = textMaxTime_1[j];
                    }
                    j++;
                }
            }
            try
            {
                FileWrite = new FileStream("alienMaxTimes.txt", FileMode.Create, FileAccess.Write);
                ScoreWrite = new StreamWriter(FileWrite);
                for (int i = 0; i < maxScores; i++)
                {
                    ScoreWrite.WriteLine(textMaxTime_2[i]);
                }
                ScoreWrite.Close();
                FileWrite.Close();
            }
            catch
            {
                workingFileIO = false;
            }
            maxTimeRun = true;
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.DrawString(Font, "Lives: " + Lives, livesLoc, Color.White);
            sb.DrawString(Font, Help1, helpLoc1, Color.Yellow);
            sb.DrawString(Font, Help2, helpLoc2, Color.Yellow);
            sb.DrawString(Font, Help3, helpLoc3, Color.Red);
            sb.DrawString(Font, Help4, helpLoc4, Color.White);
            sb.End();
            base.Draw(gameTime);
        }
    }
}
