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
using HexRPG.Overlay;
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
        SpriteBatch SpriteBatch;
        MapManager MapManager;
        public static GameWindow GameWindow;

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
            HardwareUtilities.GraphicsDevice = Graphics;
            Content.RootDirectory = "Content";
            SaveType = GameOptions.SaveType;
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

        protected override void Initialize()
        {
            base.Initialize();
            InputManager.Initialize();
            DebugOverlay.Initialize();
            ViewPort.Initialize(new CameraFocus(new EntityFocus(Player)));
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            TextureIndex.LoadContent(Content);
            FontSet = new SpriteFont[]
            {
                FontLibrary.Battlenet(GraphicsDevice)
            };
            
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Tertiary Updates
            

            // Secondary Updates
            Player.Update();
            ViewPort.Update(GameWindow);

            // Main Updates
            InputManager.Update(IsActive, Exit);
            Overlay.DebugOverlay.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
            GraphicsDevice.Clear(GameOptions.BackgroundColor);
            Overlay.DebugOverlay.Draw(SpriteBatch);
            base.Draw(gameTime);
            SpriteBatch.End();
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, ViewPort.Camera);
            Player.Draw(SpriteBatch);
            SpriteBatch.End();
        }

        public static void ToggleFullscreen()
        {
            Graphics.ToggleFullScreen();
            DebugOverlay.RecalculatePositions();
        }

        public void ExitGame()
        {
            Exit();
        }

        public static void DebugToggle()
        {
            DebugOverlay.IsVisible = !DebugOverlay.IsVisible;
        }

        public void WindowResized(Object sender, EventArgs e)
        {
            DebugOverlay.RecalculatePositions();
        }
    }
}
