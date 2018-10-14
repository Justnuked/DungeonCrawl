using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{
    public class SquareMapStrat<T> : IMapGenStrat<T> where T : IMap, new()
    {
        private readonly int _Width;
        private readonly int _Height;

        public SquareMapStrat(int width, int height)
        {
            this._Width = width;
            this._Height = height;
        }

        public T CreateMap()
        {
            var map = new T();
            map.Init(_Width, _Height);

            for (int x = 0; x < _Width; x++)
            {
                for (int y = 0; y < _Height; y++)
                {
                    if (x == 0 || x == _Width - 1)
                    {
                        map.SetTileAt(new Tile(x, y, TileType.Wall, Color.Red));
                    }
                    else if (y == 0 || y == _Height - 1)
                    {
                        map.SetTileAt(new Tile(x, y, TileType.Wall, Color.Red));
                    }
                    else
                    {
                        map.SetTileAt(new Tile(x, y, TileType.Floor, Color.White));
                    }
                }
            }

            return map;
        }
    }
}
