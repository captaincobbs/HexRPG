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
        /// <summary>
        /// The current location, in pixels, of the <see cref="IDebugOverlayItem"/> on the screen
        /// </summary>
        public Vector2 Coordinates { get; set; }

        /// <summary>
        /// Offset added to coordinates after alignment is applied
        /// </summary>
        public Vector2 Offset { get; set; }

        /// <summary>
        /// Determines how the <see cref="IDebugOverlayItem"/> is aligned horiziontally
        /// </summary>
        public HorizontalAlignment horizontalAlignment { get; set; }

        /// <summary>
        /// Determines how the <see cref="IDebugOverlayItem"/> is aligned vertically
        /// </summary>
        public VerticalAlignment verticalAlignment { get; set; }

        /// <summary>
        /// Runs logic the <see cref="IDebugOverlayItem"/> needs each frame
        /// </summary>
        /// <param name="gameTime">Current time state of the <see cref="MainGame"/></param>
        public void Update(GameTime gameTime) { }

        /// <summary>
        /// Allows the <see cref="IDebugOverlayItem"/> to render itself
        /// </summary>
        /// <param name="spriteBatch">Most recent <see cref="SpriteBatch"/></param>
        /// <param name="font">The <see cref="SpriteFont"/> the <see cref="IDebugOverlayItem"/> is rendered with</param>
        /// <param name="color">The color that the <see cref="SpriteFont"/> is rendered with</param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color) { }

        /// <summary>
        /// Re-aligns <see cref="IDebugOverlayItem"/> after a change in the screen size
        /// </summary>
        /// <param name="font">The <see cref="SpriteFont"/> used in the <see cref="IDebugOverlayItem"/></param>
        public void RecalculatePosition(SpriteFont font) { }
    }
}
