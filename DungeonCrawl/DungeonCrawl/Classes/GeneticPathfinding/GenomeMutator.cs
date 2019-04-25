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
            //first find all the genomes that got to the exit
            List<GenomeWalker> candidatesForEvolution = new List<GenomeWalker>();

            foreach (GenomeWalker walker in genomes)
            {
                if(walker.GetScore() != int.MaxValue)
                {
                    candidatesForEvolution.Add(walker);
                }
            }

            for (int i = 0; i < candidatesForEvolution.Count; i+=2)
            {
                Genome first = candidatesForEvolution[i].GetGenome();
                Genome second = candidatesForEvolution[i + 1].GetGenome();
            }

            this.genomes = candidatesForEvolution;
        }

        private void Mutate(Genome one, Genome two)
        {
            string oneFirstHalf = one.GetGenome().Substring(0, (one.GetGenome().Length / 2));
        }
    }
}
