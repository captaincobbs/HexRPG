using HexRPG.Dynamic;
using HexRPG.Entity;
using HexRPG.Generation;
using HexRPG.Managers;
using HexRPG.Sprite;
using HexRPG.Fonts;
using HexRPG.Utilities;
using HexRPG.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static HexRPG.Utilities.FileUtilities;
using HexRPG.Debug;
using static HexRPG.Debug.DebugOverlay;
using System.Collections.Generic;

namespace HexRPG
{
    public class MainGame : Game
    {
        /// <summary>
        /// Determines the format in which user files will be exported as
        /// </summary>
        public SaveType SaveType { get; set; }

        public static Player Player { get; set; }

        // Main variables
        public static GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;
        MapManager mapManager;

        // Art
        public static SpriteFont[] FontSet;

        // Debug


        /// <summary>
        /// Main Instance of the game that instantiates and creates all primary properties, generators, and managers.
        /// </summary>
        public MainGame()
        {
            // Create main game variables
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SaveType = GameOptions.SaveType;
            Player = new Player("");

            // Window properties
            IsMouseVisible = true;
            Graphics.HardwareModeSwitch = false;
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Window.AllowUserResizing = true;
            Graphics.ApplyChanges();

            // Debugging
            DebugOverlay.OverlayContents = new List<DebugOverlayItemType>()
            {
            DebugOverlayItemType.FPS,
            DebugOverlayItemType.Zoom,
            DebugOverlayItemType.Coordinates,
            DebugOverlayItemType.LastInputDevice,
            DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,
DebugOverlayItemType.FPS,

            };
        }

        protected override void Initialize()
        {
            base.Initialize();
            InputManager.Initialize();
            DebugOverlay.Initialize();
            ViewPort.Initialize(Window.ClientBounds.Height, Window.ClientBounds.Width, CameraFocus.Player, Player);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureIndex.LoadContent(Content);
            FontSet = new SpriteFont[]
            {
                FontLibrary.Battlenet(GraphicsDevice)
            };
            
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Secondary Updates
            Player.Update();
            ViewPort.Update(Window);


            // Main Updates
            InputManager.Update(IsActive, Exit);
            DebugOverlay.Update(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
            GraphicsDevice.Clear(GameOptions.BackgroundColor);
            DebugOverlay.Draw(spriteBatch);
            base.Draw(gameTime);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, ViewPort.Camera);
            Player.Draw(spriteBatch);
            spriteBatch.End();
        }

        public static void ToggleFullscreen()
        {
            Graphics.ToggleFullScreen();
            ViewPort.WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ViewPort.WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            UpdateForNewScreenSize();
        }

        public void ExitGame()
        {
            Exit();
        }

        public static void DebugToggle()
        {
            IsVisible = !IsVisible;
        }

        public void WindowResized()
        {
            ViewPort.WindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ViewPort.WindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            UpdateForNewScreenSize();
        }
    }
}
