using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Debug
{
    public class Coordinate : IDebugOverlayItem
    {
        public Vector2 Offset { get; set; }

        private Vector2 PlayerCoordinates { get; set; }

        public Vector2 Coordinates { get; set; }

        public VerticalAlignment verticalAlignment { get; set; } = VerticalAlignment.Manual;

        public HorizontalAlignment horizontalAlignment { get; set; } = HorizontalAlignment.Manual;

        public Coordinate(Vector2 Coordinates)
        {
            this.Offset = Coordinates;
            PlayerCoordinates = MainGame.Player.Coordinate;
        }

        public void Update(float deltaTime)
        {
            PlayerCoordinates = MainGame.Player.Coordinate;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {
            spriteBatch.DrawString(font, $"COORD {PlayerCoordinates.X}, {PlayerCoordinates.Y}", Coordinates, color);
        }

        public string GetStringValue()
        {
            return $"COORD {PlayerCoordinates.X}, {PlayerCoordinates.Y}";
        }
    }
}
