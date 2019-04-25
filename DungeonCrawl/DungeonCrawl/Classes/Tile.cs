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
        Start = 3,
        End = 4
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

        public Texture2D texture2d {get;set;}

        public char Texture { get; private set; }

        public Tile(int x, int y, TileType type)
            : this()
        {
            X = x;
            Y = y;
            Type = type;
            Init();
        }

        private void Init()
        {
            if (Type == TileType.Floor || Type ==  TileType.Start || Type == TileType.End)
            {
                this.texture2d = Game1.sprites["dirt0"];
                this.Texture = '.';
                this.color = Color.White;
            }

            if (Type == TileType.Wall)
            {
                this.texture2d = Game1.sprites["brick_gray0"];
                this.Texture = 'X';
                this.color = Color.Red;
            }
        }

        public bool IsWalkAble()
        {
            if (this.Type == TileType.Wall)
            {
                return false;
            }

            return true;
        }
    }
}
