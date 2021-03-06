﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes.GeneticPathfinding
{
    //this class will take a genome and walk its genes
    public class GenomeWalker
    {
        private int X;
        private int Y;
        private Color color;
        private int spriteSize;
        private IMap map;
        private Genome genome;
        private int score;
        private int genomeNumber;
        private int posInList;

        public GenomeWalker(int X, int Y, IMap map, Genome genome, int genomeNumber)
        {
            this.X = X;
            this.Y = Y;
            this.color = Color.White;
            this.spriteSize = Game1.TILEMULTIPLIER;
            this.map = map;
            this.genome = genome;
            this.genomeNumber = genomeNumber;
            this.posInList = 0;

        }

        public void Draw(ContentManager content, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.sprites["player"], new Vector2(X * this.spriteSize, Y * this.spriteSize), color);
        }

        public Tile GetPlayerTile(IMap map)
        {
            return map.GetTileAt(X, Y);
        }

        public void ResetPosition(IMap map)
        {
            this.X = map.GetStartTile().X;
            this.Y = map.GetStartTile().Y;
        }

        public void VictoryWalk()
        {
            List<string> instructions = new List<string>();

            for (int i = 0; i < genome.GetGenome().Length; i += 2)
            {
                instructions.Add(genome.GetGenome().Substring(i, 2));
            }

            switch (instructions[posInList])
            {
                case "00":
                    if (map.GetTileAt(X, Y - 1).IsWalkAble())
                        Y--;
                    break;
                case "01":
                    if (map.GetTileAt(X + 1, Y).IsWalkAble())
                        X++;
                    break;
                case "10":
                    if (map.GetTileAt(X, Y + 1).IsWalkAble())
                        Y++;
                    break;
                case "11":
                    if (map.GetTileAt(X - 1, Y).IsWalkAble())
                        X--;
                    break;
            }

            if (posInList == instructions.Count - 1)
            {
                Game1.state = GAMESTATE.PAUSED;
            }

            if (posInList < instructions.Count - 1)
            {
                posInList++;
            }
        }

        private void Walk()
        {
            score = 0;
            List<string> instructions = new List<string>();

            List<Tile> walkedTiles = new List<Tile>();

            for (int i = 0; i < genome.GetGenome().Length; i += 2)
            {
                instructions.Add(genome.GetGenome().Substring(i, 2));
            }


            foreach (string s in instructions)
            {
                walkedTiles.Add(map.GetTileAt(X, Y));
                switch (s)
                {
                    case "00":
                        if (map.GetTileAt(X, Y - 1).IsWalkAble())
                            Y--;
                        else
                            score += 2;
                        break;
                    case "01":
                        if (map.GetTileAt(X + 1, Y).IsWalkAble())
                            X++;
                        else
                            score += 2;
                        break;
                    case "10":
                        if (map.GetTileAt(X, Y + 1).IsWalkAble())
                            Y++;
                        else
                            score += 2;
                        break;
                    case "11":
                        if (map.GetTileAt(X - 1, Y).IsWalkAble())
                            X--;
                        else
                            score += 2;
                        break;

                }
            }

            walkedTiles.Add(map.GetTileAt(X, Y));

            this.score += CalculateScore(walkedTiles);
            this.score += walkedTiles.Count;

            if (score < 0)
            {
                score = int.MaxValue;
            }
        }

        private int CalculateScore(List<Tile> walkedTiles)
        {
            Tile endTile = map.GetEndTile();

            //end not found bad route set max score
            if(!walkedTiles.Contains(endTile))
            {
                return int.MaxValue;
            }

            //Steps after the end is found are irrelevant, so cut them out of the genome
            int index = walkedTiles.FindIndex(tile => tile.Type == TileType.End);

            this.genome = new Genome(genome.GetGenome().Substring(0, index * 2));


            //check for duplicate tiles in the walked tiles -- backtracking is never good
            var query = walkedTiles.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();

            foreach (var element in query)
            {
                for (int i = 0; i < element.Counter; i++)
                {
                    score += i * element.Counter;
                }
            }

            return score;
        }

        public void Update(InputState inputState)
        {
            Walk();
        }

        public int GetScore()
        {
            return this.score;
        }

        public Genome GetGenome()
        {
            return this.genome;
        }

        public void SetGenome(Genome genome)
        {
            this.genome = genome;
        }

        public int GetGenomeNumber()
        {
            return this.genomeNumber;
        }
    }
}
