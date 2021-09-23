﻿//using HexRPG.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static HexRPG.Utilities.FileUtilities;
using HexRPG.Fonts;

namespace HexRPG
{
    /// <summary>
    /// Location for storing all global static game variables
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Pixel size of a tile on the map, tiles wider or taller than this will be placed as normal.
        /// </summary>
        public const int TileSize = 32;

        /// <summary>
        /// Inertia rate of the camera when scrolling
        /// </summary>
        public const float CameraScrollInertia = 0.07f;

        /// <summary>
        /// Inertia rate of the camera when zooming
        /// </summary>
        public const float CameraZoomInertia = 0.15f;

        /// <summary>
        /// Inertia speed of the player
        /// </summary>
        public const float MovementInertiaFactor = 0.12f;

        /// <summary>
        /// Length of a chunk in tiles
        /// </summary>
        public const int ChunkSize = 5;

        /// <summary>
        /// How many chunks are rendered in any direction near the player
        /// </summary>
        public const int ChunkLimit = 5;

        /// <summary>
        /// Color of the game background when no content blocks it
        /// </summary>
        public static readonly Color BackgroundColor = Color.Black;

        /// <summary>
        /// Font color of debug text
        /// </summary>
        public static readonly Color ForegroundColor = Color.Green;

        /// <summary>
        /// Index of <see cref="SpriteFont"/> in <see cref="FontLibrary"/> to be used for debug text
        /// </summary>
        public const int ForegroundFont = 0;

        /// <summary>
        /// Color of the debug overlay grid
        /// </summary>
        public static readonly Color GridColor = Color.Red;

        /// <summary>
        /// Determines the sensitivity of horizontal and vertical scrolling mappings, smaller numbers means more sensitivity.
        /// </summary>
        public const float ScrollSensitivity = 10f;

        /// <summary>
        /// Threshold for gamepads on what is considered a button press
        /// </summary>
        public const float AnalogSensitivity = 0.5f;

        /// <summary>
        /// Determines speed of horizontal and vertical scrolling for non-scroll inputs assigned to scrolling mappings, lower speed equals higher sensitivity.
        /// </summary>
        public const float InputSensitivity = 15f;

        /// <summary>
        /// Determines the amount of "zoom" applied to the camera per scroll action
        /// </summary>
        public const float ZoomThreshold = 0.5f;

        /// <summary>
        /// Determines the format in which user files will be exported as
        /// </summary>
        public readonly static SaveType SaveType = SaveType.JSON;
    }
}