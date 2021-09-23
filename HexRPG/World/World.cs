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
        /// <summary>
        /// Array of chunks centered around the player that are currently loaded
        /// </summary>
        public Rectangle ActiveChunks { get; set; }

        /// <summary>
        /// The name given to the world by the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of chunks with changed data that will be saved next time there is a save call
        /// </summary>
        private List<Chunk> dirtyChunks { get; set; }

        public World(string name)
        {
            Name = name;
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
