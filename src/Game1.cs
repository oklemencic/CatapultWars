#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace CatapultWars
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D _cloud1;
        
        private TimeSpan _lastGameTime;
        private Catapulte _player1;
        private Catapulte _player2;
        
        private bool _isPlayer1Active;
        private Point _currentMousePosition;
        private CatapulteContent _redCatapultContent;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _lastGameTime = TimeSpan.Zero;

            _InitializePlayer1();
            

            _player2 = new Catapulte(new Vector2(0, 0));

            _isPlayer1Active = true;

            Helper.Initialize(
                graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight
            );

            base.Initialize();
        }

        private void _InitializePlayer1()
        {
            _player1 = new Catapulte(new Vector2(100, 300));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Helper.Font = Content.Load<SpriteFont>("HUDFont");
            
            _cloud1 = Content.Load<Texture2D>("cloud1");

            

            _redCatapultContent = CatapulteContent.LoadForRed(Content);
            _player1.Initialize(_redCatapultContent);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.R))
            {
                _InitializePlayer1();
                _player1.Initialize(_redCatapultContent);
            }

            _currentMousePosition = Mouse.GetState().Position;

            _player1.UpdateStateMachine(gameTime);

            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            
            // spriteBatch.Draw(_cloud1, new Vector2(100, 100));

            spriteBatch.DrawString(Helper.Font, ".", new Vector2(_currentMousePosition.X, _currentMousePosition.Y), Color.Black);

            _player1.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
