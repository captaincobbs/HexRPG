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
        public static NoiseGenerator ElevationGenerator = new NoiseGenerator();
        public static NoiseGenerator BiomeGenerator = new NoiseGenerator();
        public static NoiseGenerator WarpBiomeGenerator = new NoiseGenerator();
        public static NoiseGenerator FertilityGenerator = new NoiseGenerator();
        public static NoiseGenerator RichnessGenerator = new NoiseGenerator();
        public static NoiseGenerator DangerGenerator = new NoiseGenerator();

        /// <summary>
        /// Initializes the <see cref="MapGenerator"/>, and prepares the various types of noise generators
        /// </summary>
        public static void Initialize()
        {
            // Biome Generator
            BiomeGenerator.SetNoiseType(NoiseGenerator.NoiseType.Cellular);
            BiomeGenerator.SetFrequency(0.015f);
            BiomeGenerator.SetCellularDistanceFunction(NoiseGenerator.CellularDistanceFunction.Hybrid);
            BiomeGenerator.SetCellularReturnType(NoiseGenerator.CellularReturnType.CellValue);

            // Warp
            WarpBiomeGenerator.SetDomainWarpType(NoiseGenerator.DomainWarpType.OpenSimplex2);
            WarpBiomeGenerator.SetDomainWarpAmp(100f);
            WarpBiomeGenerator.SetFrequency(0.010f);
            WarpBiomeGenerator.SetFractalType(NoiseGenerator.FractalType.DomainWarpIndependent);
            WarpBiomeGenerator.SetFractalLacunarity(1.5f);

            // Fertility
            FertilityGenerator.SetNoiseType(NoiseGenerator.NoiseType.OpenSimplex2);
            FertilityGenerator.SetFrequency(0.005f);
            FertilityGenerator.SetFractalType(NoiseGenerator.FractalType.FBm);
            FertilityGenerator.SetFractalOctaves(2);
            FertilityGenerator.SetFractalLacunarity(-2.0f);

            // Richness


            // Danger
            DangerGenerator.SetNoiseType(NoiseGenerator.NoiseType.Perlin);
            DangerGenerator.SetFrequency(0.06f);
            DangerGenerator.SetFractalType(NoiseGenerator.FractalType.FBm);
            DangerGenerator.SetFractalGain(1.0f);
            DangerGenerator.SetFractalWeightedStrength(0.5f);
        }

        /// <summary>
        /// Changes the seeds of all generators to input integer
        /// </summary>
        /// <param name="seed">Seed to set all generators to</param>
        private static void SetSeed(int seed)
        {
            BiomeGenerator.SetSeed(seed);
            WarpBiomeGenerator.SetSeed(seed);
        }

        public static Tile[,] Generate(Vector2 size, Vector2 chunkCoordinates, int seed)
        {
            SetSeed(seed);
            Tile[,] Tiles = new Tile[(int)size.X, (int)size.Y];

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    Vector2 position = new Vector2(x, y);
                    Tiles[x, y].ChunkCoordinates = chunkCoordinates;
                    Tiles[x, y].Coordinates = position;
                    Tiles[x, y].Biome = GenerateBiome(position);
                    Tiles[x, y].Element = GenerateElement(position);
                }
            }

            return Tiles;
        }

        public static BiomeType GenerateBiome(Vector2 position)
        {
            float xf = position.X;
            float yf = position.Y;

            WarpBiomeGenerator.DomainWarp(ref xf, ref yf);
            float noise = BiomeGenerator.GetNoise(xf, yf);

            return BiomeType.Plains;
        }

        public static Element GenerateElement(Vector2 position)
        {
            float xf = position.X;
            float yf = position.Y;

            WarpBiomeGenerator.DomainWarp(ref xf, ref yf);
            float noise = 0 - BiomeGenerator.GetNoise(xf, yf);

            return Element.Neutral;
        }
    }
}
