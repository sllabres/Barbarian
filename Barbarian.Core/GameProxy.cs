using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Barbarian.Core
{
    public delegate void DrawDelegate(GameTime gameTime);
    public delegate void LoadContentDelegate();
    public delegate void InitializeDelegate();

    public interface IGameProxy : IDisposable
    {
        event DrawDelegate DrawEvent;
        event LoadContentDelegate LoadContentEvent;
        event InitializeDelegate InitializeEvent;
        GameWindow Window { get; }

        GraphicsDevice GraphicsDevice { get; }
        ContentManager Content { get; }
        void Run();
    }

    public class GameProxy : Game, IGameProxy
    {
        public event DrawDelegate DrawEvent;
        public event LoadContentDelegate LoadContentEvent;
        public event InitializeDelegate InitializeEvent;

        public GameProxy()
        {            
            IsMouseVisible = true;
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
            InitializeEvent();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LoadContentEvent();
        }        

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState(PlayerIndex.One).GetPressedKeys().Contains(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            DrawEvent(gameTime);
            base.Draw(gameTime);
        }
    }
}
