using HexRPG.Utilities;
using HexRPG.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using HexRPG.Utilities;
using HexRPG.World;
using static HexRPG.World.Biome;

namespace HexRPG.Generation
{
    /// <summary>
    /// Handles terrain generation
    /// </summary>
    public static class MapGenerator
    {
        public static GeneratorUtilities RNG = new GeneratorUtilities();

        public static Tile[,] Generate(Vector2 size, Vector2 chunkCoordinates, int seed)
        {
            RNG.SetNoiseType(GeneratorUtilities.NoiseType.OpenSimplex2);
            RNG.SetSeed(seed);
            Tile[,] Tiles = new Tile[(int)size.X, (int)size.Y];

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    Vector2 position = new Vector2(x, y);
                    Tiles[x, y].ChunkCoordinates = chunkCoordinates;
                    Tiles[x, y].Coordinates = position;
                    Tiles[x, y].Biome = GenerateBiome(position);
                }
            }

            return Tiles;
        }

        public static BiomeType GenerateBiome(Vector2 position)
        {
            return BiomeType.Plains;
        }
    }
}
