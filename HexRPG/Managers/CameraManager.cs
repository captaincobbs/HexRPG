using HexRPG.Entity;
using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using HexRPG.Overlay;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace HexRPG.Dynamic
{
    public static class CameraManager
    {
        public static Matrix Camera { get; private set; }
        public static IFocusObject CameraFocus { get; set; }

        // Camera properties
        /// <summary>
        /// The current value of the Camera Zoom
        /// </summary>
        public static float ZoomValue = 2f;

        /// <summary>
        /// The target value for the Camera Zoom
        /// </summary>
        public static float ZoomTarget = 2f;

        /// <summary>
        /// The current X,Y Coordiates of the Camera
        /// </summary>
        public static Vector2 Coordinates { get; set; } = new Vector2(0, 0);

        /// <summary>
        /// The target X,Y Coordiates of the Camera
        /// </summary>
        public static Vector2 TargetCoordinates { get; set; } = new Vector2(0, 0);

        private static Viewport Viewport { get; set; }

        static float maxCamZoom = 10f;
        static float minCamZoom = 0.75f;

        public static void Initialize(IFocusObject cameraFocus, Viewport viewPort)
        {
            CameraFocus = cameraFocus;
            Viewport = viewPort;
        }

        public static void Update()
        {
            // Apply zoom delta to current zoom value
            ZoomValue += ((ZoomTarget - ZoomValue) * GameOptions.CameraZoomInertia);

            // If camera isn't manually controlled, interporate between target position and current position
            if (CameraFocus.GetType() != typeof(PlayerControlled))
            {
                TargetCoordinates = new Vector2(
                    CameraFocus.GetPosition().X - (GameOptions.TileSize / 2),
                    CameraFocus.GetPosition().Y - (GameOptions.TileSize / 2));
                Coordinates = Vector2.Round(new Vector2(
                    Coordinates.X + (TargetCoordinates.X - Coordinates.X) * GameOptions.CameraScrollInertia,
                    Coordinates.Y + (TargetCoordinates.Y - Coordinates.Y) * GameOptions.CameraScrollInertia));

            }
            // If camera is manually controlled, watch for input
            else
            {
                // INSERT MANUAL CAMERA CONTROL LOGIC
            }

            // Scroll In
            if (InputManager.IsActionPressed(InputManager.InputAction.ZoomIn))
            {
                ZoomTarget += GameOptions.ZoomThreshold * InputManager.GetActionScroll(InputManager.InputAction.ZoomIn);
            }

            // Scroll Out
            if (InputManager.IsActionPressed(InputManager.InputAction.ZoomOut))
            {
                ZoomTarget -= GameOptions.ZoomThreshold * InputManager.GetActionScroll(InputManager.InputAction.ZoomOut);
            }

            // Reset camera zoom
            if (InputManager.IsActionPressed(InputManager.InputAction.ZoomReset))
            {
                ZoomTarget = 2f;
            }

            // Keep camera zoom within a specific range
            ZoomTarget = MathUtilities.Clamp(ZoomTarget, minCamZoom, maxCamZoom);

            // Change camera matrix properties with updated information
            Camera =
                Matrix.CreateTranslation(new Vector3(-Coordinates.X, -Coordinates.Y, 0)) *
                Matrix.CreateScale(new Vector3(ZoomValue, ZoomValue, 1)) *
                Matrix.CreateTranslation(new Vector3(MainGame.GameWindow.ClientBounds.Width * 0.5f, MainGame.GameWindow.ClientBounds.Height * 0.5f, 0));
        }
    }

    #region Focus Objects
    /// <summary>
    /// Object to store information about the current focus of the ca
    /// </summary>
    public interface IFocusObject
    {
        public Vector2 GetPosition()
        {
            return new Vector2(0, 0);
        }
    }

    /// <summary>
    /// <see cref="IFocusObject"/> for tracking an input entity with the camera
    /// </summary>
    public class EntityFocus : IFocusObject
    {
        private IEntity focusObject { get; set; }

        /// <summary>
        /// <see cref="IFocusObject"/> for tracking an input entity with the camera
        /// </summary>
        /// <param name="focusObject"><see cref="IEntity"/> to track</param>
        public EntityFocus(IEntity focusObject)
        {
            this.focusObject = focusObject;
        }

        /// <summary>
        /// Returns current position of the tracked entity in the <see cref="IFocusObject"/>
        /// </summary>
        /// <returns><see cref="Vector2"/> of the current position of the tracked entity</returns>
        public Vector2 GetPosition()
        {
            return new Vector2(focusObject.Coordinates.X * GameOptions.TileSize, focusObject.Coordinates.Y * GameOptions.TileSize);
        }
    }

    /// <summary>
    /// <see cref="IFocusObject"/> for storing a static coordinate as the camera's target position
    /// </summary>
    public class StaticFocus : IFocusObject
    {
        private Vector2 staticPosition { get; set; }

        /// <summary>
        /// <see cref="IFocusObject"/> for storing a static coordinate as the camera's target position
        /// </summary>
        /// <param name="staticPosition"><see cref="Vector2"/> coordinates to focus the camera on</param>
        public StaticFocus(Vector2 staticPosition)
        {
            this.staticPosition = staticPosition;
        }

        /// <summary>
        /// Returns the static position stored in the <see cref="IFocusObject"/>
        /// </summary>
        /// <returns><see cref="Vector2"/> of Static Coordinate</returns>
        public Vector2 GetPosition()
        {
            return staticPosition;
        }
    }

    public class PlayerControlled : IFocusObject
    {
        private Vector2 cameraPosition { get; set; }

        public PlayerControlled(Vector2 cameraPosition)
        {
            this.cameraPosition = cameraPosition;
        }

        public Vector2 GetPosition()
        {
            return cameraPosition;
        }

        public void SetPosition(Vector2 cameraPosition)
        {
            this.cameraPosition = cameraPosition;
        }
    }
    #endregion
}
