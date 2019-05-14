using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Classes.GeneticPathfinding
{
    class GenomeMutator
    {
        List<GenomeWalker> genomes;
        Genome prevBest = null;
        Genome prevSecBest = null;
        int best = int.MaxValue;

        public GenomeMutator()
        {
            genomes = new List<GenomeWalker>();
        }

        public void AddGenomeWalker(GenomeWalker walker)
        {
            genomes.Add(walker);
        }

        public List<GenomeWalker> GetGenomeWalkers()
        {
            return genomes;
        }

        public void RemoveAllMutators()
        {
            genomes = new List<GenomeWalker>();
        }

        public List<int> GetScores()
        {
            List<int> scores = new List<int>();

            foreach (GenomeWalker w in genomes)
            {
                scores.Add(w.GetScore());
            }

            return scores;
        }


        public void Evolve()
        {
            Game1.ITERATIONS++;
            int bestScore = genomes.Min(e => e.GetScore());
            best = bestScore;

            if (bestScore != int.MaxValue)
            {
                Genome bestGenome = genomes.Where(g => g.GetScore() == bestScore).First().GetGenome();
                Genome secondBestGenome = genomes.Where(g => g.GetScore() > bestScore).OrderBy(g => g.GetScore()).First().GetGenome();

                prevBest = bestGenome;
                prevSecBest = secondBestGenome;

                Mutator(bestGenome, secondBestGenome);
 
            }
            else
            {
                if (prevBest != null && prevSecBest != null)
                {
                    Mutator(prevBest, prevSecBest);
                }
                else
                {
                    Mutator(new Genome(5000), new Genome(5000));
                }
            }
        }

        public GenomeWalker GetBestWalker()
        {
            GenomeWalker temp = null;
            int score = int.MaxValue;

            foreach (GenomeWalker walker in genomes)
            {
                if (walker.GetScore() < score)
                {
                    temp = walker;
                }
            }

            return temp;
        }

        public void Mutator(Genome best, Genome secondBest)
        {
            foreach(GenomeWalker walker in genomes)
            {
                string newGenome = string.Empty;

                for (int i = 0; i < best.GetGenome().Length; i += 2)
                {
                    if (Game1.r.Next(0, 1000) % 2 == 0)
                    {
                        newGenome += best.GetGenome().Substring(i, 2); 
                    }
                    else
                    {
                        if (i + 2 < secondBest.GetGenome().Length)
                        {
                            newGenome += secondBest.GetGenome().Substring(i, 2);
                        }
                    }
                }

                walker.SetGenome(new Genome(newGenome));
            }
        }

        public int GetBestScore()
        {
            return this.best;
        }
    }
}
