using HexRPG.Dynamic;
using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HexRPG.World
{
    /// <summary>
    /// 
    /// </summary>
    public class Tile
    {
        public Vector2 Coordinates { get; set; }

        public Vector2 ChunkCoordinates { get; set; }

        public List<IEntity> Entities { get; set; } = new List<IEntity>();
    }
}
