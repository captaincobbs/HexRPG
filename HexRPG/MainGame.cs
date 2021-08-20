using HexRPG.Dynamic;
using HexRPG.Entity;
using HexRPG.Generation;
using HexRPG.Managers;
using HexRPG.Sprite;
using HexRPG.UI;
using HexRPG.Fonts;
using HexRPG.Utilities;
using HexRPG.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static HexRPG.Utilities.FileUtilities;

namespace HexRPG
{
    public class MainGame : Game
    {
        /// <summary>
        /// Viewport for the user
        /// </summary>
        public ViewPort ViewPort { get; private set; }

        /// <summary>
        /// Determines the format in which user files will be exported as
        /// </summary>
        public SaveType SaveType { get; set; }

        Player player { get; set; }

        // Main variables
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MapManager mapViewer;

        // Art
        SpriteFont[] fonts;

        // Overlay
        private FrameCounter _frameCounter = new FrameCounter();

        /// <summary>
        /// Main Instance of the game that instantiates and creates all primary properties, generators, and managers.
        /// </summary>
        public MainGame()
        {
            // Create main game variables
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SaveType = SaveType.JSON;
            player = new Player("");
            ViewPort = new ViewPort(Window.ClientBounds.Height, Window.ClientBounds.Width, CameraFocus.Player,player);

            // Window properties
            IsMouseVisible = true;
            graphics.HardwareModeSwitch = false;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Window.AllowUserResizing = true;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
            InputManager.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureIndex.LoadContent(Content);
            fonts = new SpriteFont[]
            {
                FontLibrary.Battlenet(GraphicsDevice)
            };
            
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update();
            ViewPort.Update(Window);
            // Camera scrolling each frame - subtracting (GameOptions.TileSize/2) centers the screen


            // Main Updates
            Action toggleFullScreen = () => graphics.ToggleFullScreen();
            Action exit = () => Exit();
            InputManager.Update(IsActive, toggleFullScreen, exit);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCounter.Update(deltaTime);
            string fps = string.Format($"FPS     {_frameCounter.AverageFramesPerSecond}");
            string loc = string.Format($"COORD {player.Coordinate.X}, {player.Coordinate.Y}");
            string zom = string.Format($"ZOOM   {Math.Round(ViewPort.CamZoom, 1)} / {ViewPort.CamZoomDest}");
            string lui = string.Format($"INPUT  {InputManager.InputTypeLabel[(int)InputManager.LastUsedInput]}");
            spriteBatch.DrawString(fonts[0], fps, new Vector2(4, 1), Color.Green);
            spriteBatch.DrawString(fonts[0], loc, new Vector2(4, 24), Color.Green);
            spriteBatch.DrawString(fonts[0], zom, new Vector2(4, 48), Color.Green);
            spriteBatch.DrawString(fonts[0], lui, new Vector2(4, 72), Color.Green);
            GraphicsDevice.Clear(GameOptions.BackgroundColor);
            base.Draw(gameTime);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, ViewPort.Camera);
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
