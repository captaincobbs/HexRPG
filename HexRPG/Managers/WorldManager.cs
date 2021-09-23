using HexRPG.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

// TODO - Fast way to do tile lookups without needing cos/sin to avoid lag

namespace HexRPG.Managers
{
    public static class WorldManager
    {
        public static World.World World { get; set; }

        public static void Initialize()
        {

        }

        public static void UpdateChunkRange(GameTime gameTime, Rectangle area)
        {
            
        }

        public static void DrawChunkRange(GameTime gameTime, Rectangle area)
        {

        }

        public static void Save()
        {

        }

        public static World.World Load()
        {
            return null;
        }

        public static Rectangle GetAreaFromPlayer(Player player)
        {
            return new Rectangle((int)player.Coordinates.X - Globals.ChunkLimit, (int)player.Coordinates.Y - Globals.ChunkLimit, (int)player.Coordinates.X + Globals.ChunkLimit, (int)player.Coordinates.Y + Globals.ChunkLimit);
        }
    }
}
