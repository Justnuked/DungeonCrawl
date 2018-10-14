using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{

    public enum TileType
    {

        Floor = 1,
        Wall = 2,
    }

    public struct Tile
    {
        public TileType Type { get; set; }

        public int X { get; private set; }


        public int Y { get; private set; }

        public Color color { get; set; }

        public int TileSize
        {
            get { return 12; }
        }

        public char Texture { get; private set; }

        public Tile(int x, int y, TileType type, Color color)
            : this()
        {
            X = x;
            Y = y;
            Type = type;
            Texture = SetChar();
            this.color = color;
        }


        private char SetChar()
        {
            if (Type == TileType.Floor)
            {
                return '.';
            }

            if (Type == TileType.Wall)
            {
                return 'x';
            }

            return ';';
        }
    }
}
