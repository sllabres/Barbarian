using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Barbarian.Core;
using Barbarian.Win.Animation;

namespace Barbarian.Win
{
    public class AnimationHandler
    {
        private IGameProxy _game;
        private SpriteBatch _spriteBatch;

        private AnimatedTexture _spriteTexture;
        private const float Rotation = 0;
        private const float Scale = 2.0f;
        private const float Depth = 50.5f;
        private Vector2 _shipPos;

        public AnimationHandler(IGameProxy gameProxy, IGraphicsDeviceManager graphicsDeviceManager)
        {
            _game = gameProxy;                        
            _game.DrawEvent += Draw;
            _game.UpdateEvent += Update;
            _game.LoadContentEvent += LoadContent;

            _spriteTexture = new AnimatedTexture(Vector2.Zero, 0, 1.0f, 0.5f);
        }

        private void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            _spriteTexture.Load(_game.Content, "Barbarian/Walking", 4, 8);
            _shipPos = new Vector2(_game.GraphicsDevice.Viewport.Width / 2, _game.GraphicsDevice.Viewport.Height / 2);
        }

        private void Update(GameTime gameTime)
        {
            _spriteTexture.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteTexture.DrawFrame(_spriteBatch, _shipPos);            
            _spriteBatch.End();
        }
    }
}
