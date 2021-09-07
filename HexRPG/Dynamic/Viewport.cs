using HexRPG.Entity;
using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using HexRPG.Overlay;

namespace HexRPG.Dynamic
{
    public static class ViewPort
    {
        public static Matrix Camera { get; private set; }
        public static IFocusObject CameraFocus { get; set; }

        // Camera properties
        /// <summary>
        /// Current value of the Camera Zoom
        /// </summary>
        public static float ZoomValue = 2f;

        /// <summary>
        /// Destination value for the Camera Zoom over time
        /// </summary>
        public static float ZoomDestination = 2f;

        /// <summary>
        /// Current X,Y Coordiates of the Camera
        /// </summary>
        public static Vector2 Coordinates = new Vector2(0, 0);

        static float maxCamZoom = 10f;
        static float minCamZoom = 0.75f;
        static float horizontalBounds = 0f;
        static float verticalBounds = 0f;
        static float cameraSmoothing = 10;

        public static void Initialize(IFocusObject cameraFocus)
        {
            ViewPort.CameraFocus = cameraFocus;
        }

        public static void Update()
        {
            // Apply zoom delta to current zoom value
            ZoomValue += ((ZoomDestination - ZoomValue) * GameOptions.CameraZoomInertia);

            if (CameraFocus.GetType() != typeof(PlayerControlled))
            {
                Coordinates = Vector2.Lerp(Coordinates, CameraFocus.GetPosition(), 1f);
            }

            // Scroll In
            if (InputManager.IsActionPressed(InputManager.InputAction.ZoomIn))
            {
                ZoomDestination += GameOptions.ZoomThreshold * InputManager.GetActionScroll(InputManager.InputAction.ZoomIn);
            }

            // Scroll Out
            if (InputManager.IsActionPressed(InputManager.InputAction.ZoomOut))
            {
                ZoomDestination -= GameOptions.ZoomThreshold * InputManager.GetActionScroll(InputManager.InputAction.ZoomOut);
            }

            // Reset camera zoom
            if (InputManager.IsActionPressed(InputManager.InputAction.ZoomReset))
            {
                ZoomDestination = 2f;
            }

            // Keep camera zoom within a specific range
            ZoomDestination = MathUtilities.Clamp(ZoomDestination, minCamZoom, maxCamZoom);

            // Change camera matrix properties with updated information
            Camera =
                Matrix.CreateTranslation(new Vector3(Coordinates.X, Coordinates.Y, 0)) *
                Matrix.CreateScale(new Vector3(ZoomValue, ZoomValue, 1)) *
                Matrix.CreateTranslation(new Vector3(MainGame.GameWindow.ClientBounds.Width / 2,
                MainGame.GameWindow.ClientBounds.Height / 2, 0));
        }
    }

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
            return focusObject.AnimCoordinate;
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
        public PlayerControlled()
        {
            throw new NotImplementedException("PlayerControlled : IFocusObject");
        }

        public Vector2 GetPosition()
        {
            return new Vector2(0, 0);
        }
    }
}
