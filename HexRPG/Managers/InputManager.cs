using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace HexRPG.Entity
{
    /// <summary>
    /// Handles all keyboard, gamepad, and mouse input in the game
    /// </summary>
    public static class InputManager
    {
        #region Enumeration

        public enum InputType
        {
            Keyboard,
            GamePad,
            Mouse,
        }

        public static string[] InputTypeLabel = { "Keyboard", "GamePad", "Mouse" };

        /// <summary>
        /// General input of all gamepad controls simplified into one format
        /// </summary>
        public enum GamePadButton
        {
            Start,
            Back,
            A,
            B,
            X,
            Y,
            LeftStickUp,
            LeftStickDown,
            LeftStickLeft,
            LeftStickRight,
            RightStickUp,
            RightStickDown,
            RightStickLeft,
            RightStickRight,
            LeftShoulder,
            RightShoulder,
            LeftTrigger,
            RightTrigger,
            DPadUp,
            DPadDown,
            DPadLeft,
            DPadRight,
            LeftStick,
            RightStick,
        }

        /// <summary>
        /// Rebindable actions possible within the game
        /// </summary>
        public enum InputAction
        {
            /// <summary>
            /// <see cref="InputAction"/> for accepting/acknowledging dialogue or UI
            /// </summary>
            Accept,

            /// <summary>
            /// <see cref="InputAction"/> for refusing/cancelling dialogue or UI
            /// </summary>
            Cancel,

            /// <summary>
            /// <see cref="InputAction"/> for decrementing number boxes
            /// </summary>
            Decrease,

            /// <summary>
            /// <see cref="InputAction"/> for moving the player or cursor downwards
            /// </summary>
            Down,

            /// <summary>
            /// <see cref="InputAction"/> for moving the player downwards to the left
            /// </summary>
            DownLeft,

            /// <summary>
            /// <see cref="InputAction"/> for moving the player downwards to the right
            /// </summary>
            DownRight,

            /// <summary>
            /// <see cref="InputAction"/> for exiting the program to desktop
            /// </summary>
            ExitGame,

            /// <summary>
            /// <see cref="InputAction"/> for toggling fullscreen
            /// </summary>
            Fullscreen,

            /// <summary>
            /// <see cref="InputAction"/> for incrementing number boxes
            /// </summary>
            Increase,

            /// <summary>
            /// <see cref="InputAction"/> for opening inventory menu
            /// </summary>
            Inventory,

            /// <summary>
            /// <see cref="InputAction"/> for returning to the previous selected object
            /// </summary>
            Last,

            /// <summary>
            /// <see cref="InputAction"/> for moving the cursor leftwards
            /// </summary>
            Left,

            /// <summary>
            /// <see cref="InputAction"/> for opening the pause menu
            /// </summary>
            Menu,

            /// <summary>
            /// <see cref="InputAction"/> for moving to the next item in a list
            /// </summary>
            Next,

            /// <summary>
            /// <see cref="InputAction"/> for moving the cursor rightwards
            /// </summary>
            Right,

            /// <summary>
            /// <see cref="InputAction"/> for opening the status menu
            /// </summary>
            Status,

            /// <summary>
            /// <see cref="InputAction"/> for moving the player and cursor upwwards
            /// </summary>
            Up,

            /// <summary>
            /// <see cref="InputAction"/> for moving the player upwards to the left
            /// </summary>
            UpLeft,

            /// <summary>
            /// <see cref="InputAction"/> for moving the player upwards to the right
            /// </summary>
            UpRight,

            /// <summary>
            /// <see cref="InputAction"/> for scrolling the camera in
            /// </summary>
            ZoomIn,

            /// <summary>
            /// <see cref="InputAction"/> for resetting camera to default state
            /// </summary>
            ZoomReset,

            /// <summary>
            /// <see cref="InputAction"/> for scrolling the camera out
            /// </summary>
            ZoomOut,

            /// <summary>
            /// <see cref="InputAction"/> for toggling visibility of debug overlay
            /// </summary>
            DebugToggle,
        }
        /// <summary>
        /// General input of all mice controls simplified into one format
        /// </summary>
        public enum MouseAction
        {
            LeftClick,
            RightClick,
            MiddleClick,
            ScrollOut,
            ScrollIn,
            HorizontalScrollLeft,
            HorizontalScrollRight,
        }

        #endregion Enumeration

        #region Mapping

        /// <summary>
        /// Gamepad Buttons and Keyboard Keys mapped to an action
        /// </summary>
        public class ActionMapping
        {
            /// <summary>
            /// List of Gamepad Buttons mapped to a given action
            /// </summary>
            public List<GamePadButton> gamePadButtons = new List<GamePadButton>();

            /// <summary>
            /// List of Keyboard Keys mapped to a given action
            /// </summary>
            public List<Keys> keyboardKeys = new List<Keys>();

            /// <summary>
            /// List of Mouse Actions mapped to a given action
            /// </summary>
            public List<MouseAction> mouseActions = new List<MouseAction>();
        }

        #endregion Mapping

        #region States

        /// <summary>
        /// Indicates if the game is the focused application
        /// </summary>
        public static bool IsActive { get; set; } = false;

        #endregion States

        #region Keyboard Information

        /// <summary>
        /// Keyboard State from most recent update
        /// </summary>
        public static KeyboardState CurrentKeyboardState { get; private set; }

        /// <summary>
        /// Keyboard State from the previous update
        /// </summary>
        private static KeyboardState previousKeyboardState;

        /// <summary>
        /// Checks if a key is pressed in most recent update
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>Whether key is pressed or not</returns>
        public static bool IsKeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks if a key was just pressed in most recent update
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>Whether key was triggered or not</returns>
        public static bool IsKeyTriggered(Keys key)
        {
            return (CurrentKeyboardState.IsKeyDown(key)) && (!previousKeyboardState.IsKeyDown(key));
        }

        #endregion Keyboard Information

        #region Gamepad Information
        /// <summary>
        /// Gamepad State from the most recent update
        /// </summary>
        private static GamePadState CurrentGamepadState { get; set; }

        /// <summary>
        /// Gamepad State from the previous update
        /// </summary>
        private static GamePadState previousGamepadState { get; set; }

        #endregion Gamepad Information

        #region Mouse Information
        /// <summary>
        /// Mouse State from the most recent update
        /// </summary>
        public static MouseState CurrentMouseState { get; private set; }

        /// <summary>
        /// Mouse State from the previous update
        /// </summary>
        private static MouseState previousMouseState { get; set; }

        /// <summary>
        /// Delta in scroll value of the horizontal scroll wheel multiplied by the scroll sensitivity
        /// </summary>
        private static int ScrollHorizontalWheelDistance { get { return (int)Math.Round(CurrentDeltaHorizontalScrollWheelValue * (GameOptions.ScrollSensitivity / 1000f)); } }

        /// <summary>
        /// Delta in scroll value of the vertical scroll wheel multiplied by the scroll sensitivity
        /// </summary>
        private static int ScrollWheelDistance { get { return (int)Math.Round(CurrentDeltaScrollWheelValue * (GameOptions.ScrollSensitivity / 1000f)); } }

        /// <summary>
        /// Change in horizontal scroll distance since most recent update
        /// </summary>
        private static int CurrentDeltaHorizontalScrollWheelValue { get; set; }

        /// <summary>
        /// Change in vertical scroll distance since most recent update
        /// </summary>
        private static int CurrentDeltaScrollWheelValue { get; set; }

        /// <summary>
        /// Value of the horizontal scroll wheel as of the most recent update
        /// </summary>
        private static int CurrentHorizontalScrollWheelValue { get; set; }

        /// <summary>
        /// Value of the horizontal scroll wheel as of the most recent update
        /// </summary>
        private static int CurrentScrollWheelValue { get; set; }

        /// <summary>
        /// Change in the horizontal scroll distance since the previous update
        /// </summary>
        private static int PreviousDeltaHorizontalScrollWheelValue { get; set; }

        /// <summary>
        /// Change in vertical scroll distance since the previous update
        /// </summary>
        private static int PreviousDeltaScrollWheelValue { get; set; }

        /// <summary>
        /// Value of the horizontal scroll wheel as of the previous update
        /// </summary>
        private static int PreviousHorizontalScrollWheelValue { get; set; }

        /// <summary>
        /// Value of the vertical scroll wheel as of the previous update
        /// </summary>
        private static int PreviousScrollWheelValue { get; set; }

        /// <summary>
        /// Returns the current coordinates of the mouse
        /// </summary>
        /// <returns><see cref="Vector2"/> of the X,Y coordinate of the Mouse</returns>
        public static Vector2 GetCursorPosition()
        {
            return new Vector2(CurrentMouseState.Position.X, CurrentMouseState.Position.Y);
        }

        /// <summary>
        /// Returns the distance traveled by the mouse since the last update
        /// </summary>
        /// <returns><seealso cref="Vector2"/> of the distance traveled by the mouse since the last update</returns>
        public static Vector2 GetMouseMovement()
        {
            return new Vector2(previousMouseState.Position.X - CurrentMouseState.Position.X, previousMouseState.Position.Y - CurrentMouseState.Position.Y);
        }

        /// <summary>
        /// Checks to see whether mouse actions is occuring as of the last update, including vertical and horizontal scrolling.
        /// </summary>
        /// <param name="action"><see cref="MouseAction"/> to check is occuring</param>
        /// <returns>Whether the <see cref="MouseAction"/> is occurring or not</returns>
        public static bool IsClickPressed(MouseAction action)
        {
            switch (action)
            {
                case MouseAction.LeftClick:
                    return CurrentMouseState.LeftButton == ButtonState.Pressed;

                case MouseAction.MiddleClick:
                    return CurrentMouseState.MiddleButton == ButtonState.Pressed;

                case MouseAction.RightClick:
                    return CurrentMouseState.RightButton == ButtonState.Pressed;

                case MouseAction.ScrollIn:
                    return CurrentDeltaScrollWheelValue < 0;

                case MouseAction.ScrollOut:
                    return CurrentDeltaScrollWheelValue > 0;

                case MouseAction.HorizontalScrollLeft:
                    return CurrentDeltaHorizontalScrollWheelValue < 0;

                case MouseAction.HorizontalScrollRight:
                    return CurrentDeltaHorizontalScrollWheelValue > 0;

                default:
                    return false;
            };
        }

        /// <summary>
        /// Checks to see whether mouse actions was just triggered as of the last update, including vertical and horizontal scrolling.
        /// </summary>
        /// <param name="action"><see cref="MouseAction"/> to check if triggered</param>
        /// <returns>Whether the <see cref="MouseAction"/> just triggered or not</returns>
        public static bool IsClickTriggered(MouseAction action)
        {
            switch (action)
            {
                case MouseAction.LeftClick:
                    return (CurrentMouseState.LeftButton == ButtonState.Pressed) && !(previousMouseState.LeftButton == ButtonState.Pressed);

                case MouseAction.MiddleClick:
                    return (CurrentMouseState.MiddleButton == ButtonState.Pressed) && !(previousMouseState.MiddleButton == ButtonState.Pressed);

                case MouseAction.RightClick:
                    return (CurrentMouseState.RightButton == ButtonState.Pressed) && !(previousMouseState.RightButton == ButtonState.Pressed);

                case MouseAction.ScrollIn:
                    return (CurrentDeltaScrollWheelValue > 0) && (CurrentDeltaScrollWheelValue != PreviousDeltaScrollWheelValue);

                case MouseAction.ScrollOut:
                    return (CurrentDeltaScrollWheelValue < 0) && (CurrentDeltaScrollWheelValue != PreviousDeltaScrollWheelValue);

                case MouseAction.HorizontalScrollLeft:
                    return (CurrentDeltaHorizontalScrollWheelValue < 0) && (CurrentDeltaScrollWheelValue != PreviousDeltaScrollWheelValue);

                case MouseAction.HorizontalScrollRight:
                    return (CurrentDeltaHorizontalScrollWheelValue > 0) && (CurrentDeltaHorizontalScrollWheelValue != PreviousDeltaHorizontalScrollWheelValue);

                default:
                    return false;
            };
        }

        /// <summary>
        /// Checks whether any horizontal scrolling is detected
        /// </summary>
        /// <returns>Whether any horizontal scrolling is detected</returns>
        public static bool IsHorizontalScrolling()
        {
            return CurrentDeltaHorizontalScrollWheelValue != CurrentHorizontalScrollWheelValue;
        }

        /// <summary>
        /// Checks whether any vertical scrolling is detected
        /// </summary>
        /// <returns>Whether any vertical scrolling is detected</returns>
        public static bool IsVerticalScrolling()
        {
            return CurrentDeltaScrollWheelValue != CurrentScrollWheelValue;
        }
        #endregion Mouse Information

        #region Gamepad Buttons

        /// <summary>
        /// Checks whether A on the GamePad is held down
        /// </summary>
        /// <returns>Whether A on the GamePad is held down</returns>
        public static bool IsGamePadAPressed()
        {
            return (CurrentGamepadState.Buttons.A == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks whether A on the GamePad was just triggered
        /// </summary>
        /// <returns>Whether A on the GamePad was just triggered</returns>
        public static bool IsGamePadATriggered()
        {
            return (CurrentGamepadState.Buttons.A == ButtonState.Pressed) && (previousGamepadState.Buttons.A == ButtonState.Released);
        }

        /// <summary>
        /// Checks whether Back on the GamePad is held down
        /// </summary>
        /// <returns>Whether Back on the GamePad is held down</returns>
        public static bool IsGamePadBackPressed()
        {
            return (CurrentGamepadState.Buttons.Back == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks whether Back on the GamePad was just triggered
        /// </summary>
        /// <returns>Whether Back on the GamePad was just triggered</returns>
        public static bool IsGamePadBackTriggered()
        {
            return (CurrentGamepadState.Buttons.Back == ButtonState.Pressed) && (previousGamepadState.Buttons.Back == ButtonState.Released);
        }

        /// <summary>
        /// Checks whether B on the GamePad is held down
        /// </summary>
        /// <returns>Whether B on the GamePad is held down</returns>
        public static bool IsGamePadBPressed()
        {
            return (CurrentGamepadState.Buttons.B == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks whether B on the GamePad was just triggered
        /// </summary>
        /// <returns>Whether B on the GamePad was just triggered</returns>
        public static bool IsGamePadBTriggered()
        {
            return (CurrentGamepadState.Buttons.B == ButtonState.Pressed) && (previousGamepadState.Buttons.B == ButtonState.Released);
        }

        /// <summary>
        /// Checks whether DPad Down on the GamePad is held down
        /// </summary>
        /// <returns>Whether DPad Down on the GamePad is held down</returns>
        public static bool IsGamePadDPadDownPressed()
        {
            return (CurrentGamepadState.DPad.Down == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks whether DPad Down on the GamePad was just triggered
        /// </summary>
        /// <returns>Whether DPad Down on the GamePad was just triggered</returns>
        public static bool IsGamePadDPadDownTriggered()
        {
            return (CurrentGamepadState.DPad.Down == ButtonState.Pressed) && (previousGamepadState.DPad.Down == ButtonState.Released);
        }

        /// <summary>
        /// Checks whether DPad Left on the GamePad is held down
        /// </summary>
        /// <returns>Whether DPad Left on the GamePad is held down</returns>
        public static bool IsGamePadDPadLeftPressed()
        {
            return (CurrentGamepadState.DPad.Left == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks whether DPad Left on the GamePad was just triggered
        /// </summary>
        /// <returns>Whether DPad Left on the GamePad was just triggered</returns>
        public static bool IsGamePadDPadLeftTriggered()
        {
            return (CurrentGamepadState.DPad.Left == ButtonState.Pressed) && (previousGamepadState.DPad.Left == ButtonState.Released);
        }

        /// <summary>
        /// Checks whether DPad Right on the GamePad is held down
        /// </summary>
        /// <returns>Whether DPad Right on the GamePad is held down</returns>
        public static bool IsGamePadDPadRightPressed()
        {
            return (CurrentGamepadState.DPad.Right == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks whether DPad Right on the GamePad was just triggered
        /// </summary>
        /// <returns>Whether DPad Right on the GamePad was just triggered</returns>
        public static bool IsGamePadDPadRightTriggered()
        {
            return (CurrentGamepadState.DPad.Right == ButtonState.Pressed) && (previousGamepadState.DPad.Right == ButtonState.Released);
        }

        /// <summary>
        /// Checks whether DPad Up on the GamePad is held down
        /// </summary>
        /// <returns>Whether DPad Up on the GamePad is held down</returns>
        public static bool IsGamePadDPadUpPressed()
        {
            return (CurrentGamepadState.DPad.Up == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks whether DPad Up on the GamePad was just triggered
        /// </summary>
        /// <returns>Whether DPad Up on the GamePad was just triggered</returns>
        public static bool IsGamePadDPadUpTriggered()
        {
            return (CurrentGamepadState.DPad.Up == ButtonState.Pressed) && (previousGamepadState.DPad.Up == ButtonState.Released);
        }

        public static bool IsGamePadLeftShoulderPressed()
        {
            return (CurrentGamepadState.Buttons.LeftShoulder == ButtonState.Pressed);
        }

        public static bool IsGamePadLeftShoulderTriggered()
        {
            return (CurrentGamepadState.Buttons.LeftShoulder == ButtonState.Pressed) && (previousGamepadState.Buttons.LeftShoulder == ButtonState.Released);
        }

        public static bool IsGamePadLeftStickDownPressed()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Left.Y > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickDownTriggered()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Left.Y > GameOptions.AnalogSensitivity) && (-1f * previousGamepadState.ThumbSticks.Left.Y < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickLeftPressed()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Left.X > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickLeftTriggered()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Left.X > GameOptions.AnalogSensitivity) && (-1f * previousGamepadState.ThumbSticks.Left.X < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickPressed()
        {
            return (CurrentGamepadState.Buttons.LeftStick == ButtonState.Pressed);
        }

        public static bool IsGamePadLeftStickRightPressed()
        {
            return (CurrentGamepadState.ThumbSticks.Left.X > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickRightTriggered()
        {
            return (CurrentGamepadState.ThumbSticks.Left.X > GameOptions.AnalogSensitivity) && (previousGamepadState.ThumbSticks.Left.X < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickTriggered()
        {
            return (CurrentGamepadState.Buttons.LeftStick == ButtonState.Pressed) && (previousGamepadState.Buttons.LeftStick == ButtonState.Released);
        }

        public static bool IsGamePadLeftStickUpPressed()
        {
            return (CurrentGamepadState.ThumbSticks.Left.Y > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickUpTriggered()
        {
            return (CurrentGamepadState.ThumbSticks.Left.Y > GameOptions.AnalogSensitivity) && (previousGamepadState.ThumbSticks.Left.Y < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftTriggerPressed()
        {
            return (CurrentGamepadState.Triggers.Left > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftTriggerTriggered()
        {
            return (CurrentGamepadState.Triggers.Left > GameOptions.AnalogSensitivity) && (previousGamepadState.Triggers.Left < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadRightShoulderPressed()
        {
            return (CurrentGamepadState.Buttons.RightShoulder == ButtonState.Pressed);
        }

        public static bool IsGamePadRightShoulderTriggered()
        {
            return (CurrentGamepadState.Buttons.RightShoulder == ButtonState.Pressed) && (previousGamepadState.Buttons.RightShoulder == ButtonState.Released);
        }

        public static bool IsGamepadRightStickDownPressed()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Right.Y > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickDownTriggered()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Right.Y > GameOptions.AnalogSensitivity) && (-1f * previousGamepadState.ThumbSticks.Right.Y < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickLeftPressed()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Right.X > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickLeftTriggered()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Right.X > GameOptions.AnalogSensitivity) && (-1f * previousGamepadState.ThumbSticks.Right.X < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadRightStickPressed()
        {
            return (CurrentGamepadState.Buttons.RightStick == ButtonState.Pressed);
        }

        public static bool IsGamepadRightStickRightPressed()
        {
            return (CurrentGamepadState.ThumbSticks.Right.X > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickRightTriggered()
        {
            return (CurrentGamepadState.ThumbSticks.Right.X > GameOptions.AnalogSensitivity) && (previousGamepadState.ThumbSticks.Right.X < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadRightStickTriggered()
        {
            return (CurrentGamepadState.Buttons.RightStick == ButtonState.Pressed) && (previousGamepadState.Buttons.RightStick == ButtonState.Released);
        }

        public static bool IsGamepadRightStickUpPressed()
        {
            return (CurrentGamepadState.ThumbSticks.Right.Y > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickUpTriggered()
        {
            return (CurrentGamepadState.ThumbSticks.Right.Y > GameOptions.AnalogSensitivity) && (previousGamepadState.ThumbSticks.Right.Y < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadRightTriggerPressed()
        {
            return (CurrentGamepadState.Triggers.Right > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadRightTriggerTriggered()
        {
            return (CurrentGamepadState.Triggers.Right > GameOptions.AnalogSensitivity) && (previousGamepadState.Triggers.Right < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadStartPressed()
        {
            return (CurrentGamepadState.Buttons.Start == ButtonState.Pressed);
        }
        public static bool IsGamePadStartTriggered()
        {
            return (CurrentGamepadState.Buttons.Start == ButtonState.Pressed) && (previousGamepadState.Buttons.Start == ButtonState.Released);
        }

        public static bool IsGamePadXPressed()
        {
            return (CurrentGamepadState.Buttons.X == ButtonState.Pressed);
        }

        public static bool IsGamePadXTriggered()
        {
            return (CurrentGamepadState.Buttons.X == ButtonState.Pressed) && (previousGamepadState.Buttons.X == ButtonState.Released);
        }

        public static bool IsGamePadYPressed()
        {
            return (CurrentGamepadState.Buttons.Y == ButtonState.Pressed);
        }
        public static bool IsGamePadYTriggered()
        {
            return (CurrentGamepadState.Buttons.Y == ButtonState.Pressed) && (previousGamepadState.Buttons.Y == ButtonState.Released);
        }
        private static bool IsGamePadButtonPressed(GamePadButton gamePadKey)
        {
            switch (gamePadKey)
            {
                case GamePadButton.Start:
                    return IsGamePadStartPressed();

                case GamePadButton.Back:
                    return IsGamePadBackPressed();

                case GamePadButton.A:
                    return IsGamePadAPressed();

                case GamePadButton.B:
                    return IsGamePadBPressed();

                case GamePadButton.X:
                    return IsGamePadXPressed();

                case GamePadButton.Y:
                    return IsGamePadYPressed();

                case GamePadButton.LeftShoulder:
                    return IsGamePadLeftShoulderPressed();

                case GamePadButton.RightShoulder:
                    return IsGamePadRightShoulderPressed();

                case GamePadButton.LeftTrigger:
                    return IsGamePadLeftTriggerPressed();

                case GamePadButton.RightTrigger:
                    return IsGamePadRightTriggerPressed();

                case GamePadButton.LeftStick:
                    return IsGamePadLeftStickPressed();

                case GamePadButton.RightStick:
                    return IsGamePadRightStickPressed();

                case GamePadButton.LeftStickUp:
                    return IsGamePadLeftStickUpPressed();

                case GamePadButton.LeftStickDown:
                    return IsGamePadLeftStickDownPressed();

                case GamePadButton.LeftStickLeft:
                    return IsGamePadLeftStickLeftPressed();

                case GamePadButton.LeftStickRight:
                    return IsGamePadLeftStickRightPressed();

                case GamePadButton.RightStickUp:
                    return IsGamepadRightStickUpPressed();

                case GamePadButton.RightStickDown:
                    return IsGamepadRightStickDownPressed();

                case GamePadButton.RightStickLeft:
                    return IsGamepadRightStickLeftPressed();

                case GamePadButton.RightStickRight:
                    return IsGamepadRightStickRightPressed();

                case GamePadButton.DPadUp:
                    return IsGamePadDPadUpPressed();

                case GamePadButton.DPadDown:
                    return IsGamePadDPadDownPressed();

                case GamePadButton.DPadLeft:
                    return IsGamePadDPadLeftPressed();

                case GamePadButton.DPadRight:
                    return IsGamePadDPadRightPressed();
            }
            return false;
        }

        private static bool IsGamePadButtonTriggered(GamePadButton gamePadKey)
        {
            switch (gamePadKey)
            {
                case GamePadButton.Start:
                    return IsGamePadStartTriggered();

                case GamePadButton.Back:
                    return IsGamePadBackTriggered();

                case GamePadButton.A:
                    return IsGamePadATriggered();

                case GamePadButton.B:
                    return IsGamePadBTriggered();

                case GamePadButton.X:
                    return IsGamePadXTriggered();

                case GamePadButton.Y:
                    return IsGamePadYTriggered();

                case GamePadButton.LeftShoulder:
                    return IsGamePadLeftShoulderTriggered();

                case GamePadButton.RightShoulder:
                    return IsGamePadRightShoulderTriggered();

                case GamePadButton.LeftTrigger:
                    return IsGamePadLeftTriggerTriggered();

                case GamePadButton.RightTrigger:
                    return IsGamePadRightTriggerTriggered();

                case GamePadButton.LeftStick:
                    return IsGamePadLeftStickTriggered();

                case GamePadButton.RightStick:
                    return IsGamePadRightStickTriggered();

                case GamePadButton.LeftStickUp:
                    return IsGamePadLeftStickUpTriggered();

                case GamePadButton.LeftStickDown:
                    return IsGamePadLeftStickDownTriggered();

                case GamePadButton.LeftStickLeft:
                    return IsGamePadLeftStickLeftTriggered();

                case GamePadButton.LeftStickRight:
                    return IsGamePadLeftStickRightTriggered();

                case GamePadButton.RightStickUp:
                    return IsGamepadRightStickUpTriggered();

                case GamePadButton.RightStickDown:
                    return IsGamepadRightStickDownTriggered();

                case GamePadButton.RightStickLeft:
                    return IsGamepadRightStickLeftTriggered();

                case GamePadButton.RightStickRight:
                    return IsGamepadRightStickRightTriggered();

                case GamePadButton.DPadUp:
                    return IsGamePadDPadUpTriggered();

                case GamePadButton.DPadDown:
                    return IsGamePadDPadDownTriggered();

                case GamePadButton.DPadLeft:
                    return IsGamePadDPadLeftTriggered();

                case GamePadButton.DPadRight:
                    return IsGamePadDPadRightTriggered();
            }
            return false;
        }

        #endregion Gamepad Buttons

        #region Mapping

        public static ActionMapping[] Mappings { get; set; } = new ActionMapping[0];

        public static void DefaultActionMaps()
        {
            Mappings = new ActionMapping[Enum.GetValues(typeof(InputAction)).Length];

            // Accept
            Mappings[(int)InputAction.Accept] = new ActionMapping();
            Mappings[(int)InputAction.Accept].keyboardKeys.Add(Keys.Enter);
            Mappings[(int)InputAction.Accept].gamePadButtons.Add(GamePadButton.A);

            // Cancel
            Mappings[(int)InputAction.Cancel] = new ActionMapping();
            Mappings[(int)InputAction.Cancel].keyboardKeys.Add(Keys.X);
            Mappings[(int)InputAction.Cancel].gamePadButtons.Add(GamePadButton.B);

            // Decrease
            Mappings[(int)InputAction.Decrease] = new ActionMapping();
            Mappings[(int)InputAction.Decrease].keyboardKeys.Add(Keys.Down);
            Mappings[(int)InputAction.Decrease].gamePadButtons.Add(GamePadButton.DPadDown);

            // Down
            Mappings[(int)InputAction.Down] = new ActionMapping();
            Mappings[(int)InputAction.Down].keyboardKeys.Add(Keys.S);
            Mappings[(int)InputAction.Down].gamePadButtons.Add(GamePadButton.LeftStickDown);

            // DownLeft
            Mappings[(int)InputAction.DownLeft] = new ActionMapping();
            Mappings[(int)InputAction.DownLeft].keyboardKeys.Add(Keys.A);

            // DownRight
            Mappings[(int)InputAction.DownRight] = new ActionMapping();
            Mappings[(int)InputAction.DownRight].keyboardKeys.Add(Keys.D);

            // ExitGame
            Mappings[(int)InputAction.ExitGame] = new ActionMapping();
            Mappings[(int)InputAction.ExitGame].keyboardKeys.Add(Keys.Escape);

            // Fullscreen
            Mappings[(int)InputAction.Fullscreen] = new ActionMapping();
            Mappings[(int)InputAction.Fullscreen].keyboardKeys.Add(Keys.F11);

            // Increase
            Mappings[(int)InputAction.Increase] = new ActionMapping();
            Mappings[(int)InputAction.Increase].keyboardKeys.Add(Keys.Up);
            Mappings[(int)InputAction.Increase].gamePadButtons.Add(GamePadButton.DPadUp);

            // Inventory
            Mappings[(int)InputAction.Inventory] = new ActionMapping();
            Mappings[(int)InputAction.Inventory].keyboardKeys.Add(Keys.Tab);
            Mappings[(int)InputAction.Inventory].gamePadButtons.Add(GamePadButton.Back);

            // Last
            Mappings[(int)InputAction.Last] = new ActionMapping();
            Mappings[(int)InputAction.Last].keyboardKeys.Add(Keys.Left);
            Mappings[(int)InputAction.Last].gamePadButtons.Add(GamePadButton.DPadLeft);

            // Left
            Mappings[(int)InputAction.Left] = new ActionMapping();
            Mappings[(int)InputAction.Left].keyboardKeys.Add(Keys.A);
            Mappings[(int)InputAction.Left].gamePadButtons.Add(GamePadButton.LeftStickLeft);

            // Menu
            Mappings[(int)InputAction.Menu] = new ActionMapping();
            Mappings[(int)InputAction.Menu].keyboardKeys.Add(Keys.F);
            Mappings[(int)InputAction.Menu].gamePadButtons.Add(GamePadButton.Start);

            // Next
            Mappings[(int)InputAction.Next] = new ActionMapping();
            Mappings[(int)InputAction.Next].keyboardKeys.Add(Keys.Right);
            Mappings[(int)InputAction.Next].gamePadButtons.Add(GamePadButton.DPadRight);

            // Right
            Mappings[(int)InputAction.Right] = new ActionMapping();
            Mappings[(int)InputAction.Right].keyboardKeys.Add(Keys.D);
            Mappings[(int)InputAction.Right].gamePadButtons.Add(GamePadButton.LeftStickRight);

            // Status
            Mappings[(int)InputAction.Status] = new ActionMapping();
            Mappings[(int)InputAction.Status].keyboardKeys.Add(Keys.X);
            Mappings[(int)InputAction.Status].gamePadButtons.Add(GamePadButton.Start);

            // Up
            Mappings[(int)InputAction.Up] = new ActionMapping();
            Mappings[(int)InputAction.Up].keyboardKeys.Add(Keys.W);
            Mappings[(int)InputAction.Up].gamePadButtons.Add(GamePadButton.LeftStickUp);

            // UpLeft
            Mappings[(int)InputAction.UpLeft] = new ActionMapping();
            Mappings[(int)InputAction.UpLeft].keyboardKeys.Add(Keys.Q);

            // UpRight
            Mappings[(int)InputAction.UpRight] = new ActionMapping();
            Mappings[(int)InputAction.UpRight].keyboardKeys.Add(Keys.E);

            // ZoomIn
            Mappings[(int)InputAction.ZoomIn] = new ActionMapping();
            Mappings[(int)InputAction.ZoomIn].gamePadButtons.Add(GamePadButton.RightStickUp);
            Mappings[(int)InputAction.ZoomIn].gamePadButtons.Add(GamePadButton.A);
            Mappings[(int)InputAction.ZoomIn].mouseActions.Add(MouseAction.ScrollIn);

            // ZoomReset
            Mappings[(int)InputAction.ZoomReset] = new ActionMapping();
            Mappings[(int)InputAction.ZoomReset].mouseActions.Add(MouseAction.MiddleClick);
            Mappings[(int)InputAction.ZoomReset].gamePadButtons.Add(GamePadButton.RightStick);

            // ZoomOut
            Mappings[(int)InputAction.ZoomOut] = new ActionMapping();
            Mappings[(int)InputAction.ZoomOut].gamePadButtons.Add(GamePadButton.RightStickDown);
            Mappings[(int)InputAction.ZoomOut].gamePadButtons.Add(GamePadButton.B);
            Mappings[(int)InputAction.ZoomOut].mouseActions.Add(MouseAction.ScrollOut);

            // DebugToggle
            Mappings[(int)InputAction.DebugToggle] = new ActionMapping();
            Mappings[(int)InputAction.DebugToggle].keyboardKeys.Add(Keys.F1);
        }


        /// <summary>
        ///  Returns whether any of mapped input are pressed
        /// </summary>
        /// <param name="action">Action to check mapped inputs for</param>
        /// <returns>Whether the Action has any pressed mapped inputs</returns>
        public static bool IsActionPressed(InputAction action)
        {
            if (IsActive)
            {
                return IsActionMapPressed(Mappings[(int)action]);
            }
            return false;
        }

        /// <summary>
        ///  Returns whether any of mapped input are pressed
        /// </summary>
        /// <param name="action">Action to check mapped inputs for</param>
        /// <returns>Whether the Action has any pressed mapped inputs</returns>
        public static bool IsActionTriggered(InputAction action)
        {
            if (IsActive)
            {
                return IsActionMapTriggered(Mappings[(int)action]);
            }
            return false;
        }

        public static float GetActionScroll(InputAction action)
        {
            return GetActionMapScroll(Mappings[(int)action]);
        }

        public static InputType LastUsedInput { get; set; } = InputType.Mouse;

        private static bool IsActionMapPressed(ActionMapping mapping)
        {
            for (int i = 0; i < mapping.keyboardKeys.Count; i++)
            {
                if (IsKeyPressed(mapping.keyboardKeys[i]))
                {
                    LastUsedInput = InputType.Keyboard;
                    return true;
                }
            }
            if (CurrentGamepadState.IsConnected)
            {
                for (int i = 0; i < mapping.gamePadButtons.Count; i++)
                {
                    if (IsGamePadButtonPressed(mapping.gamePadButtons[i]))
                    {
                        LastUsedInput = InputType.GamePad;
                        return true;
                    }
                }
            }
            for (int i = 0; i < mapping.mouseActions.Count; i++)
            {
                if (IsClickPressed(mapping.mouseActions[i]))
                {
                    LastUsedInput = InputType.Mouse;
                    return true;
                }
            }
            return false;
        }

        private static bool IsActionMapTriggered(ActionMapping mapping)
        {
            for (int i = 0; i < mapping.keyboardKeys.Count; i++)
            {
                if (IsKeyTriggered(mapping.keyboardKeys[i]))
                {
                    LastUsedInput = InputType.Keyboard;
                    return true;
                }
            }

            if (CurrentGamepadState.IsConnected)
            {
                for (int i = 0; i < mapping.gamePadButtons.Count; i++)
                {
                    if (IsGamePadButtonTriggered(mapping.gamePadButtons[i]))
                    {
                        LastUsedInput = InputType.GamePad;
                        return true;
                    }
                }
            }

            for (int i = 0; i < mapping.mouseActions.Count; i++)
            {
                if (IsClickTriggered(mapping.mouseActions[i]))
                {
                    LastUsedInput = InputType.Mouse;
                    return true;
                }
            }

            return false;
        }

        private static float GetActionMapScroll(ActionMapping mapping)
        {
            for (int i = 0; i < mapping.keyboardKeys.Count; i++)
            {
                if (IsKeyPressed(mapping.keyboardKeys[i]))
                {
                    LastUsedInput = InputType.Keyboard;
                    return GameOptions.ScrollSensitivity / GameOptions.InputSensitivity;
                }
            }

            for (int i = 0; i < mapping.gamePadButtons.Count; i++)
            {
                if (IsGamePadButtonPressed(mapping.gamePadButtons[i]))
                {
                    LastUsedInput = InputType.GamePad;
                    return GameOptions.ScrollSensitivity / GameOptions.ScrollSensitivity / 2;
                }
            }

            for (int i = 0; i < mapping.mouseActions.Count; i++)
            {
                if (IsClickPressed(mapping.mouseActions[i]))
                {
                    LastUsedInput = InputType.Mouse;
                    switch (mapping.mouseActions[i])
                    {
                        case (MouseAction.ScrollIn):
                            return MathUtilities.Clamp(ScrollWheelDistance, -1, 0);
                        case (MouseAction.ScrollOut):
                            return -1 * MathUtilities.Clamp(ScrollWheelDistance, 0, 1);
                        case (MouseAction.HorizontalScrollLeft):
                            return MathUtilities.Clamp(ScrollHorizontalWheelDistance, 0, 1);
                        case (MouseAction.HorizontalScrollRight):
                            return MathUtilities.Clamp(ScrollHorizontalWheelDistance, 1, 0);
                        case (MouseAction.LeftClick):
                            return GameOptions.ScrollSensitivity / 15f;
                        case (MouseAction.RightClick):
                            return GameOptions.ScrollSensitivity / 15f;
                        case (MouseAction.MiddleClick):
                            return GameOptions.ScrollSensitivity / 15f;
                    }
                }
            }

            return 0f;
        }
        #endregion Mapping

        #region Functions

        public static void Initialize()
        {
            DefaultActionMaps();
        }

        public static void Update(bool IsActive, Action exit)
        {
            previousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            previousGamepadState = CurrentGamepadState;
            CurrentGamepadState = GamePad.GetState(PlayerIndex.One);

            previousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            InputManager.IsActive = IsActive;

            // Vertical Scrolling
            PreviousScrollWheelValue = CurrentScrollWheelValue;
            CurrentScrollWheelValue = CurrentMouseState.ScrollWheelValue;
            PreviousDeltaScrollWheelValue = CurrentDeltaScrollWheelValue;
            CurrentDeltaScrollWheelValue = CurrentScrollWheelValue - PreviousScrollWheelValue;

            // Horizontal Scrolling
            PreviousHorizontalScrollWheelValue = CurrentHorizontalScrollWheelValue;
            CurrentHorizontalScrollWheelValue = CurrentMouseState.HorizontalScrollWheelValue;
            PreviousDeltaHorizontalScrollWheelValue = CurrentDeltaHorizontalScrollWheelValue;
            CurrentDeltaHorizontalScrollWheelValue = CurrentHorizontalScrollWheelValue - PreviousHorizontalScrollWheelValue;

            GlobalKeyCommands(exit);
        }

        private static void GlobalKeyCommands(Action exit)
        {
            if (IsActionTriggered(InputAction.Fullscreen))
            {
                MainGame.ToggleFullscreen();
            }

            if (IsActionTriggered(InputAction.ExitGame))
            {
                exit();
            }

            if (IsActionTriggered(InputAction.DebugToggle))
            {
                MainGame.DebugToggle();
            }
        }
        #endregion Functions
    }

}