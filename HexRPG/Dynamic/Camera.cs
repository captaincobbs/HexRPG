using HexRPG.Entity;
using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexRPG.Dynamic
{
    public class ViewPort
    {
        public Matrix Camera {get; private set;}
        public CameraFocus cameraFocus { get; set; }

        /// <summary>
        /// Change in scroll wheel value since last update
        /// </summary>
        public static int CurrentScrollWheelValue { get; set; }

        /// <summary>
        /// Last recorded scroll wheel update
        /// </summary>
        public static int DeltaScrollWheelValue { get; set; }

        // Camera properties
        float camZoom = 0.5f;
        float camZoomDest = 0.5f;
        float maxCamZoom = 4f;
        float minCamZoom = 0.5f;
        int camX = 0;
        int camY = 0;
        private int camXDest = 0;
        private int camYDest = 0;
        private int windowHeight { get; set; }
        private int windowWidth { get; set; }
        private Player player { get; set; }

        public ViewPort(int windowHeight, int windowWidth, CameraFocus cameraFocus, Player player)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.cameraFocus = cameraFocus;
            this.player = player;
        }

        public void Update(GameWindow window)
        {
            this.windowHeight = window.ClientBounds.Height;
            this.windowWidth = window.ClientBounds.Width;
            if(cameraFocus == CameraFocus.Player)
            {
                camXDest = (int)((player.Coordinate.X * GameOptions.TileSize) - (GameOptions.TileSize / 2));
                camYDest = (int)((player.Coordinate.Y * GameOptions.TileSize) - (GameOptions.TileSize / 2));
                camX += (int)((camXDest - camX) * GameOptions.InertiaFactor);
                camY += (int)((camYDest - camY) * GameOptions.InertiaFactor);

                // Scrolling
                camZoom += ((camZoomDest - camZoom) * GameOptions.InertiaFactor);
                DeltaScrollWheelValue = InputManager.MouseState.ScrollWheelValue - CurrentScrollWheelValue;
                CurrentScrollWheelValue += DeltaScrollWheelValue;
                if (DeltaScrollWheelValue != CurrentScrollWheelValue)
                {
                    camZoomDest += DeltaScrollWheelValue * (GameOptions.ScrollSensitivity / 1000f);
                }

                // Reset camera zoom
                if (InputManager.MouseState.MiddleButton == ButtonState.Pressed)
                {
                    camZoomDest = 0.5f;
                }

                // Keep camera zoom within a specific range
                camZoomDest = MathUtilities.ContainInRange(camZoomDest, minCamZoom, maxCamZoom);
                camZoom = MathUtilities.ContainInRange(camZoom, minCamZoom, maxCamZoom);

                // Change camera matrix properties with updated information
                Camera =
                    Matrix.CreateTranslation(new Vector3(-camX, -camY, 0)) *
                    Matrix.CreateScale(new Vector3(camZoom, camZoom, 1)) *
                    Matrix.CreateTranslation(new Vector3(windowWidth * 0.5f,
                    windowHeight * 0.5f, 0));
            }
        }
    }
    
    public enum CameraFocus
    {
        Player,
        Entity,
        Free
    }
}
