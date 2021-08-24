//using HexRPG.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexRPG
{
    public static class GameOptions
    {
        /// <summary>
        /// Pixel size of a tile on the map, tiles wider or taller than this will be placed as normal.
        /// </summary>
        public const int TileSize = 32;

        /// <summary>
        /// Inertia speed of the camera
        /// </summary>
        public static float InertiaFactor = 0.15f;

        /// <summary>
        /// Inertia speed of the player
        /// </summary>
        public static float MovementInertiaFactor = 0.12f;

        /// <summary>
        /// Length of a chunk in tiles
        /// </summary>
        public static int ChunkSize = 20;

        /// <summary>
        /// Color of the game background when no content blocks it
        /// </summary>
        public static Color BackgroundColor = Color.Black;

        /// <summary>
        /// Determines the sensitivity of horizontal and vertical scrolling, smaller numbers means more sensitivity.
        /// </summary>
        public static float ScrollSensitivity = 5f;

        /// <summary>
        /// Threshold for gamepads on what is considered a button press
        /// </summary>
        public static float AnalogSensitivity = 0.5f;

        /// <summary>
        /// Determines speed of horizontal and vertical scrolling for keys assigned to scrolling mappings
        /// </summary>
        public static float KeyScrollSensitivity = 0.2f;
    }
}
