using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{
    public class Player : Actor
    {
        public Player(int X, int Y, ref IMap map)
        {
            this.X = X;
            this.Y = Y;
            this.sprite = '@';
            this.color = Color.White;
            this.spriteSize = Game1.TILEMULTIPLIER;
            this.map = map;

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, sprite.ToString(), new Vector2(X * this.spriteSize, Y * this.spriteSize), color);
        }

        public Tile GetPlayerTile(IMap map)
        {
            return map.GetTileAt(X, Y);
        }

        public override void Update(InputState inputState)
        {
            if (inputState.IsLeft(PlayerIndex.One))
            {
                if(map.GetTileAt(X - 1 , Y).IsWalkAble())
                    X--;
            }
            else if (inputState.IsRight(PlayerIndex.One))
            {
                if (map.GetTileAt(X + 1, Y).IsWalkAble())
                    X++;
            }
            else if (inputState.IsUp(PlayerIndex.One))
            {
                if (map.GetTileAt(X, Y - 1).IsWalkAble())
                    Y--;
            }
            else if (inputState.IsDown(PlayerIndex.One))
            {
                if (map.GetTileAt(X, Y + 1).IsWalkAble())
                    Y++;
            }
            else if (inputState.IsDiagonalLeftUp(PlayerIndex.One))
            {
                if (map.GetTileAt(X - 1, Y - 1).IsWalkAble())
                {
                    Y--;
                    X--;
                }
                   
            }
            else if (inputState.IsDiagonalLeftDown(PlayerIndex.One))
            {
                if (map.GetTileAt(X - 1, Y + 1).IsWalkAble())
                {
                    Y++;
                    X--;
                }

            }
            else if (inputState.IsDiagonalRightUp(PlayerIndex.One))
            {
                if (map.GetTileAt(X + 1, Y - 1).IsWalkAble())
                {
                    Y--;
                    X++;
                }

            }
            else if (inputState.IsDiagonalRightDown(PlayerIndex.One))
            {
                if (map.GetTileAt(X + 1, Y + 1).IsWalkAble())
                {
                    Y++;
                    X++;
                }

            }
        }
    }
}
