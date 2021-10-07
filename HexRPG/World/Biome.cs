using System;
using System.Collections.Generic;
using System.Text;

namespace HexRPG.World
{
    public static class Biome
    {
        public static BiomeType GetBiomeFromNoise(float noise)
        {
            return BiomeType.Ocean;
        }

        public enum BiomeType
        {
            Ocean =  0,
            Plains = 1,
            Desert = 2,
        }
    }
}
