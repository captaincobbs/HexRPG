using HexRPG.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static HexRPG.Utilities.UIUtilities;

namespace HexRPG.Overlay
{
    /// <summary>
    /// <see cref="IDebugOverlayItem"/> that displays current / target zoom level.
    /// </summary>
    public class Zoom : IDebugOverlayItem
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Vector2 Coordinates { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public Vector2 Offset { get; set; } = new Vector2(0, 0);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public HorizontalAlignment horizontalAlignment { get; set; } = HorizontalAlignment.Left;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public VerticalAlignment verticalAlignment { get; set; } = VerticalAlignment.Top;

        /// <summary>
        /// <see cref="IDebugOverlayItem"/> that displays current / target zoom level.
        /// </summary>
        public Zoom()
        {

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="gameTime"><inheritdoc/></param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="spriteBatch"><inheritdoc/></param>
        /// <param name="font"><inheritdoc/></param>
        /// <param name="color"><inheritdoc/></param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {
            spriteBatch.DrawString(font, GetString(), Coordinates, color);
        }

        /// <summary>
        /// Gets the current values, then returns it as a formatted string
        /// </summary>
        /// <returns>Returns formatted label text</returns>
        private string GetString()
        {
            return $"ZOOM: {Math.Round(CameraManager.ZoomValue, 2):0.00} / {CameraManager.ZoomTarget:0.00}";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="font"><inheritdoc/></param>
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
