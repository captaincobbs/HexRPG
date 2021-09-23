using HexRPG.Dynamic;
using HexRPG.Entity;
using HexRPG.World;
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
            CreateWord("Test-1");
        }

        public static void UpdateActiveChunks(Rectangle area)
        {
            World.ActiveChunks = area;
        }

        public static void UpdateChunkRange(GameTime gameTime, Rectangle area)
        {
            for (int x = World.ActiveChunks.X; x < World.ActiveChunks.X + World.ActiveChunks.Width; x++)
            {
                for (int y = World.ActiveChunks.Y; y < World.ActiveChunks.Y + World.ActiveChunks.Height; y++)
                {

                }
            }
        }

        public static void DrawChunkRange(GameTime gameTime, Rectangle area)
        {

        }

        public static void CreateWord(string name)
        {
            World = new World.World(name);
        }

        public static void Save()
        {

        }

        public static World.World Load()
        {
            return null;
        }

        public static Rectangle GetAreaAroundFocus(IFocusObject focus)
        {
            return new Rectangle(
                (int)focus.GetPosition().X - Globals.ChunkLimit,
                (int)focus.GetPosition().Y - Globals.ChunkLimit,
                (int)focus.GetPosition().X + Globals.ChunkLimit,
                (int)focus.GetPosition().Y + Globals.ChunkLimit);
        }
    }
}
