using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CatapultWars
{
    public class CatapulteContent
    {
        public Texture2D FireTexture { get; set; }
        public Texture2D PullbackTexture { get; set; }
        public Texture2D RockTexture { get; set; }
        public Texture2D ArrowTexture { get; set; }

        public static CatapulteContent LoadForRed(ContentManager content)
        {
            var aimingArrow = content.Load<Texture2D>("Arrow");
            var fireTexture = content.Load<Texture2D>("redCatapult_fire");
            var pullbackTexture = content.Load<Texture2D>("redCatapult_Pullback");
            var rockTexture = content.Load<Texture2D>("rock_ammo");
            return new CatapulteContent
            {
                ArrowTexture = aimingArrow,
                FireTexture = fireTexture,
                PullbackTexture = pullbackTexture,
                RockTexture = rockTexture
            };
        }

        public static CatapulteContent LoadForBlue(ContentManager content)
        {
            throw new System.NotImplementedException();
        }
    }
}