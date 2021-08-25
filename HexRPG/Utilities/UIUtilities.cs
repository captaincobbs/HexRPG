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
            Manual,
            Right,
            Left,
            Center
        }

        public enum VerticalAlignment
        {
            Manual,
            Top,
            Bottom,
            Center
        }

        public static int HorizontalAlignRight(Rectangle renderArea, Vector2 size)
        {
            return renderArea.Width - (int)size.X;
        }

        public static int HorizontalAlignLeft(Rectangle renderArea, Vector2 size)
        {
            return 0;
        }

        public static int HorizontalAlignCenter(Rectangle renderArea, Vector2 size)
        {
            return (renderArea.Width - (int)size.X) / 2;
        }

        public static int VerticalAlignTop(Rectangle renderArea, Vector2 size)
        {
            return 0;
        }

        public static int VerticalAlignBottom(Rectangle renderArea, Vector2 size)
        {
            return renderArea.Height - (int)size.Y;
        }

        public static int VerticalAlignCenter(Rectangle renderArea, Vector2 size)
        {
            return (renderArea.Height - (int)size.Y) / 2;
        }
    }
}
