using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatapultWars
{
    public class Projectile
    {
        private readonly Vector2 _initialPosition;
        private Texture2D _texture;
        private Vector2 _position;
        private TimeSpan _startGameTime;

        /// <summary>
        /// Velocity, between 10 and 100.
        /// </summary>
        private double _velocity;

        private ProjectileState _state;

        private const int X_VELOCITY = 10;

        public Projectile(Vector2 initialPosition)
        {
            _initialPosition = initialPosition;
        }

        public void LoadContent(CatapulteContent content)
        {
            _texture = content.RockTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var drawingPosition = new Vector2(_initialPosition.X + _position.X, _initialPosition.Y - _position.Y);
            spriteBatch.Draw(_texture, drawingPosition);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start(double strength)
        {
            _state = ProjectileState.IdleOrCrashed;
            _velocity = _CalculateVelocity(strength);
            _position = new Vector2();
        }


        private double _CalculateVelocity(double strength)
        {
            // strength is between 0 and 1
            // velocity should be between 10 and 100
            var temp = Math.Max(0.1, strength);
            return temp * 100;
        }

        public ProjectileState Update(GameTime gameTime)
        {
            if (_state == ProjectileState.IdleOrCrashed)
                _startGameTime = gameTime.TotalGameTime;

            var time = (gameTime.TotalGameTime - _startGameTime).TotalSeconds * 10;
            
            var y = (_velocity*time) - (0.5*9.81*Math.Pow(time, 2));
            var x = _velocity * time;

            _position = new Vector2((float)x, (float)y);

            _state = (y <= 0 && time > 0) ? ProjectileState.IdleOrCrashed : ProjectileState.Flying;
            return _state;
        }
    }
}