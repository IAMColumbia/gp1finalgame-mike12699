using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using MonoGameLibrary.Util;

namespace AlienInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
     
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Level level;
        Scrolling scrolling1;
        Scrolling scrolling2;
        //Aliens aliens;
        ScoreManager score;
        //InputHandler input;
        PlayerInput pi;
        Timer timer;
        GameStates gs;
        //Vector2 endScreen_Loc;
        //Vector2 startScreen_Loc;
        //Texture2D imageEnd;
        //Texture2D imageStart;
        //int intGameState;
        //Texture2D spaceship, projectile;
        //Rectangle rectspaceship, rectprojectile;
        //Rectangle[,] rectinvader;
        //bool[,] invaderalive;
        //int invaderspeed = 3;
        //int rows = 5;
        //int cols = 10;
        //string Direction = "RIGHT";
        //bool ProjectileVisible = false;

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gs = new GameStates(this);
            this.Components.Add(gs);
            //input = new InputHandler(this);
            //this.Components.Add(input);
            score = new ScoreManager(this);
            this.Components.Add(score);
            //aliens = new Aliens(this);
            //this.Components.Add(aliens);
            pi = new PlayerInput(this);
            this.Components.Add(pi);
            timer = new Timer(this);
            this.Components.Add(timer);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //endScreen_Win = new GameStates();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scrolling1 = new Scrolling(Content.Load<Texture2D>("stars1"), new Rectangle(0,0,800,500));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("stars2"), new Rectangle(0, 0, 800, 500));
            /*imageStart = Content.Load<Texture2D>("imageStart");
            imageEnd = Content.Load<Texture2D>("imageEnd");
            endScreen_Win.spriteTexture = imageEnd;
            endScreen_Loc = Vector2.Zero;
            startScreen_Loc = Vector2.Zero;
            intGameState = 0;*/
            /*invader = Content.Load<Texture2D>("invader");
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
            spaceship = Content.Load<Texture2D>("spaceship");
            rectspaceship.Width = spaceship.Width;
            rectspaceship.Height = spaceship.Height;
            rectspaceship.X = 0;
            rectspaceship.Y = 400;
            projectile = Content.Load<Texture2D>("projectile");
            rectprojectile.Width = projectile.Width;
            rectprojectile.Height = projectile.Height;
            rectprojectile.X = 0;
            rectprojectile.Y = 0;*/
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (scrolling1.rectangle.Y - scrolling1.stars.Height >= 0)
            {
                scrolling1.rectangle.Y = scrolling2.rectangle.Y - scrolling2.stars.Height;
            }
            if (scrolling2.rectangle.Y - scrolling2.stars.Height >= 0)
            {
                scrolling2.rectangle.Y = scrolling1.rectangle.Y - scrolling1.stars.Height;
            }
            scrolling1.Update();
            scrolling2.Update();
            /*int rightside = graphics.GraphicsDevice.Viewport.Width;
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
            KeyboardState kb = Keyboard.GetState();
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
            if (rectprojectile.Y + rectprojectile.Height < 0)
            {
                ProjectileVisible = false;
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
                            this.Exit();
                        }
                    }
                }
            }*/

            
                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            /*switch (intGameState)
            {
                case 0:
                    spriteBatch.Draw(imageStart, startScreen_Loc, Color.White);
                    break;
                case 1:
                    spriteBatch.Draw(imageEnd, endScreen_Loc, Color.White);
                    break;
            }*/
            /*for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (invaderalive[r, c].Equals(true))
                    {
                        spriteBatch.Draw(invader, rectinvader[r, c], Color.White);
                    }
                }
            }
            spriteBatch.Draw(spaceship, rectspaceship, Color.White);
            if (ProjectileVisible.Equals(true))
            {
                spriteBatch.Draw(projectile, rectprojectile, Color.White);
            }*/
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
