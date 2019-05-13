using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes.GeneticPathfinding
{
    public class Genome
    {
        private String genes;

        public Genome(int length)
        {
            CreateGenome(length);
        }

        public Genome(String genome)
        {
            genes = genome;
        }

        private void CreateGenome(int length)
        {
            //create a genome with random instructions 00 = up 01 = right 10 = down 11 = left

            for (int i = 0; i < length; i++)
            {
                switch (Game1.r.Next(1, 5))
                {
                    case 1:
                            genes += "00";
                            break;
                    case 2:
                            genes += "01";
                            break;
                    case 3:
                            genes += "10";
                            break;
                    case 4:
                            genes += "11";
                            break;
                    default:
                        genes += "oops";
                        break;
                }
            }
        }

        public string GetGenome()
        {
            return this.genes;
        }
    }
}
