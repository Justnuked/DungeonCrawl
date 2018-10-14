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
        public Player(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            this.sprite = '@';
            this.color = Color.White;
            this.spriteSize = 12;

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
                X--;
            }
            else if (inputState.IsRight(PlayerIndex.One))
            {
                X++;
            }
            else if (inputState.IsUp(PlayerIndex.One))
            {
                Y--;
            }
            else if (inputState.IsDown(PlayerIndex.One))
            {
                Y++;
            }
        }
    }
}
