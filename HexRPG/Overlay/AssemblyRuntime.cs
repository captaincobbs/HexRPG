using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Overlay
{
    class AssemblyRuntime : IDebugOverlayItem
    {
        public Vector2 Coordinates { get; set; }

        public Vector2 Offset { get; set; } = new Vector2(0, 0);

        public HorizontalAlignment horizontalAlignment { get; set; } = HorizontalAlignment.Left;

        public VerticalAlignment verticalAlignment { get; set; } = VerticalAlignment.Top;

        public AssemblyRuntime()
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
            return $"C#.NET: {HardwareUtilities.GetAssemblyRuntimeVersion()}";
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

            switch (verticalAlignment)
            {
                case VerticalAlignment.Center:
                    y += VerticalAlignCenter(new Rectangle(0, 0, MainGame.GameWindow.ClientBounds.Width, MainGame.GameWindow.ClientBounds.Height), font.MeasureString(GetString()));
                    break;
                case VerticalAlignment.Bottom:
                    y += VerticalAlignBottom(new Rectangle(0, 0, MainGame.GameWindow.ClientBounds.Width, MainGame.GameWindow.ClientBounds.Height), font.MeasureString(GetString()));
                    break;
            }

            Coordinates = new Vector2(x, y);
        }
    }
}
