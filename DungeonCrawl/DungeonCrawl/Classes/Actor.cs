using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes
{
    public abstract class Actor
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int health { get; set; }
        public char sprite { get; set; }
        public int spriteSize { get; set; }
        public Color color { get; set; }
        public IMap map { get; set; }

        public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);
        public abstract void Update(InputState inputState);
    }
}
