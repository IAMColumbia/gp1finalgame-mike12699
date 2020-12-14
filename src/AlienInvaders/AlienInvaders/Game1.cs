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
        Scrolling scrolling1;
        Scrolling scrolling2;
        ScoreManager score;
        PlayerInput pi;
        Timer timer;
        GameStates gs;

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gs = new GameStates(this);
            this.Components.Add(gs);;
            score = new ScoreManager(this);
            this.Components.Add(score);
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
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
