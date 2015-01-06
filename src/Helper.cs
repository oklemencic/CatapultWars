using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatapultWars
{
    public static class Helper
    {
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static SpriteFont Font { get; set; }

        public static void Initialize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static int FixMousePosition(MouseState mouseState)
        {
            var position = mouseState.Position;
            if (position.X < 0)
                return 0;

            if (position.X > Width)
                return Width;

            return position.X;
        }
    }
}