using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Barbarian.Core;
using Barbarian.Win.Animation;
using StructureMap;
using System.Collections.Generic;

namespace Barbarian.Win
{
    public class GraphicsHandler
    {
        //private AnimationHandler _animationHandler;
        private Texture2D _background;
        private IGameProxy _game;
        private SpriteBatch _spriteBatch;
        private GraphicsDevice GraphicsDevice { get { return _game.GraphicsDevice; } }
        private IEnumerable<IDrawable> _drawables;

        public GraphicsHandler(IGameProxy gameProxy, IGraphicsDeviceManager graphicsDeviceManager)
        {            
            _game = gameProxy;
            _game.LoadContentEvent += LoadContent;
            _game.DrawEvent += Draw;            
            ((GraphicsDeviceManager)graphicsDeviceManager).PreparingDeviceSettings += PreparingDeviceSettings;            
        }

        private void PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 384;
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 282;
            e.GraphicsDeviceInformation.PresentationParameters.IsFullScreen = false;
        }

        private void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = _game.Content.Load<Texture2D>("Backgrounds/Background1");

            _drawables = ObjectFactory.GetAllInstances<IDrawable>();

            foreach (var drawable in _drawables)
            {
                drawable.Load(_game.Content);
            }
        }

        private void Draw(GameTime gameTime)
        {
            IEnumerable<AnimatedTexture> animatedTextures = ObjectFactory.GetAllInstances<AnimatedTexture>();
            
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, new Vector2 { X = 0, Y = 0 }, Color.White);

            foreach (var drawable in _drawables)
            {
                drawable.Draw(_spriteBatch);                
            }

            _spriteBatch.End();            
        }
    }
}
