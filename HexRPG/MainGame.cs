using HexRPG.Dynamic;
using HexRPG.Entity;
using HexRPG.Managers;
using HexRPG.Sprite;
using HexRPG.Fonts;
using HexRPG.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using HexRPG.Overlay;

namespace HexRPG
{
    public class MainGame : Game
    {
        /// <summary>
        /// The active user-controlled player
        /// </summary>
        public static Player Player { get; set; }

        // Main variables
        /// <summary>
        /// Presentation of the graphics device
        /// </summary>
        public static GraphicsDeviceManager Graphics;

        /// <summary>
        /// The system window of <see cref="MainGame"/>
        /// </summary>
        public static GameWindow GameWindow;

        SpriteBatch SpriteBatch;

        // Art
        /// <summary>
        /// Array of all fonts used by the game
        /// </summary>
        public static SpriteFont[] FontSet;

        /// <summary>
        /// Main Instance of the game that instantiates and creates all primary properties, generators, and managers.
        /// </summary>
        public MainGame()
        {
            // Create main game variables
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Player = new Player("");
            GameWindow = Window;

            // Window properties
            IsMouseVisible = true;
            Graphics.HardwareModeSwitch = false;
            Graphics.PreferredBackBufferHeight = GameWindow.ClientBounds.Height;
            Graphics.PreferredBackBufferWidth = GameWindow.ClientBounds.Width;
            GameWindow.AllowUserResizing = true;
            Graphics.ApplyChanges();

            GameWindow.ClientSizeChanged += WindowResized;
        }

        /// <summary>
        /// Prepares static classes, runs on game launch
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            InputManager.Initialize();
            HardwareUtilities.Initialize();
            DebugOverlay.Initialize();
            CameraManager.Initialize(new EntityFocus(Player), GraphicsDevice.Viewport);
            WorldManager.Initialize();
        }

        /// <summary>
        /// Loads game content, runs on game content
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            TextureIndex.LoadContent(Content);
            FontSet = new SpriteFont[]
            {
                FontLibrary.Battlenet(GraphicsDevice)
            };
            
        }

        /// <summary>
        /// Called each tick, runs game logic
        /// </summary>
        /// <param name="gameTime">Holds the time state of the game</param>
        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Tertiary Updates
            

            // Secondary Updates
            Player.Update();
            EntityManager.Update();
            CameraManager.Update();

            // Main Updates
            InputManager.Update(IsActive, Exit);
            DebugOverlay.Update(gameTime);
            WorldManager.UpdateChunkRange(gameTime, WorldManager.GetAreaAroundFocus(CameraManager.CameraFocus));

            base.Update(gameTime);
        }

        /// <summary>
        /// Called each frame, runs rendering logic
        /// </summary>
        /// <param name="gameTime">Holds the time state of the game</param>
        protected override void Draw(GameTime gameTime)
        {
            // Draw Background
            GraphicsDevice.Clear(Globals.BackgroundColor);

            // Draw Tiles
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
            WorldManager.DrawChunkRange(gameTime, WorldManager.GetAreaAroundFocus(CameraManager.CameraFocus));
            SpriteBatch.End();

            // Draw Game Objects
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, CameraManager.Camera);
            Player.Draw(SpriteBatch);
            EntityManager.Draw(gameTime, SpriteBatch);
            SpriteBatch.End();

            // Draw Overlay
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
            DebugOverlay.Draw(SpriteBatch);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Called on <see cref="InputManager.ActionMapping"/> <see cref="InputManager.InputAction.Fullscreen"/>, toggles fullscreen and recalculates the positions of the debug overlay.
        /// </summary>
        public static void ToggleFullscreen()
        {
            Graphics.ToggleFullScreen();
            DebugOverlay.RecalculatePositions();
        }

        /// <summary>
        /// Called on <see cref="InputManager.ActionMapping"/> <see cref="InputManager.InputAction.ExitGame"/>, triggers exit logic
        /// </summary>
        public void ExitGame()
        {
            Exit();
        }

        /// <summary>
        /// Called on <see cref="InputManager.ActionMapping"/> <see cref="InputManager.InputAction.DebugToggle"/>, toggles visibility of the diagnostics overlay
        /// </summary>
        public static void DebugToggle()
        {
            DebugOverlay.IsDiagnosticsVisible = !DebugOverlay.IsDiagnosticsVisible;
        }

        /// <summary>
        /// Called on <see cref="InputManager.ActionMapping"/> <see cref="InputManager.InputAction.GridToggle"/>, toggles visibility of the tile grid overlay
        /// </summary>
        public static void GridToggle()
        {
            DebugOverlay.IsGridVisible = !DebugOverlay.IsGridVisible;
        }

        /// <summary>
        /// Called when window is resized, runs recalculation logic
        /// </summary>
        public void WindowResized(Object sender, EventArgs e)
        {
            DebugOverlay.RecalculatePositions();
        }
    }
}
