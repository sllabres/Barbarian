using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Barbarian.Core;

namespace Barbarian.Win.Animation
{
    public class WalkingAnimation : IAnimation
    {
        public AnimationType Type { get { return AnimationType.Walking; } }
        Texture2D _frameSet;
        IGameProxy _game;
        private float _frameTime;
        private bool _isLooping;
        private int _frameHeight, _frameWidth;
        private int[] _frameSequence;

        public WalkingAnimation(IGameProxy gameProxy)
        {
            _game = gameProxy;
            _game.LoadContentEvent += LoadContentEvent;
            _frameSequence = new int[] { 0, 1, 2, 3 };
            _frameHeight = 63;
            _frameWidth = 48;
            _frameTime = 100;
            _isLooping = false;
        }

        void LoadContentEvent()
        {
            _frameSet = _game.Content.Load<Texture2D>("Barbarian/Walking");
        }
    }

    public interface IAnimation
    {        
        AnimationType Type { get; }
    }

    public enum AnimationType
    {
        Walking
    }
}
