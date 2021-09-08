using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexRPG.Utilities
{
    public static class UIUtilities
    {
        public enum HorizontalAlignment
        {
            Left,
            Right,
            Center
        }

        public enum VerticalAlignment
        {
            Top,
            Bottom,
            Center
        }

        /// <summary>
        /// Returns the X coordinate of a horizontally center-aligned box given the height and space it is being centered in.
        /// </summary>
        /// <param name="renderArea"><see cref="Rectangle"/> of the bounds where the object is being aligned</param>
        /// <param name="size"><see cref="Vector2"/> of the width/height of the source object</param>
        /// <returns>Center-aligned X-Coordinate</returns>
        public static int HorizontalAlignRight(Rectangle renderArea, Vector2 size)
        {
            return renderArea.Width - (int)size.X;
        }

        /// <summary>
        /// Returns the X coordinate of a horizontally center-aligned box given the height and space it is being centered in.
        /// </summary>
        /// <param name="renderArea"><see cref="Rectangle"/> of the bounds where the object is being aligned</param>
        /// <param name="size"><see cref="Vector2"/> of the width/height of the source object</param>
        /// <returns>Center-aligned X-Coordinate</returns>
        public static int HorizontalAlignCenter(Rectangle renderArea, Vector2 size)
        {
            return (renderArea.Width - (int)size.X) / 2;
        }

        /// <summary>
        /// Returns the Y coordinate of a vertically bottom-aligned box given the height and space it is being centered in.
        /// </summary>
        /// <param name="renderArea"><see cref="Rectangle"/> of the bounds where the object is being aligned</param>
        /// <param name="size"><see cref="Vector2"/> of the width/height of the source object</param>
        /// <returns>Bottom-aligned Y-Coordinate</returns>
        public static int VerticalAlignBottom(Rectangle renderArea, Vector2 size)
        {
            return renderArea.Height - (int)size.Y;
        }

        /// <summary>
        /// Returns the Y coordinate of a vertically center-aligned box given the height and space it is being centered in.
        /// </summary>
        /// <param name="renderArea"><see cref="Rectangle"/> of the bounds where the object is being aligned</param>
        /// <param name="size"><see cref="Vector2"/> of the width/height of the source object</param>
        /// <returns>Center-aligned Y-Coordinate</returns>
        public static int VerticalAlignCenter(Rectangle renderArea, Vector2 size)
        {
            return (renderArea.Height - (int)size.Y) / 2;
        }
    }
}
