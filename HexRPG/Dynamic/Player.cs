using HexRPG.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using static HexRPG.Entity.InputManager;

namespace HexRPG.Entity
{
    /// <summary>
    /// Class of the controllable player, moves around the map from user input
    /// </summary>
    public class Player
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
        public Vector2 Coordinate { get; set; }

        /// <summary>
        /// Coordinate of the graphical representation of the player, NOT the player's effective coordinates
        /// </summary>
        private Vector2 AnimCoordinate { get; set; }

        /// <summary>
        /// Direction player sprite is facing
        /// </summary>
        public float Rotation { get; set; }
        private int frameCounter = 0;
        private int frameLimit = 5;

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

        /// <inheritdoc/>
        public bool IsEntity { get; } = true;

        /// <summary>
        /// Constructor for a player instance
        /// </summary>
        /// <param name="Name">The players chosen name</param>
        public Player(string Name)
        {
            Coordinate = new Vector2(0, 0);
            AnimCoordinate = new Vector2(GameOptions.ChunkSize / 2, GameOptions.ChunkSize / 2);
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
            (Coordinate.X - AnimCoordinate.X) * GameOptions.MovementInertiaFactor + AnimCoordinate.X,
            (Coordinate.Y - AnimCoordinate.Y) * GameOptions.MovementInertiaFactor + AnimCoordinate.Y
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
                Coordinate = new Vector2(Coordinate.X + 1, Coordinate.Y);
            }
            // Down Right
            else if (xMove == 1 && yMove == 1)
            {
                Image = SpriteAtlas.Player_BottomRight;
                Coordinate = new Vector2(Coordinate.X + 1, Coordinate.Y);
                Coordinate = new Vector2(Coordinate.X, Coordinate.Y + 1);

            }
            // Down
            else if (xMove == 0 && yMove == 1)
            {
                Image = SpriteAtlas.Player_Down;
                Coordinate = new Vector2(Coordinate.X, Coordinate.Y + 1);
            }
            // Down Left
            if (xMove == -1 && yMove == 1)
            {
                Image = SpriteAtlas.Player_BottomLeft;
                Coordinate = new Vector2(Coordinate.X - 1, Coordinate.Y);
                Coordinate = new Vector2(Coordinate.X, Coordinate.Y + 1);
            }
            // Left
            else if (xMove == -1 && yMove == 0)
            {
                Image = SpriteAtlas.Player_Left;
                Coordinate = new Vector2(Coordinate.X - 1, Coordinate.Y);
            }
            // Up Left
            else if (xMove == -1 && yMove == -1)
            {
                Image = SpriteAtlas.Player_TopLeft;
                Coordinate = new Vector2(Coordinate.X - 1, Coordinate.Y);
                Coordinate = new Vector2(Coordinate.X, Coordinate.Y - 1);
            }
            // Up
            else if (xMove == 0 && yMove == -1)
            {
                Image = SpriteAtlas.Player_Up;
                Coordinate = new Vector2(Coordinate.X, Coordinate.Y - 1);
            }
            // Up Right
            else if (xMove == 1 && yMove == -1)
            {
                Image = SpriteAtlas.Player_TopRight;
                Coordinate = new Vector2(Coordinate.X + 1, Coordinate.Y);
                Coordinate = new Vector2(Coordinate.X, Coordinate.Y - 1);
            }

            // Reset movement direction
            VerticalIndicatedDirection = VerticalPlayerDirection.None;
            HorizontalIndicatedDirection = HorizontalPlayerDirection.None;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destRect = new Rectangle(
               (int)(AnimCoordinate.X * GameOptions.TileSize) - (GameOptions.TileSize / 2),
               (int)(AnimCoordinate.Y * GameOptions.TileSize) - (GameOptions.TileSize / 2),
               GameOptions.TileSize,
               GameOptions.TileSize);

            spriteBatch.Draw(TextureIndex.SpriteAtlas, destRect, Image, Color.White, Rotation, new Vector2(GameOptions.TileSize / 2, GameOptions.TileSize / 2), SpriteEffects.None, Depth);
        }

    }
}