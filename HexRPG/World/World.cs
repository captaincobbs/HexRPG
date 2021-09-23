using HexRPG.Generation;
using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static HexRPG.Utilities.FileUtilities;

namespace HexRPG.World
{
    public class World
    {
        public Chunk[,] Chunks { get; set; }
        public int worldSize { get; set; }
        public List<Chunk> dirtyChunks { get; set; }

        public World()
        {

        }

        public Tile GetTile(Vector2 coordinates, bool forceLoad)
        {
            return null;
        }

        public Chunk GetChunk(Vector2 coordinates, bool forceLoad)
        {
            return null;
        }
    }
}
