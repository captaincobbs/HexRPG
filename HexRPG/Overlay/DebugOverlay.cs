using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Overlay
{
    public static class DebugOverlay
    {
        /// <summary>
        /// Whether the debug overlay is allowed to be rendered or not, default false
        /// </summary>
        public static bool IsDiagnosticsVisible { get; set; } = false;

        /// <summary>
        /// Whether the grid overlay is visible
        /// </summary>
        public static bool IsGridVisible { get; set; } = false;

        // All debug overlay items
        private static List<IDebugOverlayItem> DebugOverlayItems;

        /// <summary>
        /// Creates all <see cref="IDebugOverlayItem"/>s and determines their initial positions
        /// </summary>
        public static void Initialize()
        {
            DebugOverlayItems = new List<IDebugOverlayItem>()
            {
                // Top Left
                new FPSItem() { Offset = new Vector2(4, 0)},
                new LastUsedInputItem() {Offset = new Vector2(4, 12)},

                // Top Right
                new CoordinateItem() {Offset = new Vector2(-4, 0), horizontalAlignment = HorizontalAlignment.Right},
                new CameraCoordinatesItem() {Offset = new Vector2(-4, 12), horizontalAlignment = HorizontalAlignment.Right},
                new ZoomItem() {Offset = new Vector2(-4, 24), horizontalAlignment = HorizontalAlignment.Right },

                // Bottom Left
                new ProcessorArchitectureItem() {Offset = new Vector2(4, -14), verticalAlignment = VerticalAlignment.Bottom},
                new GraphicsDeviceItem() {Offset = new Vector2(4, -2), verticalAlignment = VerticalAlignment.Bottom},

                // Bottom Right
                new MonoGameItem() {Offset = new Vector2(-4, -14), horizontalAlignment = HorizontalAlignment.Right, verticalAlignment = VerticalAlignment.Bottom},
                new AssemblyRuntimeItem() {Offset = new Vector2(-4, -2), horizontalAlignment = HorizontalAlignment.Right, verticalAlignment = VerticalAlignment.Bottom},
            };
            RecalculatePositions();
        }

        /// <summary>
        /// Recalculates the position of all active <see cref="IDebugOverlayItem"/>s when the screen size is changed
        /// </summary>
        public static void RecalculatePositions()
        {
            foreach (IDebugOverlayItem item in DebugOverlayItems)
            {
                item.RecalculatePosition(MainGame.FontSet[Globals.ForegroundFont]);
            }
        }

        /// <summary>
        /// Runs on-tick update logic for each <see cref="IDebugOverlayItem"/>
        /// </summary>
        /// <param name="gameTime">Most recent time state of <see cref="MainGame"/></param>
        public static void Update(GameTime gameTime)
        {
            if (IsDiagnosticsVisible)
            {
                foreach (IDebugOverlayItem item in DebugOverlayItems)
                {
                    item.Update(gameTime);
                }
            }
        }

        /// <summary>
        /// Calls each <see cref="IDebugOverlayItem"/> to render itself
        /// </summary>
        /// <param name="spriteBatch">Most recent <see cref="SpriteBatch"/></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (IsDiagnosticsVisible)
            {
                foreach (IDebugOverlayItem item in DebugOverlayItems)
                {
                    item.Draw(spriteBatch, MainGame.FontSet[Globals.ForegroundFont], Globals.ForegroundColor);
                }
                RecalculatePositions();
            }

            if (IsGridVisible)
            {

            }
        }
    }
}
