using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{
    public class DungeonMap : IMap
    {
        private Tile[,] tiles;

        public int Width { get; set; }

        public int Height { get; set; }

        public void SetTiles(Tile[,] tiles)
        {
            this.tiles = tiles;
        }

        public IEnumerable<Tile> GetAllTiles()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    yield return tiles[x, y];
                }
            }
        }

        public Tile GetTileAt(int x, int y)
        {
            return tiles[x, y];
        }

        public Tile GetStartTile()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (tiles[x, y].Type == TileType.Start)
                    {
                        return tiles[x, y];
                    }
                }
            }

            return new Tile();
        }

        public Tile GetEndTile()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (tiles[x, y].Type == TileType.End)
                    {
                        return tiles[x, y];
                    }
                }
            }

            return new Tile();
        }


        public void SetTileAt(Tile t)
        {
            tiles[t.X, t.Y] = t;
        }


        public void Init(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            tiles = new Tile[Width, Height];
        }

        public override string ToString()
        {
            StringBuilder map = new StringBuilder();
            int lastX = 0;

            foreach (Tile t in tiles)
            {
                if (t.X != lastX)
                {
                    lastX = t.X;
                    map.Append(Environment.NewLine);
                }
                map.Append(t.Texture);
            }
            return map.ToString();
        }


        public Tile GetRandomWalkable()
        {
            while (true)
            {
                Random r = new Random();

                Tile temp = GetTileAt(r.Next(1, Width), r.Next(1, Height));

                if (temp.Type == TileType.Floor)
                {
                    return temp;
                }

            }
        }
    }
}
