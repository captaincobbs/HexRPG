using HexRPG.Dynamic;
using HexRPG.Entity;
using HexRPG.Generation;
using HexRPG.Managers;
using HexRPG.Sprite;
using HexRPG.Utilities;
using HexRPG.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureIndex.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update();
            ViewPort.Update(Window);
            // Camera scrolling each frame - subtracting (GameOptions.TileSize/2) centers the screen


            // Main Updates
            InputManager.Update(IsActive, this);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, ViewPort.Camera);
            player.Draw(spriteBatch);
            GraphicsDevice.Clear(GameOptions.BackgroundColor);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
