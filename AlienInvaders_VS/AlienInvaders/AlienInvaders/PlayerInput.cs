using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;

namespace AlienInvaders
{
    public class PlayerInput : DrawableGameComponent
    {
        InputHandler input;
        SpriteBatch sb;
        Texture2D spaceship, projectile;
        Rectangle rectspaceship, rectprojectile;
        Rectangle[,] rectinvader;
        public static bool[,] invaderalive;
        public static int rows = 5;
        public static int cols = 10;
        public static int invaderspeed = 3;
        public static string Direction = "RIGHT";
        public static bool ProjectileVisible = false;
        public Vector2 Movement { get; private set; }

        public PlayerInput(Game game) : base(game)
        {
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            this.Movement = Vector2.Zero;
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(this.Game.GraphicsDevice);
            //invader = this.Game.Content.Load<Texture2D>("invader");
            rectinvader = new Rectangle[rows, cols];
            invaderalive = new bool[rows, cols];
            /*for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    rectinvader[r, c].Width = invader.Width;
                    rectinvader[r, c].Height = invader.Height;
                    rectinvader[r, c].X = 60 * c;
                    rectinvader[r, c].Y = 60 * r;
                    invaderalive[r, c] = "YES";
                }
            }*/
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
            this.Movement = Vector2.Zero;
            /*int rightside = 800;
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
                    if (invaderalive[r, c].Equals("YES"))
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
            }*/
            if (input.KeyboardState.IsKeyDown(Keys.Left))
            {
                rectspaceship.X = rectspaceship.X - 3;
            }

            if (input.KeyboardState.IsKeyDown(Keys.Right))
            {
                rectspaceship.X = rectspaceship.X + 3;
            }
            if (input.KeyboardState.IsKeyDown(Keys.Space) && ProjectileVisible.Equals(false))
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
                Game.Exit();
            }
            if (rectprojectile.Y + rectprojectile.Height < 0)
            {
                ProjectileVisible = false;
            }
            /*int count = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (invaderalive[r, c].Equals("YES"))
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
                invaderspeed = 6;
            }
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (invaderalive[r, c].Equals(true))
                    {
                        if (rectinvader[r, c].Y + rectinvader[r, c].Height > rectspaceship.Y)
                        {
                            ScoreManager.Lives--;
                        }
                    }
                }
            }*/
            base.Update(gametime);
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            /*for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (invaderalive[r, c].Equals("YES"))
                    {
                        sb.Draw(invader, rectinvader[r, c], Color.White);
                    }
                }
            }*/
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
