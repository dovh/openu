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
        int m_max_fitness;
        int m_min_fitness;
        double m_avg_fitness;

        int m_steady_state_prev;
        int m_steady_state_counter;
        int m_steady_state_threshold; 

        // properties 
        int m_population;
        double m_Pc; // Crossover probability 
        double m_Pm; // Mutation probability 

        SortedDictionary<int, int> m_dbg_histogram = new SortedDictionary<int, int>();

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
            m_random = new Random(123);
            m_population = 100;

            m_steady_state_counter = 0;
            m_steady_state_threshold = 100;

            Chromosome.StaticInitialize();
            Initialize();
        }

        void update()
        {
            // update statistics 
            m_max_fitness = m_chromosomes.Max(chrom => chrom.Fitness);
            m_min_fitness = m_chromosomes.Min(chrom => chrom.Fitness);
            m_avg_fitness = m_chromosomes.Sum(chrom => chrom.Fitness) / m_chromosomes.Count;
        }

        public void Initialize()
        {
            m_chromosomes = new List<Chromosome>();
            for (int i = 0; i < m_population; i++)
            {
                Chromosome chromosome = new Chromosome(/*i*/);
                chromosome.Randomize();
                chromosome.ComputeFitness();
                m_chromosomes.Add(chromosome);
            }

            update();
        }

        Chromosome Select()
        {
            int max_fitness = m_chromosomes.Max(chrom => chrom.Fitness);
            int min_fitness = m_chromosomes.Min(chrom => chrom.Fitness);
            Debug.Assert(max_fitness == m_max_fitness);
            Debug.Assert(min_fitness == m_min_fitness);

            if (m_max_fitness == m_min_fitness)
            {
                return m_chromosomes.First();
            }

            // sort descending order 
            //m_chromosomes.Sort(delegate(Chromosome x, Chromosome y) { return y.Fitness.CompareTo(x.Fitness); });

            // While trying to minimaze fiteness, filter only the X% lower chromosones 
            //  X is the 'selection threshold' 
            double selection_threshold = 40.0 / 100.0;
            double fitness_threshold = (m_max_fitness - m_min_fitness)* selection_threshold + m_min_fitness;

            List<Chromosome> filtered_chromosomes = m_chromosomes.ToList();
            filtered_chromosomes.RemoveAll(chrom => chrom.Fitness >= fitness_threshold);
            Debug.Assert(filtered_chromosomes.Count > 0);

            double total = filtered_chromosomes.Sum(chrom => chrom.Fitness);
            double wheel = m_random.NextDouble() * total;
            double accumulate = 0;

            List<Chromosome>.Enumerator iter = filtered_chromosomes.GetEnumerator();
            bool valid_iter = iter.MoveNext();
            while (accumulate < wheel && valid_iter)
            {
                accumulate += iter.Current.Fitness;
                valid_iter = iter.MoveNext();
            }

            Chromosome retval;
            if (valid_iter)
            {
                retval = iter.Current;
            }
            else
            {
                Debug.Assert(accumulate == total);
                retval = filtered_chromosomes.Last();
            }

            //key = retval.Fitness - m_min_fitness;
            //int key = retval.Fitness;
            //if (!m_dbg_histogram.ContainsKey(key))
            //    m_dbg_histogram.Add(key, 1);
            //else
            //    m_dbg_histogram[key]++;

            return retval;
        }

        public void Create_Generation()
        {
            List<Chromosome> next_chromosomes = new List<Chromosome>();

            // update current 
            update();
            m_dbg_histogram.Clear();

            // steady state detection 
            if (m_steady_state_prev == m_min_fitness)
                m_steady_state_counter++;
            else
                m_steady_state_counter = 0;
            m_steady_state_prev = m_min_fitness;

            if (m_steady_state_counter == m_steady_state_threshold)
            {
                Debug.Assert(false);
            }

            while(next_chromosomes.Count < m_population)
            {
                // Selection
                Chromosome first = Select();
                Chromosome second = Select();
                Chromosome offspring;

                // Crossover
                if (m_random.NextDouble() < m_Pc)
                {
                    offspring = new Chromosome();
                    offspring.Crossover(first, second);
                }
                else
                {
                    //if (0.5 < m_random.NextDouble())
                    if (first.Fitness < second.Fitness)
                        offspring = first.DeepCopy();
                    else
                        offspring = second.DeepCopy();
                }

                // Mutation 
                if (m_random.NextDouble() < m_Pm)
                {
                    offspring.Mutate();
                }

                //offspring.Index = next_chromosomes.Count + 1;
                offspring.ComputeFitness();
                next_chromosomes.Add(offspring);
            }

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
