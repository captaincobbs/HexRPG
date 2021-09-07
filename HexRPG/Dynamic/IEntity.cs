using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexRPG.Dynamic
{
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
    }
}
