using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System.IO;

namespace HexRPG.Fonts
{
    public class FontLibrary
    {
        public static SpriteFont Battlenet(GraphicsDevice graphicsDevice)
        {
            TtfFontBakerResult battlenet = (TtfFontBaker.Bake(File.ReadAllBytes("./Content/Fonts/Battlenet.ttf"),
                14,
                1024,
                1024,
                new[]
                {
                    CharacterRange.BasicLatin
                }
            ));

            return battlenet.CreateSpriteFont(graphicsDevice);
                
        }
    }

}
