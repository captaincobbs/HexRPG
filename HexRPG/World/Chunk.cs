using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HexRPG.World
{
    public class Chunk
    {
        /// <summary>
        /// 
        /// </summary>
        public int[,] Tiles { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        Dictionary<(int, int), ITileObject> TileObjects { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        bool isLoaded = false;

        /// <summary>
        /// 
        /// </summary>
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