using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatapultWars
{
    public class Animation
    {
        private readonly Texture2D _texture;
        private readonly Point _sheetSize;

        private int _currentFrameIndex = 0;
        private Point _frameSize;
        private Point _currentFrame;

        public Vector2 Position { get; set; }

        public Animation(Texture2D texture, Point frameSize, Point sheetSize)
        {
            _frameSize = frameSize;
            _texture = texture;
            _sheetSize = sheetSize;

            FrameCount = sheetSize.X*sheetSize.Y;
        }

        public int FrameCount { get; private set; }

        public int FrameIndex
        {
            get { return _currentFrameIndex; }
            set
            {
                _currentFrameIndex = value;
                _FixFrameIndex();
            }
        }

        public void NextFrame()
        {
            _currentFrameIndex++;
            _FixFrameIndex();
        }

        private void _FixFrameIndex()
        {
            if (_currentFrameIndex >= FrameCount)
                _currentFrameIndex = 0;

            var xIndex = _currentFrameIndex%_sheetSize.X;
            var yIndex = _currentFrameIndex/_sheetSize.X;
            _currentFrame = new Point(xIndex, yIndex);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = new Rectangle(
                _frameSize.X * _currentFrame.X,
                _frameSize.Y * _currentFrame.Y, 
                _frameSize.X, 
                _frameSize.Y);

            spriteBatch.Draw(_texture, Position, sourceRectangle,
                Color.White, 0f, Vector2.Zero, new Vector2(1), SpriteEffects.None, 0);
        }
    }
}