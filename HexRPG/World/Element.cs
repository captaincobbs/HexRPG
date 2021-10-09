using System;
using System.Collections.Generic;
using System.Text;

namespace HexRPG.World
{
    public enum Element
    {
        Neutral = 0,

        // Natural
        Earth = 1,
        Plants = 2,
        Air = 3,
        Water = 4,
        Lightning = 5,
        Fire = 6,

        // Human
        Craft = 7,
        Shadow = 8,
        Beast = 9,
        Order = 10,
        Party = 11,
        War = 12,

        // Abhorred
        Avarice = 13,
        Neglect = 14,
        Death = 15,
        Frost = 16,
        Blood = 17,
        Manipulation = 18,
    }
}
