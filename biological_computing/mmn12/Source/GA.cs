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
        List<Chromosome> m_escape_period;
        int m_max_fitness;
        int m_min_fitness;
        double m_avg_fitness;

        int     m_generations; 
        int     m_min_fitness_prev;
        int     m_min_fitness_ever;
        int     m_local_minimum_detection;
        int     m_local_minimum_escape_period;
        double  m_Pm_save;        

        // properties 
        int     m_population;
        double  m_Pc;        // Crossover probability 
        double  m_Pm;        // Mutation probability 
        int     m_selection_range;
        int     m_local_minimum_detection_period;
        int     m_elites;


        /********************************************************
         *              Accessors 
         ********************************************************/
        public static double NextRandom { get { return m_random.NextDouble(); } }
        public double Pc { get { return m_Pc; } set { m_Pc = value; } }
        public double Pm { get { return m_Pm; } set { m_Pm = value; } }
        public int SelectionRange { get { return m_selection_range; } set { m_selection_range = value; } }
        public int LocalMinimumDetectionPeriod { get { return m_local_minimum_detection_period; } set { m_local_minimum_detection_period = value; } }
        public int Elites { get { return m_elites; } set { m_elites = value; } }
        public double Max_fitness { get { return m_max_fitness; } }
        public double Min_fitness { get { return m_min_fitness; } }
        public double Avg_fitness { get { return m_avg_fitness; } }
        public int Min_fitness_Ever { get { return m_min_fitness_ever; } set { m_min_fitness_ever = value; } }
        public int Generations { get { return m_generations; } set { m_generations = value; } }

        public int Population
        {
            get { return m_population; }
            set
            {
                if (value != m_population)
                {
                    m_population = value;
                    Initialize();
                    Randomize();
                }
            }
        }

        /********************************************************
         *              Methods 
         ********************************************************/

        // Constructor 
        public GA()
        {
            m_random = new Random();
            m_escape_period = new List<Chromosome>();
            m_population = 100;

            Chromosome.StaticInitialize();
            Initialize(); 
            Randomize();
        }

        public void Initialize()
        {
            m_local_minimum_detection = 0;
            m_min_fitness_ever = int.MaxValue;
            m_generations = 0; 
        }

        void update()
        {
            // update statistics 
            m_max_fitness = m_chromosomes.Max(chrom => chrom.Fitness);
            m_min_fitness = m_chromosomes.Min(chrom => chrom.Fitness);
            m_avg_fitness = m_chromosomes.Sum(chrom => chrom.Fitness) / m_chromosomes.Count;
            m_min_fitness_ever = Math.Min(m_min_fitness_ever, m_min_fitness);
        }

        public void Randomize()
        {
            m_chromosomes = new List<Chromosome>();
            for (int i = 0; i < m_population; i++)
            {
                Chromosome chromosome = new Chromosome();
                chromosome.Randomize();
                chromosome.ComputeFitness();
                m_chromosomes.Add(chromosome);
            }

            update();
        }

        Chromosome Select()
        {
            if (m_max_fitness == m_min_fitness)
            {
                // if all population is the same, select first  
                return m_chromosomes.First();
            }

            // Select best 'selection_range' invididuals to choose from 
            List<Chromosome> filtered_chromosomes = m_chromosomes.ToList();
            int range_to_remove = ((100 - m_selection_range) * m_population) / 100;
            filtered_chromosomes.RemoveRange(0, range_to_remove);

            /* 
             * Implemet wheel roulette 
             */
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

            return retval;
        }

        public void Create_Generation()
        {
            // allocater space for new generation 
            List<Chromosome> next_chromosomes = new List<Chromosome>();

            // update min,max and avg statistics 
            update();

            // take elites 
            m_chromosomes.Sort(delegate(Chromosome x, Chromosome y) { return y.Fitness.CompareTo(x.Fitness); });
            next_chromosomes.AddRange(m_chromosomes.Skip(m_population - m_elites));

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

                offspring.ComputeFitness();
                next_chromosomes.Add(offspring);
            }

            // step to next generation 
            m_chromosomes = next_chromosomes;

            update();
            m_generations++; 
        }

        public void Local_Minimum_Escape()
        {
            if (m_local_minimum_escape_period != 0)
            {
                // if inside escape period 
                m_local_minimum_escape_period--; 
                if (m_local_minimum_escape_period == 0)
                {
                    // if escaoe period ends, return best 5 individuals to population
                    m_chromosomes.Sort(delegate(Chromosome x, Chromosome y) { return y.Fitness.CompareTo(x.Fitness); });
                    m_chromosomes.RemoveRange(0, 5);
                    m_chromosomes.AddRange(m_escape_period);
                    m_escape_period.Clear();

                    // restore mutation probabilty 
                    m_Pm = m_Pm_save;
                }
            }
            else
            {
                // local minimum detection 
                if (m_min_fitness_prev == m_min_fitness)
                    m_local_minimum_detection++;
                else
                    m_local_minimum_detection = 0;
                m_min_fitness_prev = m_min_fitness;

                if (m_local_minimum_detection == m_local_minimum_detection_period)
                {
                    m_local_minimum_detection = 0;

                    // if local minimum detected, save best 5 individuals and 
                    m_chromosomes.Sort(delegate(Chromosome x, Chromosome y) { return x.Fitness.CompareTo(y.Fitness); });
                    m_escape_period.Clear();
                    m_escape_period.AddRange(m_chromosomes.Take(5));

                    // start escape period by: 
                    //  1. set mutation probability to double 4 
                    //  2. rerandomize all population 
                    //  3. set escape perios length to detection threshold double 4 
                    m_Pm_save = m_Pm;
                    m_Pm = Math.Min(m_Pm * 4, 1);
                    m_local_minimum_escape_period = m_local_minimum_detection_period * 4;
                    Randomize();
                }
            }

        }

        void Sanity() // Debug 
        {
            foreach (Chromosome chromosome in m_chromosomes)
            {
                chromosome.Sanity();
            }

        }
    }
}
