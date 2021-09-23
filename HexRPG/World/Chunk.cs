using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static HexRPG.Utilities.FileUtilities;

namespace HexRPG.World
{
    public class Chunk
    {
        public int[,] Tiles { get; set; }
        Dictionary<(int, int), ITileObject> TileObjects { get; set; }
        bool isLoaded = false;
        Vector2 Coordinates { get; set; }

        public Chunk()
        {

        }

        public void CreateTiles()
        {

        }

        public void SaveChunk()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public static string GetDirectory(Chunk chunk)
        {
            return "";
        }

        public static string GetPath()
        {
            return "";
        }

        public static Vector2 GetCoordinatesFromPath(string directory)
        {
            Vector2 vector = new Vector2(0, 0);
            return vector;
        }

        public static Chunk LoadChunk(string filePath)
        {
            return null;
        }
    }
}
