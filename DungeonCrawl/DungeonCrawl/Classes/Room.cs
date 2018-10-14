using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DungeonCrawl.Classes
{
    public class Room
    {
        public int room_Width { get; private set; }
        public int room_Height { get; private set; }
        public int x_Pos { get; private set; }
        public int y_Pos { get; private set; }

        public Room(int width, int height, int xpos, int ypos)
        {
            this.room_Width = width;
            this.room_Height = height;
            this.x_Pos = xpos;
            this.y_Pos = ypos;
        }



        public bool Intersects(Room rect)
        {
            return (rect.x_Pos < this.x_Pos + this.room_Width) &&
            (this.x_Pos < (rect.x_Pos + rect.room_Width)) &&
            (rect.y_Pos < this.y_Pos + this.room_Height) &&
            (this.y_Pos < rect.y_Pos + rect.room_Height);
        }

        public void Inflate(int width, int height)
        {
            this.x_Pos -= width;
            this.y_Pos -= height;
            this.room_Width += 2 * width;
            this.room_Height += 2 * height;
        }

        public Vector2 GetCenter()
        {
            return new Vector2(x_Pos + (room_Width / 2), y_Pos + (room_Height / 2));
        }


    }
}
