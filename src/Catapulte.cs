using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatapultWars
{
    public class Catapulte
    {
        private Animation _fireAnimation;
        private Animation _pullbackAnimation;
        private float _strength;
        private float _aimingArrowScale;
        private int _mouseDownPosition;
        private CatapulteState _currentState;
        private readonly Vector2 _position;
        private Projectile _projectile;

        private CatapulteContent _content;


        public Catapulte(Vector2 position)
        {
            _currentState = CatapulteState.Idle;
            _position = position;
        }

        public void Initialize(CatapulteContent content)
        {
            _projectile = new Projectile(_position);
            _projectile.LoadContent(content);

            _content = content;

            _fireAnimation = new Animation(content.FireTexture, new Point(75, 60), new Point(15, 2));
            _fireAnimation.Position = _position;
            
            _pullbackAnimation = new Animation(content.PullbackTexture, new Point(75, 60), new Point(18, 1));
            _pullbackAnimation.Position = _position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentState == CatapulteState.Idle)
            {
                _pullbackAnimation.FrameIndex = 0;
                _pullbackAnimation.Draw(spriteBatch);
            }

            if (_currentState == CatapulteState.Firing)
            {
                _fireAnimation.NextFrame();
                _fireAnimation.Draw(spriteBatch);
            }

            if (_currentState == CatapulteState.Aiming)
            {
                
                _pullbackAnimation.Draw(spriteBatch);

                spriteBatch.Draw(_content.ArrowTexture, new Vector2(20, 100), null, Color.Green, 0f, Vector2.Zero, new Vector2(_aimingArrowScale, 0.2f), SpriteEffects.None, 0);
            }

            if (_currentState == CatapulteState.Flying)
            {
                _pullbackAnimation.FrameIndex = 0;
                _pullbackAnimation.Draw(spriteBatch);
                _projectile.Draw(spriteBatch);
                
            }

            spriteBatch.DrawString(Helper.Font, _strength.ToString(), new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(Helper.Font, _currentState.ToString(), new Vector2(10, 10), Color.Black);
        }

        public void UpdateStateMachine(GameTime gameTime)
        {
            switch (_currentState)
            {
                case CatapulteState.Idle:
                    _HandleIdleState();
                    break;
                case CatapulteState.Aiming:
                    _HandleAimingState();
                    break;
                case CatapulteState.Firing:
                    _HandleFiringState();
                    break;
                case CatapulteState.Flying:
                    _HandleFlyingState(gameTime);
                    break;
                case CatapulteState.GroundHit:
                case CatapulteState.PlayerHit:
                case CatapulteState.FinishedPlayerRound:
                    break;
            }
        }

        private void _HandleFlyingState(GameTime gameTime)
        {
            _projectile.Update(gameTime);
        }

        private void _HandleFiringState()
        {
            if (_fireAnimation.FrameIndex == _fireAnimation.FrameCount - 1)
            {
                _currentState = CatapulteState.Flying;
                _projectile.Start(_strength);
            }
        }



        private void _HandleAimingState()
        {
            var mouseState = Mouse.GetState();
            var mouseUpPosition = Helper.FixMousePosition(mouseState);
            _strength = Math.Abs(mouseUpPosition - _mouseDownPosition) / (float)Helper.Width;

            _aimingArrowScale = _strength;
            _pullbackAnimation.FrameIndex = (int)(17 * _strength);

            if (mouseState.LeftButton == ButtonState.Released)
                _currentState = CatapulteState.Firing;
        }

        private void _HandleIdleState()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                _mouseDownPosition = Helper.FixMousePosition(mouseState);
                _currentState = CatapulteState.Aiming;
            }
        }
    }
}