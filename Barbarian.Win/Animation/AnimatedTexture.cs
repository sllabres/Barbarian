using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Barbarian.Win.Animation
{
    public class AnimatedTexture
    {
        private int _framecount;
        public Texture2D _texture;
        private float _timePerFrame;
        private int _frame;
        private float _totalElapsed;
        private bool _isPaused;
        public float Rotation, Scale, Depth;
        public Vector2 Origin;

        public AnimatedTexture(Vector2 origin, float rotation,
            float scale, float depth)
        {
            this.Origin = origin;
            this.Rotation = rotation;
            this.Scale = scale;
            this.Depth = depth;
        }
        public void Load(ContentManager content, string asset,
            int frameCount, int framesPerSec)
        {
            _framecount = frameCount;
            _texture = content.Load<Texture2D>(asset);
            _timePerFrame = (float)1 / framesPerSec;
            _frame = 0;
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
                _frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                _frame = _frame % _framecount;
                _totalElapsed -= _timePerFrame;
            }
        }

        // class AnimatedTexture
        public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
        {
            DrawFrame(batch, _frame, screenPos);
        }
        public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
        {
            int FrameWidth = _texture.Width / _framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0,
                FrameWidth, _texture.Height);
            batch.Draw(_texture, screenPos, sourcerect, Color.White,
                Rotation, Origin, Scale, SpriteEffects.None, Depth);
        }

        public bool IsPaused
        {
            get { return _isPaused; }
        }
        
        public void Reset()
        {
            _frame = 0;
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
