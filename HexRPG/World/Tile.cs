using HexRPG.Dynamic;
using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static HexRPG.World.Biome;

namespace HexRPG.World
{
    /// <summary>
    /// 
    /// </summary>
    public class Tile
    {
        public Vector2 Coordinates { get; set; }

        public Vector2 ChunkCoordinates { get; set; }

        public BiomeType Biome { get; set; }

        public Tile()
        {

        }
    }
}
