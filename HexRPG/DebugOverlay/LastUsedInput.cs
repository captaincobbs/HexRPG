using HexRPG.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static HexRPG.Entity.InputManager;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Overlay
{
    public class LastUsedInput : IDebugOverlayItem
    {
        public Vector2 Coordinates { get; set; }

        public Vector2 Offset { get; set; } = new Vector2(0, 0);

        public HorizontalAlignment horizontalAlignment { get; set; } = HorizontalAlignment.Left;

        public VerticalAlignment verticalAlignment { get; set; } = VerticalAlignment.Top;

        public LastUsedInput()
        {
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {
            spriteBatch.DrawString(font, GetString(), Coordinates, color);
        }

        private string GetString()
        {
            return $"INPUT: {InputTypeLabel[(int)InputManager.LastUsedInput]}";
        }

        public void RecalculatePosition(SpriteFont font)
        {
            int x = (int)Offset.X;
            int y = (int)Offset.Y;
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Center:
                    x += HorizontalAlignCenter(new Rectangle(0, 0, MainGame.GameWindow.ClientBounds.Width, MainGame.GameWindow.ClientBounds.Height), font.MeasureString(GetString()));
                    break;
                case HorizontalAlignment.Right:
                    x += HorizontalAlignRight(new Rectangle(0, 0, MainGame.GameWindow.ClientBounds.Width, MainGame.GameWindow.ClientBounds.Height), font.MeasureString(GetString()));
                    break;
            }

            Coordinates = new Vector2(x, y);
        }
    }
}
