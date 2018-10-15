using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
            get { return Game1.TILEMULTIPLIER; }
        }

        public char Texture { get; private set; }

        public Tile(int x, int y, TileType type)
            : this()
        {
            X = x;
            Y = y;
            Type = type;
            Texture = SetChar();
            this.color = SetColor();
        }

        public bool IsWalkAble()
        {
            if (this.Type != TileType.Floor)
            {
                return false;
            }

            return true;
        }

        private Color SetColor()
        {
            {
                if (Type == TileType.Floor)
                {
                    return Color.White;
                }

                if (Type == TileType.Wall)
                {
                    return Color.Red;
                }

                return Color.AliceBlue;
            }
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
