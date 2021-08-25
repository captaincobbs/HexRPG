using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Debug
{
    public interface IDebugOverlayItem
    {
        public Vector2 Offset { get; set; }

        public Vector2 Coordinates { get; set; }

        VerticalAlignment verticalAlignment { get; set; }
        HorizontalAlignment horizontalAlignment { get; set; }

        public void Update(float deltaTime) { }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color) { }

        public string GetStringValue()
        {
            return "";
        }
    }
}
