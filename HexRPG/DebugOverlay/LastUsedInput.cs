using HexRPG.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static HexRPG.Entity.InputManager;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Debug
{
    public class LastUsedInput : IDebugOverlayItem
    {
        public Vector2 Offset { get; set; }

        public Vector2 Coordinates { get; set; }

        public VerticalAlignment verticalAlignment { get; set; } = VerticalAlignment.Manual;

        public HorizontalAlignment horizontalAlignment { get; set; } = HorizontalAlignment.Manual;

        public LastUsedInput(Vector2 Coordinates)
        {
            this.Offset = Coordinates;
        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {
            spriteBatch.DrawString(font, $"INPUT  {InputManager.InputTypeLabel[(int)InputManager.LastUsedInput]}", Coordinates, color);
        }

        public string GetStringValue()
        {
            return $"INPUT  {InputManager.InputTypeLabel[(int)InputManager.LastUsedInput]}";
        }
    }
}
