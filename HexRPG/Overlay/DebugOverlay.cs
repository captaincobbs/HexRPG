using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Overlay
{
    public static class DebugOverlay
    {
        public static bool IsVisible { get; set; } = false;

        private static List<IDebugOverlayItem> DebugOverlayItems;

        public static void Initialize()
        {
            DebugOverlayItems = new List<IDebugOverlayItem>()
            {
                // Top Left
                new FPS() { Offset = new Vector2(4, 0)},
                new LastUsedInput() {Offset = new Vector2(4, 12)},

                // Top Right
                new Coordinate() {Offset = new Vector2(-4, 0), horizontalAlignment = HorizontalAlignment.Right},
                new CameraCoordinates() {Offset = new Vector2(-4, 12), horizontalAlignment = HorizontalAlignment.Right},
                new Zoom() {Offset = new Vector2(-4, 24), horizontalAlignment = HorizontalAlignment.Right },

                // Bottom Left
                new ProcessorArchitecture() {Offset = new Vector2(4, -14), verticalAlignment = VerticalAlignment.Bottom},
                new GraphicsDevice() {Offset = new Vector2(4, -2), verticalAlignment = VerticalAlignment.Bottom},
                new AssemblyRuntime() {Offset = new Vector2(4, -26), verticalAlignment = VerticalAlignment.Bottom},

                // Bottom Right
            };
            RecalculatePositions();
        }

        public static void RecalculatePositions()
        {
            foreach (IDebugOverlayItem item in DebugOverlayItems)
            {
                item.RecalculatePosition(MainGame.FontSet[GameOptions.ForegroundFont]);
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (IsVisible)
            {
                foreach (IDebugOverlayItem item in DebugOverlayItems)
                {
                    item.Update(gameTime);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                foreach (IDebugOverlayItem item in DebugOverlayItems)
                {
                    item.Draw(spriteBatch, MainGame.FontSet[GameOptions.ForegroundFont], GameOptions.ForegroundColor);
                }
                RecalculatePositions();
            }

        }
    }
}
