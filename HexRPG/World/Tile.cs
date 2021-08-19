using HexRPG.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HexRPG.World
{
    /// <summary>
    /// 
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// 
        /// </summary>
        private PointF points;

        private float side;

        /// <summary>
        /// 
        /// </summary>
        private float h;

        private float r;

        private HexOrientation orientation;

        private float x;

        private float y;
    }

    /// <summary>
    /// Determines whether the bottom of the hexagon is a point, or the bottom of the hexagon is flat
    /// </summary>
    public enum HexOrientation
    {
        Flat = 0,
        Pointed = 1,
    }
}
