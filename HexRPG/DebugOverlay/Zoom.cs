using HexRPG.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Debug
{
    public class Zoom : IDebugOverlayItem
    {
        public Vector2 Offset { get; set; }

        public Vector2 Coordinates { get; set; }

        public VerticalAlignment verticalAlignment { get; set; } = VerticalAlignment.Manual;

        public HorizontalAlignment horizontalAlignment { get; set; } = HorizontalAlignment.Manual;

        public Zoom(Vector2 Coordinates)
        {
            this.Offset = Coordinates;
        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {
            spriteBatch.DrawString(font, $"ZOOM   {Math.Round(ViewPort.CamZoom, 2):0.00} / {ViewPort.CamZoomDest:0.00}", Coordinates, color);
        }

        public string GetStringValue()
        {
            return $"ZOOM   {Math.Round(ViewPort.CamZoom, 2):0.00} / {ViewPort.CamZoomDest:0.00}";
        }
    }
}
