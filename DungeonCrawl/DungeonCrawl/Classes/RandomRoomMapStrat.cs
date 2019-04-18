using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{
    class RandomRoomMapStrat<T> : IMapGenStrat<T> where T : IMap, new()
    {
        private readonly int _Width;
        private readonly int _Height;
        private readonly int _Rooms;
        private readonly int _RoomMinSize;
        private readonly int _RoomMaxSize;
        private readonly int _MaxRoomTries;

        public RandomRoomMapStrat(int width, int height, int rooms, int roomMinSize, int roomMaxsize, int maxRoomTries)
        {
            this._Width = width;
            this._Height = height;
            this._MaxRoomTries = maxRoomTries;
            this._RoomMaxSize = roomMaxsize;
            this._RoomMinSize = roomMinSize;
            this._Rooms = rooms;
        }

        public T CreateMap()
        {
            var map = new T();
            map.Init(_Width, _Height);

            //fill entire map with walls. . .

            for (int x = 0; x < _Width; x++)
            {
                for (int y = 0; y < _Height; y++)
                {
                    map.SetTileAt(new Tile(x, y, TileType.Wall));
                }
            }

            List<Room> roomList = new List<Room>();

            Random rand = new Random();
            bool intersects = false;


            for (int i = 0; i < _Rooms; i++)
            {
                intersects = false;
                Room temp = new Room(rand.Next(_RoomMinSize, _RoomMaxSize),
                                    rand.Next(_RoomMinSize, _RoomMaxSize),
                                    rand.Next(1, _Width - _RoomMaxSize),
                                    rand.Next(1, _Height - _RoomMaxSize));

                temp.Inflate(1, 1);

                foreach (Room r in roomList)
                {
                    if (temp.Intersects(r))
                    {
                        intersects = true;
                    }
                }
                temp.Inflate(-1, -1);

                if (!intersects)
                    roomList.Add(temp);
            }

            foreach (Room r in roomList)
            {
                for (int x = r.x_Pos; x < r.x_Pos + r.room_Width; x++)
                {
                    for (int y = r.y_Pos; y < r.y_Pos + r.room_Height; y++)
                    {

                        map.SetTileAt(new Tile(x, y, TileType.Floor));
                    }
                }
            }

            for (int r = 0; r < roomList.Count; r++)
            {
                if (r == 0)
                {
                    continue;
                }

                int previousRoomCenterX = (int)roomList[r - 1].GetCenter().X;
                int previousRoomCenterY = (int)roomList[r - 1].GetCenter().Y;
                int currentRoomCenterX = (int)roomList[r].GetCenter().X;
                int currentRoomCenterY = (int)roomList[r].GetCenter().Y;

                if (rand.Next(0, 2) == 0)
                {
                    MakeHorizontalTunnel(map, previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    MakeVerticalTunnel(map, previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                }
                else
                {
                    MakeVerticalTunnel(map, previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    MakeHorizontalTunnel(map, previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }

            SetStartEndEndTile(map);

            return map;

        }

        private void MakeHorizontalTunnel(T map, int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                map.SetTileAt(new Tile(x, yPosition, TileType.Floor));
            }
        }

        private void MakeVerticalTunnel(T map, int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                map.SetTileAt(new Tile(xPosition, y, TileType.Floor));
            }
        }

        private void SetStartEndEndTile(T map)
        {
            map.SetTileAt(new Tile(map.GetRandomWalkable().X, map.GetRandomWalkable().Y, TileType.Start));
            map.SetTileAt(new Tile(map.GetRandomWalkable().X, map.GetRandomWalkable().Y, TileType.End));
        }
    }
}
