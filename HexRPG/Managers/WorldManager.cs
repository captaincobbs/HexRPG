using HexRPG.Dynamic;
using HexRPG.World;
using Microsoft.Xna.Framework;
using System;

// TODO - Fast way to do tile lookups without needing cos/sin to avoid lag

namespace HexRPG.Managers
{
    public static class WorldManager
    {
        public static World.World World { get; set; }

        public static void Initialize(World.World world)
        {
            World = world;
        }

        public static void Initialize()
        {

            World = new World.World("Test", (uint)new Random().Next(100000000));
        }

        public static void UpdateChunkRange(GameTime gameTime, Rectangle area)
        {
            int yi = 0;
            for (int y = area.Y - area.Width; y < area.Y + area.Width; y++)
            {
                int xi = 0;
                for (int x = area.X - area.Height; x < area.X + area.Height; x++)
                {

                    xi += 1;
                }
                yi += 1;
            }
        }

        public static void DrawChunkRange(GameTime gameTime, Rectangle area)
        {
            for (int x = area.X - area.Width; x < area.X + area.Width; x++)
            {
                for (int y = area.Y - area.Height; y < area.Y + area.Height; y++)
                {

                }
            }
        }

        public static void Save()
        {
            World.Save();
        }

        public static World.World Load()
        {
            throw new NotImplementedException();
        }

        public static Rectangle GetAreaAroundFocus(IFocusObject focus)
        {
            return new Rectangle(
                (int)focus.GetPosition().X - Globals.ChunkLimit,
                (int)focus.GetPosition().Y - Globals.ChunkLimit,
                (int)focus.GetPosition().X + Globals.ChunkLimit,
                (int)focus.GetPosition().Y + Globals.ChunkLimit);
        }

        public static Tile GetCameraFocusTile()
        {
            throw new NotImplementedException();
        }
    }
}
