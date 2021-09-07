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
        public static Matrix Camera { get; private set; }
        public static CameraFocus cameraFocus { get; set; }

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

        public static void Initialize(CameraFocus cameraFocus)
        {
            ViewPort.cameraFocus = cameraFocus;
        }

        public static void Update(GameWindow window)
        {
            // Apply zoom delta to current zoom value
            ZoomValue += ((ZoomDestination - ZoomValue) * GameOptions.CameraZoomInertia);

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
            ZoomValue = MathUtilities.Clamp(ZoomValue, minCamZoom, maxCamZoom);

            // Change camera matrix properties with updated information
            Camera =
                Matrix.CreateTranslation(new Vector3(Coordinates, 0)) *
                Matrix.CreateScale(new Vector3(ZoomValue, ZoomValue, 1)) *
                Matrix.CreateTranslation(new Vector3(MainGame.GameWindow.ClientBounds.Width / 2,
                MainGame.GameWindow.ClientBounds.Height / 2, 0));
        }

        public static void Follow()
        {

        }
    }
    
    public class CameraFocus
    {
        public IFocusObject FocusObject { get; set; }
        public CameraFocus(IFocusObject focusObject)
        {
            FocusObject = focusObject;
        }
    }

    public interface IFocusObject
    {
        public Vector2 GetPosition()
        {
            return new Vector2(0, 0);
        }
    }

    public class EntityFocus : IFocusObject
    {
        private IEntity focusObject { get; set; }

        public EntityFocus(IEntity focusObject)
        {
            this.focusObject = focusObject;
        }

        public Vector2 GetPosition()
        {
            return focusObject.AnimCoordinate;
        }
    }

    public class StaticFocus : IFocusObject
    {
        private Vector2 staticPosition { get; set; }

        public StaticFocus(Vector2 staticPosition)
        {
            this.staticPosition = staticPosition;
        }

        public Vector2 GetPosition()
        {
            return staticPosition;
        }
    }
}
