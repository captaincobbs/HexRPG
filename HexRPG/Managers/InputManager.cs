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
        /// <summary>
        /// Rebindable actions possible within the game
        /// </summary>
        public enum Action
        {
            /// <summary>
            /// <see cref="Action"/> for accepting/acknowledging dialogue or UI
            /// </summary>
            Accept,
            /// <summary>
            /// <see cref="Action"/> for refusing/cancelling dialogue or UI
            /// </summary>
            Cancel,
            /// <summary>
            /// <see cref="Action"/> for decrementing number boxes
            /// </summary>
            Decrease,
            /// <summary>
            /// <see cref="Action"/> for moving the player or cursor downwards
            /// </summary>
            Down,
            /// <summary>
            /// <see cref="Action"/> for moving the player downwards to the left
            /// </summary>
            DownLeft,
            /// <summary>
            /// <see cref="Action"/> for moving the player downwards to the right
            /// </summary>
            DownRight,
            /// <summary>
            /// <see cref="Action"/> for exiting the program to desktop
            /// </summary>
            ExitGame,
            /// <summary>
            /// <see cref="Action"/> for toggling fullscreen
            /// </summary>
            Fullscreen,
            /// <summary>
            /// <see cref="Action"/> for incrementing number boxes
            /// </summary>
            Increase,
            /// <summary>
            /// <see cref="Action"/> for opening inventory menu
            /// </summary>
            Inventory,
            /// <summary>
            /// <see cref="Action"/> for returning to the previous selected object
            /// </summary>
            Last,
            /// <summary>
            /// <see cref="Action"/> for moving the cursor leftwards
            /// </summary>
            Left,
            /// <summary>
            /// <see cref="Action"/> for opening the pause menu
            /// </summary>
            Menu,
            /// <summary>
            /// <see cref="Action"/> for moving to the next item in a list
            /// </summary>
            Next,
            /// <summary>
            /// <see cref="Action"/> for moving the cursor rightwards
            /// </summary>
            Right,
            /// <summary>
            /// <see cref="Action"/> for opening the status menu
            /// </summary>
            Status,
            /// <summary>
            /// <see cref="Action"/> for moving the player and cursor upwwards
            /// </summary>
            Up,
            /// <summary>
            /// <see cref="Action"/> for moving the player upwards to the left
            /// </summary>
            UpLeft,
            /// <summary>
            /// <see cref="Action"/> for dmoving the player upwards to the right
            /// </summary>
            UpRight
        }

        /// <summary>
        /// General input of all controls simplified into one format
        /// </summary>
        public enum GamePadButton
        {
            Start,
            Back,
            A,
            B,
            X,
            Y,
            Up,
            Down,
            Left,
            Right,
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

        public enum StickDirection
        {
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft,
            Left,
            UpLeft,
        }
        #endregion

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
        }
        #endregion

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
        #endregion

        #region Gamepad Information
        private static GamePadState CurrentGamepadState { get; set; }

        private static GamePadState previousGamepadState { get; set; }
        #endregion

        #region Mouse Information
        private static MouseState CurrentMouseState { get; set; }

        private static MouseState previousMouseState { get; set; }

        public static Point GetCursorPosition()
        {
            return CurrentMouseState.Position;
        }

        public static int ScrollWheelValue { get; set; }
        public static int DeltaScrollWheelValue { get; set; }

        #endregion

        #region Gamepad Buttons
        // Pressed
        public static bool IsGamePadStartPressed()
        {
            return (CurrentGamepadState.Buttons.Start == ButtonState.Pressed);
        }

        public static bool IsGamePadBackPressed()
        {
            return (CurrentGamepadState.Buttons.Back == ButtonState.Pressed);
        }

        public static bool IsGamePadAPressed()
        {
            return (CurrentGamepadState.Buttons.A == ButtonState.Pressed);
        }

        public static bool IsGamePadBPressed()
        {
            return (CurrentGamepadState.Buttons.A == ButtonState.Pressed);
        }

        public static bool IsGamePadXPressed()
        {
            return (CurrentGamepadState.Buttons.X == ButtonState.Pressed);
        }

        public static bool IsGamePadYPressed()
        {
            return (CurrentGamepadState.Buttons.Y == ButtonState.Pressed);
        }

        public static bool IsGamePadLeftShoulderPressed()
        {
            return (CurrentGamepadState.Buttons.LeftShoulder == ButtonState.Pressed);
        }

        public static bool IsGamePadRightShoulderPressed()
        {
            return (CurrentGamepadState.Buttons.RightShoulder == ButtonState.Pressed);
        }

        public static bool IsGamePadDPadUpPressed()
        {
            return (CurrentGamepadState.DPad.Up == ButtonState.Pressed);
        }

        public static bool IsGamePadDPadDownPressed()
        {
            return (CurrentGamepadState.DPad.Down == ButtonState.Pressed);
        }

        public static bool IsGamePadDPadLeftPressed()
        {
            return (CurrentGamepadState.DPad.Left == ButtonState.Pressed);
        }

        public static bool IsGamePadDPadRightPressed()
        {
            return (CurrentGamepadState.DPad.Right == ButtonState.Pressed);
        }

        public static bool IsGamePadLeftTriggerPressed()
        {
            return (CurrentGamepadState.Triggers.Left > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadRightTriggerPressed()
        {
            return (CurrentGamepadState.Triggers.Right > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickPressed()
        {
            return (CurrentGamepadState.Buttons.LeftStick == ButtonState.Pressed);
        }

        public static bool IsGamePadRightStickPressed()
        {
            return (CurrentGamepadState.Buttons.RightStick == ButtonState.Pressed);
        }

        public static bool IsGamePadLeftStickUpPressed()
        {
            return (CurrentGamepadState.ThumbSticks.Left.Y > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickDownPressed()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Left.Y > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickLeftPressed()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Left.X > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickRightPressed()
        {
            return (CurrentGamepadState.ThumbSticks.Left.X > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickUpPressed()
        {
            return (CurrentGamepadState.ThumbSticks.Right.Y > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickDownPressed()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Right.Y > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickLeftPressed()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Right.X > GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickRightPressed()
        {
            return (CurrentGamepadState.ThumbSticks.Right.X > GameOptions.AnalogSensitivity);
        }

        // Triggered
        public static bool IsGamePadStartTriggered()
        {
            return (CurrentGamepadState.Buttons.Start == ButtonState.Pressed) && (previousGamepadState.Buttons.Start == ButtonState.Released);
        }

        public static bool IsGamePadBackTriggered()
        {
            return (CurrentGamepadState.Buttons.Back == ButtonState.Pressed) && (previousGamepadState.Buttons.Back == ButtonState.Released);
        }

        public static bool IsGamePadATriggered()
        {
            return (CurrentGamepadState.Buttons.A == ButtonState.Pressed) && (previousGamepadState.Buttons.A == ButtonState.Released);
        }

        public static bool IsGamePadBTriggered()
        {
            return (CurrentGamepadState.Buttons.B == ButtonState.Pressed) && (previousGamepadState.Buttons.B == ButtonState.Released);
        }

        public static bool IsGamePadXTriggered()
        {
            return (CurrentGamepadState.Buttons.X == ButtonState.Pressed) && (previousGamepadState.Buttons.X == ButtonState.Released);
        }

        public static bool IsGamePadYTriggered()
        {
            return (CurrentGamepadState.Buttons.Y == ButtonState.Pressed) && (previousGamepadState.Buttons.Y == ButtonState.Released);
        }

        public static bool IsGamePadLeftShoulderTriggered()
        {
            return (CurrentGamepadState.Buttons.LeftShoulder == ButtonState.Pressed) && (previousGamepadState.Buttons.LeftShoulder == ButtonState.Released);
        }

        public static bool IsGamePadRightShoulderTriggered()
        {
            return (CurrentGamepadState.Buttons.RightShoulder == ButtonState.Pressed) && (previousGamepadState.Buttons.RightShoulder == ButtonState.Released);
        }

        public static bool IsGamePadDPadUpTriggered()
        {
            return (CurrentGamepadState.DPad.Up == ButtonState.Pressed) && (previousGamepadState.DPad.Up == ButtonState.Released);
        }

        public static bool IsGamePadDPadDownTriggered()
        {
            return (CurrentGamepadState.DPad.Down == ButtonState.Pressed) && (previousGamepadState.DPad.Down == ButtonState.Released);
        }

        public static bool IsGamePadDPadLeftTriggered()
        {
            return (CurrentGamepadState.DPad.Left == ButtonState.Pressed) && (previousGamepadState.DPad.Left == ButtonState.Released);
        }

        public static bool IsGamePadDPadRightTriggered()
        {
            return (CurrentGamepadState.DPad.Right == ButtonState.Pressed) && (previousGamepadState.DPad.Right == ButtonState.Released);
        }

        public static bool IsGamePadLeftTriggerTriggered()
        {
            return (CurrentGamepadState.Triggers.Left > GameOptions.AnalogSensitivity) && (previousGamepadState.Triggers.Left < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadRightTriggerTriggered()
        {
            return (CurrentGamepadState.Triggers.Right > GameOptions.AnalogSensitivity) && (previousGamepadState.Triggers.Right < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickTriggered()
        {
            return (CurrentGamepadState.Buttons.LeftStick == ButtonState.Pressed) && (previousGamepadState.Buttons.LeftStick == ButtonState.Released);
        }

        public static bool IsGamePadRightStickTriggered()
        {
            return (CurrentGamepadState.Buttons.RightStick == ButtonState.Pressed) && (previousGamepadState.Buttons.RightStick == ButtonState.Released);
        }

        public static bool IsGamePadLeftStickUpTriggered()
        {
            return (CurrentGamepadState.ThumbSticks.Left.Y > GameOptions.AnalogSensitivity) && (previousGamepadState.ThumbSticks.Left.Y < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickDownTriggered()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Left.Y > GameOptions.AnalogSensitivity) && (-1f * previousGamepadState.ThumbSticks.Left.Y < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickLeftTriggered()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Left.X > GameOptions.AnalogSensitivity) && (-1f * previousGamepadState.ThumbSticks.Left.X < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamePadLeftStickRightTriggered()
        {
            return (CurrentGamepadState.ThumbSticks.Left.X > GameOptions.AnalogSensitivity) && (previousGamepadState.ThumbSticks.Left.X < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickUpTriggered()
        {
            return (CurrentGamepadState.ThumbSticks.Right.Y > GameOptions.AnalogSensitivity) && (previousGamepadState.ThumbSticks.Right.Y < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickDownTriggered()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Right.Y > GameOptions.AnalogSensitivity) && (-1f * previousGamepadState.ThumbSticks.Right.Y < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickLeftTriggered()
        {
            return (-1f * CurrentGamepadState.ThumbSticks.Right.X > GameOptions.AnalogSensitivity) && (-1f * previousGamepadState.ThumbSticks.Right.X < GameOptions.AnalogSensitivity);
        }

        public static bool IsGamepadRightStickRightTriggered()
        {
            return (CurrentGamepadState.ThumbSticks.Right.X > GameOptions.AnalogSensitivity) && (previousGamepadState.ThumbSticks.Right.X < GameOptions.AnalogSensitivity);
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

                case GamePadButton.Up:
                    return IsGamePadLeftStickUpPressed();

                case GamePadButton.Down:
                    return IsGamePadLeftStickDownPressed();

                case GamePadButton.Left:
                    return IsGamePadLeftStickLeftPressed();

                case GamePadButton.Right:
                    return IsGamePadLeftStickRightPressed();

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

                case GamePadButton.Up:
                    return IsGamePadLeftStickUpTriggered();

                case GamePadButton.Down:
                    return IsGamePadLeftStickDownTriggered();

                case GamePadButton.Left:
                    return IsGamePadLeftStickLeftTriggered();

                case GamePadButton.Right:
                    return IsGamePadLeftStickRightTriggered();

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
        #endregion

        #region Mapping

        public static ActionMapping[] Mappings { get; set; }

        public static void ResetActionMaps()
        {
            Mappings = new ActionMapping[Enum.GetValues(typeof(Action)).Length];

            // Accept
            Mappings[(int)Action.Accept] = new ActionMapping();
            Mappings[(int)Action.Accept].keyboardKeys.Add(Keys.Enter);
            Mappings[(int)Action.Accept].gamePadButtons.Add(GamePadButton.Start);

            // Cancel
            Mappings[(int)Action.Cancel] = new ActionMapping();
            Mappings[(int)Action.Cancel].keyboardKeys.Add(Keys.X);
            Mappings[(int)Action.Cancel].gamePadButtons.Add(GamePadButton.B);

            // Decrease
            Mappings[(int)Action.Decrease] = new ActionMapping();
            Mappings[(int)Action.Decrease].keyboardKeys.Add(Keys.Down);
            Mappings[(int)Action.Decrease].gamePadButtons.Add(GamePadButton.DPadDown);

            // Down
            Mappings[(int)Action.Down] = new ActionMapping();
            Mappings[(int)Action.Decrease].keyboardKeys.Add(Keys.S);
            Mappings[(int)Action.Decrease].gamePadButtons.Add(GamePadButton.DPadDown);

            // DownLeft
            Mappings[(int)Action.DownLeft] = new ActionMapping();

            // DownRight
            Mappings[(int)Action.DownRight] = new ActionMapping();

            // ExitGame
            Mappings[(int)Action.ExitGame] = new ActionMapping();

            // Fullscreen
            Mappings[(int)Action.Fullscreen] = new ActionMapping();

            // Increase
            Mappings[(int)Action.Increase] = new ActionMapping();


            // Inventory
            Mappings[(int)Action.Inventory] = new ActionMapping();

            // Last
            Mappings[(int)Action.Last] = new ActionMapping();

            // Menu
            Mappings[(int)Action.Menu] = new ActionMapping();

            // Next
            Mappings[(int)Action.Next] = new ActionMapping();

            // Right
            Mappings[(int)Action.Right] = new ActionMapping();

            // Status
            Mappings[(int)Action.Status] = new ActionMapping();

            // Up
            Mappings[(int)Action.Up] = new ActionMapping();

            // UpLeft
            Mappings[(int)Action.UpLeft] = new ActionMapping();

            // UpRight
            Mappings[(int)Action.UpRight] = new ActionMapping();
        }

        private static bool IsActionMapPressed(ActionMapping mapping)
        {
            for (int i = 0; i < mapping.keyboardKeys.Count; i++)
            {
                if (IsKeyPressed(mapping.keyboardKeys[i]))
                {
                    return true;
                }
            }
            if (CurrentGamepadState.IsConnected)
            {
                for (int i = 0; i < mapping.gamePadButtons.Count; i++)
                {
                    if (IsGamePadButtonPressed(mapping.gamePadButtons[i]))
                    {
                        return true;
                    }
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
                    return true;
                }
            }

            if (CurrentGamepadState.IsConnected)
            {
                for (int i = 0; i < mapping.gamePadButtons.Count; i++)
                {
                    if (IsGamePadButtonTriggered(mapping.gamePadButtons[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsActionPressed(Action action)
        {
            return IsActionMapPressed(Mappings[(int)action]);
        }

        public static bool IsActionTriggered(Action action)
        {
            return IsActionMapTriggered(Mappings[(int)action]);
        }
        #endregion

        #region Functions

        public static void Reset()
        {
            ResetActionMaps();
        }

        public static void Update()
        {
            previousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            previousGamepadState = CurrentGamepadState;
            CurrentGamepadState = GamePad.GetState(PlayerIndex.One);

            previousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        #endregion
    }
}
