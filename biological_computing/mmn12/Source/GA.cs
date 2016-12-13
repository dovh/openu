using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace ApplicationSpace
{
    class GA
    {
        /********************************************************
         *              Static Members 
         ********************************************************/
        static Random m_random;

        /********************************************************
         *              Members 
         ********************************************************/
        
        List<Chromosome> m_chromosomes;
        int m_sumof_fitness;
        int m_max_fitness;
        int m_min_fitness;
        double m_avg_fitness;

        int m_sumof_delta_to_max_fitness;

        // properties 
        int m_population;
        int m_elitism;
        double m_Pc; // Crossover probability 
        double m_Pm; // Mutation probability 

        Dictionary<int, int> dbg_histogram = new Dictionary<int, int>();

        /********************************************************
         *              Accessors 
         ********************************************************/
        public static double NextRandom { get { return m_random.NextDouble(); } }
        public double Pc { get { return m_Pc; } set { m_Pc = value; } }
        public double Pm { get { return m_Pm; } set { m_Pm = value; } }

        public double Max_fitness { get { return m_max_fitness; } }
        public double Min_fitness { get { return m_min_fitness; } }
        public double Avg_fitness { get { return m_avg_fitness; } }

        /********************************************************
         *              Methods 
         ********************************************************/
        public GA()
        {
            m_random = new Random();
            m_population = 100;

            Chromosome.StaticInitialize();
            Initialize();
        }

        void update()
        {
            // update statistics 
            m_max_fitness = m_chromosomes.Max(chrom => chrom.Fitness);
            m_min_fitness = m_chromosomes.Min(chrom => chrom.Fitness);
            m_sumof_fitness = m_chromosomes.Sum(chrom => chrom.Fitness);
            m_avg_fitness = m_sumof_fitness / m_chromosomes.Count;

            m_sumof_delta_to_max_fitness = m_chromosomes.Sum(chrom => m_max_fitness - chrom.Fitness);
        }

        public void Initialize()
        {
            m_chromosomes = new List<Chromosome>();
            for (int i = 0; i < m_population; i++)
            {
                Chromosome chromosome = new Chromosome(i);
                chromosome.Randomize();
                chromosome.UpdateFitness();
                m_chromosomes.Add(chromosome);
            }

            update();
        }

        Chromosome Select()
        {

            List<Chromosome>.Enumerator iter = m_chromosomes.GetEnumerator();

            //double total = m_sumof_delta_to_max_fitness; 
            double total = m_sumof_fitness; 
            double wheel = m_random.NextDouble() * total;

            double accumulate = 0;

            //int index = 0;
            bool valid_iter = iter.MoveNext();
            while (accumulate < wheel && valid_iter)
            {
                //accumulate += iter.Current.Fitness;
                //Debug.Assert(m_max_fitness - iter.Current.Fitness >= 0);
                //if (iter.Current.Fitness == m_max_fitness)
                //    Console.WriteLine("max: {0}", index);
                //if (iter.Current.Fitness == m_min_fitness)
                //    Console.WriteLine("min: {0}", index);

                //accumulate += (m_max_fitness - iter.Current.Fitness);
                accumulate += iter.Current.Fitness;


                valid_iter = iter.MoveNext();
                //index++;
            }

            Chromosome retval;
            if (valid_iter)
            {
                //Console.WriteLine("selected: {0} out of {1}", m_max_fitness - iter.Current.Fitness, m_max_fitness - m_min_fitness);
                retval = iter.Current;
            }
            else
            {
                Debug.Assert(accumulate == total);
                retval = m_chromosomes.Last();
            }

            //int key = retval.Fitness - m_min_fitness;
            //if (dbg_histogram.ContainsKey(key))
            //    dbg_histogram[key]++;
            //else
            //    dbg_histogram.Add(key, 1);

            return retval;
        }

        public void Create_Generation()
        {
            List<Chromosome> next_chromosomes = new List<Chromosome>();

            // update current 
            update();

            while(next_chromosomes.Count < m_population)
            {
                // Selection
                Chromosome first = Select();
                Chromosome second = Select();
                Chromosome offspring;

                // Crossover
                if (m_random.NextDouble() < m_Pc)
                {
                    offspring = new Chromosome(next_chromosomes.Count + 1);
                    offspring.Crossover(first, second);
                }
                else
                {
                    //if (0.5 < m_random.NextDouble())
                    if (first.Fitness > second.Fitness)
                        offspring = first;
                    else
                        offspring = second;
                    offspring.Index = next_chromosomes.Count + 1;
                }

                // Mutation 
                if (m_random.NextDouble() < m_Pm)
                {
                    //offspring.Mutate();
                }

                offspring.UpdateFitness();
                next_chromosomes.Add(offspring);
            }

            //dbg_histogram = dbg_histogram.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // step to next generation 
            m_chromosomes = next_chromosomes;
            update();
        }

        void Sanity()
        {
            foreach (Chromosome chromosome in m_chromosomes)
            {
                chromosome.Sanity();
            }

        }
    }
}
