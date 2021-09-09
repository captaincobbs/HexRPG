using HexRPG.Dynamic;
using HexRPG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static HexRPG.Entity.InputManager;

namespace HexRPG.Entity
{
    /// <summary>
    /// Class of the controllable player, moves around the map from user input
    /// </summary>
    public class Player : IEntity
    {
        /// <summary>
        /// Name of the player, inputted by user
        /// </summary>
        public string Name { get; set; } = "Player";

        /// <summary>
        /// Image used to represent the player
        /// </summary>
        public Rectangle Image { get; set; }

        /// <summary>
        /// Images used by the player, arranged in a 3x3 array. 1, 1 is the center, so X + 1 would be the right-facing sprite, and Y + 1 would be the up-facing sprite
        /// </summary>
        public static Rectangle DiagonalImage { get; set; }

        /// <summary>
        /// Z-Index of Entity
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// Effective location of the player character on the world, represented in X & Y on the hexagon grid.
        /// </summary>
        public Vector2 Coordinates { get; set; }

        /// <summary>
        /// Coordinate of the graphical representation of the player, NOT the player's effective coordinates
        /// </summary>
        public Vector2 AnimCoordinate { get; set; }

        /// <summary>
        /// Direction player sprite is facing
        /// </summary>
        public float Rotation { get; set; }

        // Threshold for movement rate
        private int frameCounter = 0;
        private int frameLimit = 8;

        // Player Direction
        /// <summary>
        /// Vertical movement states for the player
        /// </summary>
        public enum VerticalPlayerDirection { None, Up, Down }

        /// <summary>
        /// Horizontal movement states for the player
        /// </summary>
        public enum HorizontalPlayerDirection { None, Right, Left }

        /// <summary>
        /// Vertical movement state the player is planning on moving to
        /// </summary>
        public static VerticalPlayerDirection VerticalIndicatedDirection = VerticalPlayerDirection.None;

        /// <summary>
        /// Horizontal movement state the player is planning on moving to
        /// </summary>
        public static HorizontalPlayerDirection HorizontalIndicatedDirection = HorizontalPlayerDirection.None;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsEntity { get; set;  } = true;

        /// <summary>
        /// Constructor for a player instance
        /// </summary>
        /// <param name="Name">The players chosen name</param>
        public Player(string Name)
        {
            Coordinates = new Vector2(0, 0);
            AnimCoordinate = new Vector2(Globals.ChunkSize / 2, Globals.ChunkSize / 2);
            this.Name = Name;
            Depth = 1f;
            Image = SpriteAtlas.Player_Down;
        }

        /// <summary>
        /// Method to update player actions
        /// </summary>
        public void Update()
        {
            // Movement
            if (InputManager.IsActive)
            {
                HandleMovement();
            }

            // Update the animation coordinate to make character slide
            AnimCoordinate = new Vector2(
            (Coordinates.X - AnimCoordinate.X) * Globals.MovementInertiaFactor + AnimCoordinate.X,
            (Coordinates.Y - AnimCoordinate.Y) * Globals.MovementInertiaFactor + AnimCoordinate.Y
            );

            // Frame Counting
            if (frameCounter == frameLimit)
            {
                frameCounter = 0;
            }
            else
            {
                frameCounter++;
            }
        }

        /// <summary>
        /// Handles movement input
        /// </summary>
        private void HandleMovement()
        {
            // Keyboard input
            if (IsActionPressed(InputAction.Right) && !IsActionPressed(InputAction.Left))
            {
                if (IsActionTriggered(InputAction.Right))
                {
                    frameCounter = 0;
                }

                HorizontalIndicatedDirection = HorizontalPlayerDirection.Right;
            }

            if (IsActionPressed(InputAction.Left) && !IsActionPressed(InputAction.Right))
            {
                if (IsActionTriggered(InputAction.Left))
                {
                    frameCounter = 0;
                }

                HorizontalIndicatedDirection = HorizontalPlayerDirection.Left;
            }

            if (IsActionPressed(InputAction.Up) && !IsActionPressed(InputAction.Down))
            {
                if (IsActionTriggered(InputAction.Up))
                {
                    frameCounter = 0;
                }

                VerticalIndicatedDirection = VerticalPlayerDirection.Up;
            }

            if (IsActionPressed(InputAction.Down) && !IsActionPressed(InputAction.Up))
            {
                if (IsActionTriggered(InputAction.Down))
                {
                    frameCounter = 0;
                }

                VerticalIndicatedDirection = VerticalPlayerDirection.Down;
            }

            if (frameCounter == frameLimit)
            {
                Move();
            }
        }

        /// <summary>
        /// Uses movement input to move character
        /// </summary>
        private void Move()
        {
            // Convert indicated direction state to movement, Right = Positive X, Down = Positive Y
            int xMove = HorizontalIndicatedDirection == HorizontalPlayerDirection.Left
                ? -1 : HorizontalIndicatedDirection == HorizontalPlayerDirection.Right
                ? 1 : 0;

            int yMove = VerticalIndicatedDirection == VerticalPlayerDirection.Down
                ? 1 : VerticalIndicatedDirection == VerticalPlayerDirection.Up
                ? -1 : 0;

            // Change rotational sprite based off of player movement
            // Right
            if (xMove == 1 && yMove == 0)
            {
                Image = SpriteAtlas.Player_Right;
                Coordinates = new Vector2(Coordinates.X + 1, Coordinates.Y);
            }
            // Down Right
            else if (xMove == 1 && yMove == 1)
            {
                Image = SpriteAtlas.Player_BottomRight;
                Coordinates = new Vector2(Coordinates.X + 1, Coordinates.Y);
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y + 1);

            }
            // Down
            else if (xMove == 0 && yMove == 1)
            {
                Image = SpriteAtlas.Player_Down;
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y + 1);
            }
            // Down Left
            if (xMove == -1 && yMove == 1)
            {
                Image = SpriteAtlas.Player_BottomLeft;
                Coordinates = new Vector2(Coordinates.X - 1, Coordinates.Y);
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y + 1);
            }
            // Left
            else if (xMove == -1 && yMove == 0)
            {
                Image = SpriteAtlas.Player_Left;
                Coordinates = new Vector2(Coordinates.X - 1, Coordinates.Y);
            }
            // Up Left
            else if (xMove == -1 && yMove == -1)
            {
                Image = SpriteAtlas.Player_TopLeft;
                Coordinates = new Vector2(Coordinates.X - 1, Coordinates.Y);
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y - 1);
            }
            // Up
            else if (xMove == 0 && yMove == -1)
            {
                Image = SpriteAtlas.Player_Up;
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y - 1);
            }
            // Up Right
            else if (xMove == 1 && yMove == -1)
            {
                Image = SpriteAtlas.Player_TopRight;
                Coordinates = new Vector2(Coordinates.X + 1, Coordinates.Y);
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y - 1);
            }

            // Reset movement direction
            VerticalIndicatedDirection = VerticalPlayerDirection.None;
            HorizontalIndicatedDirection = HorizontalPlayerDirection.None;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="spriteBatch"><inheritdoc/></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destRect = new Rectangle(
               (int)(AnimCoordinate.X * Globals.TileSize),
               (int)(AnimCoordinate.Y * Globals.TileSize),
               Globals.TileSize,
               Globals.TileSize);

            spriteBatch.Draw(TextureIndex.SpriteAtlas, destRect, Image, Color.White, Rotation, new Vector2(Globals.TileSize / 2, Globals.TileSize / 2), SpriteEffects.None, Depth);
        }

    }
}