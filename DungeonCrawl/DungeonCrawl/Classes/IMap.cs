using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{
    public interface IMap
    {
        int Width { get; set; }
        int Height { get; set; }

        IEnumerable<Tile> GetAllTiles();

        Tile GetTileAt(int x, int y);
        Tile GetStartTile();
        Tile GetEndTile();

        void SetTiles(Tile[,] tiles);
        void SetTileAt(Tile t);
        void Init(int Width, int Height);
        Tile GetRandomWalkable();
    }
}
