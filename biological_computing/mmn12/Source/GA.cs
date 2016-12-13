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

        SortedDictionary<int, int> m_dbg_histogram = new SortedDictionary<int, int>();
        SortedDictionary<double, int> m_dbg_histogram2 = new SortedDictionary<double, int>();
        SortedDictionary<int, int> m_dbg_histogram3 = new SortedDictionary<int, int>();

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

            // for debug 
            //Comparison<Chromosome> comp = new Comparison<Chromosome>();
            m_chromosomes.Sort(delegate(Chromosome x, Chromosome y) { return y.Fitness.CompareTo(x.Fitness); });

            update();
        }

        Chromosome Select()
        {
/*
            m_dbg_histogram3.Clear();
            foreach(Chromosome chrom in m_chromosomes)
            {
                if (!m_dbg_histogram3.ContainsKey(chrom.Fitness))
                    m_dbg_histogram3[chrom.Fitness] = 1;
                else
                    m_dbg_histogram3[chrom.Fitness]++;
            }
*/

            List<Chromosome>.Enumerator iter = m_chromosomes.GetEnumerator();

            //double total = m_sumof_delta_to_max_fitness; 
            //double total = m_sumof_fitness; 
            double total = m_chromosomes.Sum(chrom => Math.Pow(chrom.Fitness - m_min_fitness, 1));
            //double wheel = m_random.NextDouble() * total;
            double wheel = Math.Min(m_random.NextDouble() + 0.5, 1) * total;
            //Console.WriteLine("--- {0:N} {1:N}", total, wheel);

            int min_fitness = m_chromosomes.Min(chrom => chrom.Fitness);

            double accumulate = 0;

            int index = 0;
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
                //accumulate += iter.Current.Fitness;
                accumulate += Math.Pow(iter.Current.Fitness - m_min_fitness, 1);

                //Console.WriteLine("{0} {1:N0} {2:N0}", index, Math.Pow(iter.Current.Fitness - m_min_fitness, 3), accumulate);

                valid_iter = iter.MoveNext();
                index++;
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
            int key = (int)((wheel * 100) / total);
            if (!m_dbg_histogram.ContainsKey(key))
                m_dbg_histogram.Add(key, 1);
            else
                m_dbg_histogram[key]++;

            //key = retval.Fitness - m_min_fitness;
            double dkey = Math.Pow(retval.Fitness - m_min_fitness, 1);
            if (!m_dbg_histogram2.ContainsKey(dkey))
                m_dbg_histogram2.Add(dkey, 1);
            else
                m_dbg_histogram2[dkey]++;

            return retval;
        }

        public void Create_Generation()
        {
            List<Chromosome> next_chromosomes = new List<Chromosome>();

            //Chromosome find = m_chromosomes.Find(ch => ch.Size != 50);
            //Debug.Assert(find == null);

            // update current 
            update();
            m_dbg_histogram.Clear();
            m_dbg_histogram2.Clear();

            for (int i = 0; i < m_population; i++)
            //while(next_chromosomes.Count < m_population)
            {
                //find = m_chromosomes.Find(ch => ch.Size != 50);
                //Debug.Assert(find == null);

                // Selection
                Chromosome first = Select();
                Chromosome second = Select();
                Chromosome offspring;

                //find = m_chromosomes.Find(ch => ch.Size != 50);
                //Debug.Assert(find == null);
                //Debug.Assert(first.Index != 1 && second.Index != 1);

                // Crossover
                if (m_random.NextDouble() < m_Pc)
                {
                    offspring = new Chromosome(i);
                    offspring.Crossover(first, second);
                }
                else
                {
                    //if (0.5 < m_random.NextDouble())
                    if (first.Fitness > second.Fitness)
                        offspring = first;
                    else
                        offspring = second;
                    offspring.Index = i;
                }

                //find = m_chromosomes.Find(ch => ch.Size != 50);
                //Debug.Assert(find == null);

                // Mutation 
                if (m_random.NextDouble() < m_Pm)
                {
                    //offspring.Mutate();
                }

                offspring.UpdateFitness();
                next_chromosomes.Add(offspring);

                //find = m_chromosomes.Find(ch => ch.Size != 50);
                //Debug.Assert(find == null);
            }

            // step to next generation 
            m_chromosomes = next_chromosomes;

            m_chromosomes.Sort(delegate(Chromosome x, Chromosome y) { return y.Fitness.CompareTo(x.Fitness); });

            //find = m_chromosomes.Find(ch => ch.Size != 50);
            //Debug.Assert(find == null);
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
