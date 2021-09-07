using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Overlay
{
    public interface IDebugOverlayItem
    {
        public Vector2 Coordinates { get; set; }

        public HorizontalAlignment horizontalAlignment { get; set; }

        public VerticalAlignment verticalAlignment { get; set; }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color) { }

        public void RecalculatePosition(SpriteFont font) { }
    }
}
