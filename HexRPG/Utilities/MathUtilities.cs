using System;

namespace HexRPG.Utilities
{
    public static class MathUtilities
    {

        // https://www.codeproject.com/KB/graphics/hexagonal_part1/hex_geometry.jpg

        /// <summary>
        /// Calculates height of the right angle connecting two angles in the hexagon
        /// </summary>
        /// <param name="side">Radius of a 30° Hexagon</param>
        /// <returns>Height of a point from the base of the Hexagon</returns>
        public static float CalculateH(float side)
        {
            // Degree is 30 because that is the outside angle of a hexagon
            return (float)(Math.Sin(DegreesToRadians(30)) * side);
        }

        /// <summary>
        /// Calculates width of the right angle connecting two angles in the hexagon
        /// </summary>
        /// <param name="side">Height of the Right Angle</param>
        /// <returns>Radius of a 30° Hexagon</returns>
        public static float CalculateR(float side)
        {
            // Degree is 30 because that is the outside angle of a hexagon
            return (float)(Math.Cos(DegreesToRadians(30)) * side);
        }

        /// <summary>
        /// Converts given angle from Degrees to Radians
        /// </summary>
        /// <param name="degrees">Angle in Degrees</param>
        /// <returns>Angle in Radians</returns>
        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        /// <summary>
        /// Constrains a float to stay within the specified range.
        /// </summary>
        /// <param name="value">Float to constrain</param>
        /// <param name="min">Constrained minimum value</param>
        /// <param name="max">Constrained maximum value</param>
        /// <returns>Returns value if within constraints, otherwise returns the minimum or maximum</returns>
        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static float Min(float value, float min)
        {
            return value < min ? min : value;
        }

        public static float Max(float value, float max)
        {
            return value > max ? max : value;
        }
    }
}
