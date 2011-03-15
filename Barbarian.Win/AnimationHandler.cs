using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Barbarian.Core;
using Barbarian.Win.Animation;
using StructureMap;

namespace Barbarian.Win
{
    public class AnimationHandler
    {
        private IGameProxy _game;
        private SpriteBatch _spriteBatch;

        private AnimatedTexture _barbarianTexture;        
        private Vector2 _shipPos;

        public AnimationHandler(IGameProxy gameProxy, IGraphicsDeviceManager graphicsDeviceManager)
        {
            _game = gameProxy;                        
            _game.DrawEvent += Draw;
            _game.UpdateEvent += Update;
            _game.LoadContentEvent += LoadContent;            

            _barbarianTexture = new AnimatedTexture
                (new AssetValues
                {
                    ContentName = "Barbarian/Walking",
                    FrameCount = 4,
                    Depth = 0.5f,
                    Rotation = 0.0f,
                    Scale = 1.0f,
                    Origin = _shipPos,
                    TimePerFrame = 6.0f
                });
        }

        private void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            _barbarianTexture.Load(_game.Content);
            _shipPos = new Vector2(_game.GraphicsDevice.Viewport.Width / 2, _game.GraphicsDevice.Viewport.Height / 2);
        }

        private void Update(GameTime gameTime)
        {
            _barbarianTexture.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _barbarianTexture.DrawFrame(_spriteBatch, _shipPos);            
            _spriteBatch.End();
        }
    }
}
