using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Barbarian.Core;
using Barbarian.Win.Animation;
using Barbarian.Win.Enums;
using StructureMap;
using Microsoft.Xna.Framework.Input;

namespace Barbarian.Win
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
        void Load(ContentManager contentManager);
    }

    public interface IUpdateable
    {
        void Update(GameTime gameTime);
    }    

    public class Barbarian : IDrawable, IUpdateable
    {
        private AnimatedTexture _walkingTexture;
        private Vector2 _currentPosition;
        private AnimationState _animationState;

        public Barbarian()
        {
            _animationState = AnimationState.Idle;
            _currentPosition = new Vector2 { X = 70, Y = 160 };

            _walkingTexture = new AnimatedTexture
                (new AssetValues
                {
                    ContentName = "Barbarian/Walking",
                    FrameCount = 4,
                    Depth = 0.5f,
                    Rotation = 0.0f,
                    Scale = 1.0f,
                    Origin = _currentPosition,
                    TimePerFrame = 6.0f
                });

            ObjectFactory.Inject<IDrawable>(this);
            ObjectFactory.Inject<IUpdateable>(this);            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _walkingTexture.DrawFrame(spriteBatch, _currentPosition);
        }

        public void Load(ContentManager contentManager)
        {
            _walkingTexture.Load(contentManager);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState(PlayerIndex.One).GetPressedKeys().Contains(Keys.Right))
            {
                _currentPosition.X+=0.9f;                
            }

            if (Keyboard.GetState(PlayerIndex.One).GetPressedKeys().Contains(Keys.Left))
            {
                _currentPosition.X-=0.9f;                
            }

            _walkingTexture.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
