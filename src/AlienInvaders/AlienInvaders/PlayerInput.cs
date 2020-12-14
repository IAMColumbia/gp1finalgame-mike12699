using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;

namespace AlienInvaders
{
    public class PlayerInput : DrawableGameComponent
    {
        SpriteBatch sb;
        Texture2D spaceship, invader, projectile;
        Rectangle rectspaceship, rectprojectile;
        Rectangle[,] rectinvader;
        public bool[,] invaderalive;
        public int rows = 5;
        public int cols = 10;
        public int invaderspeed = 3;
        public string Direction = "RIGHT";
        public bool ProjectileVisible = false;


        public PlayerInput(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            invader = this.Game.Content.Load<Texture2D>("invader");
            rectinvader = new Rectangle[rows, cols];
            invaderalive = new bool[rows, cols];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    rectinvader[r, c].Width = invader.Width;
                    rectinvader[r, c].Height = invader.Height;
                    rectinvader[r, c].X = 60 * c;
                    rectinvader[r, c].Y = 60 * r;
                    invaderalive[r, c] = true;
                }
            }
            spaceship = this.Game.Content.Load<Texture2D>("spaceship");
            rectspaceship.Width = spaceship.Width;
            rectspaceship.Height = spaceship.Height;
            rectspaceship.X = 0;
            rectspaceship.Y = 400;
            projectile = this.Game.Content.Load<Texture2D>("projectile");
            rectprojectile.Width = projectile.Width;
            rectprojectile.Height = projectile.Height;
            rectprojectile.X = 0;
            rectprojectile.Y = 0;
            base.LoadContent();
        }

        public override void Update(GameTime gametime)
        {
            int rightside = GraphicsDevice.Viewport.Width;
            int leftside = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (Direction.Equals("RIGHT"))
                        rectinvader[r, c].X = rectinvader[r, c].X + invaderspeed;
                    if (Direction.Equals("LEFT"))
                        rectinvader[r, c].X = rectinvader[r, c].X - invaderspeed;
                }
            }
            string ChangeDirection = "N";
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (invaderalive[r, c].Equals(true))
                    {
                        if (rectinvader[r, c].X + rectinvader[r, c].Width > rightside)
                        {
                            Direction = "LEFT";
                            ChangeDirection = "Y";
                        }

                        if (rectinvader[r, c].X < leftside)
                        {
                            Direction = "RIGHT";
                            ChangeDirection = "Y";
                        }
                    }
                }
            }
            if (ChangeDirection.Equals("Y"))
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        rectinvader[r, c].Y = rectinvader[r, c].Y + 5;
                    }
                }
            }
            int count = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (invaderalive[r, c].Equals(true))
                    {
                        count = count + 1;
                    }
                }
            }
            if (count > (rows * cols / 2))
            {
                invaderspeed = invaderspeed;
            }
            if (count < (rows * cols / 3))
            {
                invaderspeed = 4;
            }
            KeyboardState kb = Keyboard.GetState();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (invaderalive[r, c].Equals(true))
                    {
                        if (rectinvader[r, c].Y + rectinvader[r, c].Height > rectspaceship.Y)
                        {
                            rows = 0;
                            cols = 0;
                            ScoreManager.Lives--;
                            GameStates.intGameState = 1;
                            
                        }
                    }
                }
            }
            if (kb.IsKeyDown(Keys.Left))
            {
                rectspaceship.X = rectspaceship.X - 3;
            }

            if (kb.IsKeyDown(Keys.Right))
            {
                rectspaceship.X = rectspaceship.X + 3;
            }
            if (kb.IsKeyDown(Keys.Space) && ProjectileVisible.Equals(false))
            {
                ProjectileVisible = true;
                rectprojectile.X = rectspaceship.X + (rectspaceship.Width / 2) - (rectprojectile.Width / 2);
                rectprojectile.Y = rectspaceship.Y - rectprojectile.Height + 2;
            }
            if (ProjectileVisible.Equals(true))
            {
                rectprojectile.Y = rectprojectile.Y - 5;
            }
            if (ProjectileVisible.Equals(true))
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (invaderalive[r, c].Equals(true))
                        {
                            if (rectprojectile.Intersects(rectinvader[r, c]))
                            {
                                ProjectileVisible = false;
                                invaderalive[r, c] = false;
                                ScoreManager.Score++;
                            }
                        }
                    }
                }
            }
            if (ScoreManager.Score == 50)
            {
                GameStates.intGameState = 2;
                if (GameStates.intGameState == 2 && kb.IsKeyDown(Keys.Enter))
                {
                    GameStates.intGameState = 0;
                    ScoreManager.Score = 0;
                    ScoreManager.Lives = 1;
                    Timer.time = 0;
                    ScoreManager.maxTimeRun = false;
                    rows = 5;
                    cols = 10;
                    invaderspeed = 3;
                    for (int r = 0; r < rows; r++)
                    {
                        for (int c = 0; c < cols; c++)
                        {
                            rectinvader[r, c].Width = invader.Width;
                            rectinvader[r, c].Height = invader.Height;
                            rectinvader[r, c].X = 60 * c;
                            rectinvader[r, c].Y = 60 * r;
                            invaderalive[r, c] = true;
                        }
                    }
                    rectspaceship.Width = spaceship.Width;
                    rectspaceship.Height = spaceship.Height;
                    rectspaceship.X = 0;
                    rectspaceship.Y = 400;
                    rectprojectile.Width = projectile.Width;
                    rectprojectile.Height = projectile.Height;
                    rectprojectile.X = 0;
                    rectprojectile.Y = 0;
                }
            }
            if (rectprojectile.Y + rectprojectile.Height < 0)
            {
                ProjectileVisible = false;
            }
            if (GameStates.intGameState == 1 && kb.IsKeyDown(Keys.Enter))
            {
                GameStates.intGameState = 0;
                ScoreManager.Score = 0;
                ScoreManager.Lives = 1;
                Timer.time = 0;
                ScoreManager.maxTimeRun = false;
                rows = 5;
                cols = 10;
                invaderspeed = 3;
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        rectinvader[r, c].Width = invader.Width;
                        rectinvader[r, c].Height = invader.Height;
                        rectinvader[r, c].X = 60 * c;
                        rectinvader[r, c].Y = 60 * r;
                        invaderalive[r, c] = true;
                    }
                }
                rectspaceship.Width = spaceship.Width;
                rectspaceship.Height = spaceship.Height;
                rectspaceship.X = 0;
                rectspaceship.Y = 400;
                rectprojectile.Width = projectile.Width;
                rectprojectile.Height = projectile.Height;
                rectprojectile.X = 0;
                rectprojectile.Y = 0;

            }

            
            base.Update(gametime);
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (invaderalive[r, c].Equals(true))
                    {
                        sb.Draw(invader, rectinvader[r, c], Color.White);
                    }
                }
            }
            sb.Draw(spaceship, rectspaceship, Color.White);
            if (ProjectileVisible.Equals(true))
            {
                sb.Draw(projectile, rectprojectile, Color.White);
            }
            sb.End();
            base.Draw(gameTime);
        }
    }
}
