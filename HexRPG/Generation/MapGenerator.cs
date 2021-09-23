using HexRPG.Utilities;
using HexRPG.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static HexRPG.Utilities.FileUtilities;

namespace HexRPG.Generation
{
    /// <summary>
    /// Handles terrain generation
    /// </summary>
    public static class MapGenerator
    {
        public static Tile[,] Generate(Vector2 size, int side, int seed)
        {
            float h = MathUtilities.CalculateH(side);
            float r = MathUtilities.CalculateR(side);

            Vector2 PixelSize = new Vector2(size.X * Globals.TileSize, size.Y * Globals.TileSize);
            Vector2 hexSize = new Vector2(side + h, r + r);
            PixelSize = new Vector2((size.X * hexSize.X) + h, (size.Y * hexSize.Y) + r);


            return new Tile[(int)size.Y, (int)size.X];
        }
    }
}
