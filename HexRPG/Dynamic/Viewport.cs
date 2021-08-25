using HexRPG.Entity;
using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexRPG.Dynamic
{
    public static class ViewPort
    {
        public static Matrix Camera {get; private set;}
        public static CameraFocus cameraFocus { get; set; }

        // Camera properties
        public static float CamZoom = 2f;
        public static float CamZoomDest = 2f;
        static float maxCamZoom = 10f;
        static float minCamZoom = 0.75f;
        static int camX = 0;
        static int camY = 0;
        private static int camXDest = 0;
        private static int camYDest = 0;
        private static Player player { get; set; }

        public static void Initialize(CameraFocus cameraFocus, Player player)
        {
            ViewPort.cameraFocus = cameraFocus;
            ViewPort.player = player;
        }

        public static void Update(GameWindow window)
        {
            if(cameraFocus == CameraFocus.Player)
            {
                camXDest = (int)((player.Coordinate.X * GameOptions.TileSize) - (GameOptions.TileSize / 2));
                camYDest = (int)((player.Coordinate.Y * GameOptions.TileSize) - (GameOptions.TileSize / 2));
                camX += (int)((camXDest - camX) * GameOptions.InertiaFactor);
                camY += (int)((camYDest - camY) * GameOptions.InertiaFactor);

                // Scrolling
                CamZoom += ((CamZoomDest - CamZoom) * GameOptions.InertiaFactor);

                // Scroll In
                if (InputManager.IsActionPressed(InputManager.InputAction.ZoomIn))
                {
                    CamZoomDest += GameOptions.ZoomThreshold * InputManager.GetActionScroll(InputManager.InputAction.ZoomIn);
                }

                // Scroll Out
                if (InputManager.IsActionPressed(InputManager.InputAction.ZoomOut))
                {
                    CamZoomDest -= GameOptions.ZoomThreshold * InputManager.GetActionScroll(InputManager.InputAction.ZoomOut);
                }

                // Reset camera zoom
                if (InputManager.IsActionPressed(InputManager.InputAction.ZoomReset))
                {
                    CamZoomDest = 2f;
                }

                // Keep camera zoom within a specific range
                CamZoomDest = MathUtilities.ContainInRange(CamZoomDest, minCamZoom, maxCamZoom);
                CamZoom = MathUtilities.ContainInRange(CamZoom, minCamZoom, maxCamZoom);

                // Change camera matrix properties with updated information
                Camera =
                    Matrix.CreateTranslation(new Vector3(-camX, -camY, 0)) *
                    Matrix.CreateScale(new Vector3(CamZoom, CamZoom, 1)) *
                    Matrix.CreateTranslation(new Vector3(MainGame.GameWindow.ClientBounds.Width * 0.5f,
                    MainGame.GameWindow.ClientBounds.Height * 0.5f, 0));
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
