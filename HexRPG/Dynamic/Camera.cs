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

        // Camera properties
        public float CamZoom = 2f;
        public float CamZoomDest = 2f;
        float maxCamZoom = 10f;
        float minCamZoom = 1f;
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
                CamZoom += ((CamZoomDest - CamZoom) * GameOptions.InertiaFactor);

                // Scroll In
                if (InputManager.IsActionPressed(InputManager.InputAction.ZoomIn))
                {
                    CamZoomDest += InputManager.GetActionScroll(InputManager.InputAction.ZoomIn);
                }

                // Scroll Out
                if (InputManager.IsActionPressed(InputManager.InputAction.ZoomOut))
                {
                    CamZoomDest += InputManager.GetActionScroll(InputManager.InputAction.ZoomOut);
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
