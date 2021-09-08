using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HexRPG.Dynamic
{
    /// <summary>
    /// Defines behavior for dynamic entities on the world map
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Z-Index of Entity
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// Effective location of the player character on the world, represented in X & Y on the hexagon grid.
        /// </summary>
        public Vector2 Coordinates { get; set; }

        /// <summary>
        /// Coordinate of the graphical representation of the entity, NOT the player's effective coordinates
        /// </summary>
        public Vector2 AnimCoordinate { get; set; }

        /// <summary>
        /// Whether selected <see cref="IEntity"/> is considered a standard entity, used for special cases
        /// </summary>
        public bool IsEntity { get; set; }

        /// <summary>
        /// Runs all related logic needed for an <see cref="IEntity"/> to draw itself
        /// </summary>
        /// <param name="spriteBatch">Current <see cref="SpriteBatch"/>, used for making an <see cref="IEntity"/> draw itself</param>
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
