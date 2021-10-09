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
        public Chunk[,] ActiveChunks { get; set; }

        /// <summary>
        /// Array of all chunks in the world
        /// </summary>
        public Chunk[,] Chunks;

        /// <summary>
        /// The name given to the world by the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of chunks with changed data that will be saved next time there is a save call
        /// </summary>
        private List<Chunk> dirtyChunks { get; set; }

        private uint _seed { get; set; }

        /// <summary>
        /// Seed used for this world's noise generator
        /// </summary>
        public uint Seed { get { return _seed; } }

        /// <summary>
        /// Creates an instance of the <see cref="World"/> type, which initializes the area around the player
        /// </summary>
        /// <param name="name">The user-defined name of the world, used for ingame display and for determining filepath</param>
        public World(string name)
        {
            Name = name;

            for (int y = -Globals.ChunkLimit + (int)MainGame.Player.Coordinates.Y; y <= Globals.ChunkLimit + MainGame.Player.Coordinates.Y; y++)
            {
                for (int x = -Globals.ChunkLimit + (int)MainGame.Player.Coordinates.X; x <= Globals.ChunkLimit + MainGame.Player.Coordinates.X; x++)
                {
                    Chunks[x, y] = new Chunk();
                }
            }
        }

        /// <summary>
        /// Returns requested tile and loads containing chunk if necessary.
        /// Does not check for valid x or y positions.
        /// Returns null if <paramref name="forceLoad"/> is false and chunk is not loaded
        /// </summary>
        /// <param name="coordinates">X,Y position of the requested tile</param>
        /// <param name="forceLoad">Loads parent chuink if not available synchronously</param>
        /// <returns></returns>
        public Tile GetTile(Vector2 coordinates, bool forceLoad)
        {
            int chunkX = (int)Math.Floor((double)coordinates.X / Globals.ChunkSize);
            int chunkY = (int)Math.Floor((double)coordinates.Y / Globals.ChunkSize);
        }

        /// <summary>
        /// Returns a chunk from the coordinate of a tile
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="forceLoad"></param>
        /// <returns></returns>
        public Chunk GetChunkFromTile(Vector2 coordinates, bool forceLoad)
        {
            return null;
        }

        /// <summary>
        /// Returns a chunk 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="forceLoad"></param>
        /// <returns></returns>
        public Chunk GetChunk(Vector2 coordinates, bool forceLoad)
        {

        }
    }
}
