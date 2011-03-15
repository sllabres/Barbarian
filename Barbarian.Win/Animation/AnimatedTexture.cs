using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Barbarian.Win.Animation
{
    public struct AssetValues
    {
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public float Depth { get; set; }
        public string ContentName { get; set; }
        public int FrameCount { get; set; }
        public int FramesPerSecond { get; set; }
        public float TimePerFrame { get; set; }
    }

    public class AnimatedTexture
    {        
        public Texture2D _texture;        
        private int _currentFrame;
        private float _totalElapsed;
        private bool _isPaused;                
        private AssetValues _assetValues;
        private readonly float _timePerFrame;

        public AnimatedTexture(AssetValues assetValues)
        {
            _assetValues = assetValues;
            _timePerFrame = (float)1 / _assetValues.FrameCount;
        }

        public void Load(ContentManager content)
        {            
            _texture = content.Load<Texture2D>(_assetValues.ContentName);            
            _currentFrame = 0;
            _totalElapsed = 0;
            _isPaused = false;
        }

        // class AnimatedTexture
        public void UpdateFrame(float elapsed)
        {
            if (_isPaused)
                return;

            _totalElapsed += elapsed;

            if (_totalElapsed > _timePerFrame)
            {
                _currentFrame++;
                // Keep the Frame between 0 and the total frames, minus one.
                _currentFrame = _currentFrame % _assetValues.FrameCount;
                _totalElapsed -= _timePerFrame;
            }
        }

        // class AnimatedTexture
        public void DrawFrame(SpriteBatch batch, Vector2 position)
        {
            DrawFrame(batch, _currentFrame, position);
        }

        public void DrawFrame(SpriteBatch batch, int frame, Vector2 position)
        {
            int frameHeight = _texture.Height / _assetValues.FrameCount;

            Rectangle sourcerect = new Rectangle(0, frameHeight * frame, _texture.Width, frameHeight);
            batch.Draw(_texture, position, sourcerect, Color.White, _assetValues.Rotation, new Vector2 { X = 0, Y = 0 }, _assetValues.Scale, SpriteEffects.None, _assetValues.Depth);
        }

        public bool IsPaused
        {
            get { return _isPaused; }
        }
        
        public void Reset()
        {
            _currentFrame = 0;
            _totalElapsed = 0f;
        }
                
        public void Stop()
        {
            Pause();
            Reset();
        }
        
        public void Play()
        {
            _isPaused = false;
        }
        
        public void Pause()
        {
            _isPaused = true;
        }
    }
}
