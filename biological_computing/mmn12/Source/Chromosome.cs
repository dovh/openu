using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

namespace ApplicationSpace
{
    class Chromosome 
    {
        /********************************************************************
         *          Typedefs 
         ********************************************************************/ 
        class triple_t
        {
            private int m_men, m_women, m_dog;
            private int m_fitness;

            public int Men { get { return m_men; } set { m_men = value; } }
            public int Women { get { return m_women; } set { m_women = value; } }
            public int Dog { get { return m_dog; } set { m_dog = value; } }
            public int Fitness { get { return m_fitness; } set { m_fitness = value; } }

            public int Get(Person o) 
            {
                if (o.GetType() == typeof(Men))
                    return m_men;
                if (o.GetType() == typeof(Women))
                    return m_women;
                if (o.GetType() == typeof(Dog))
                    return m_dog;

                Debug.Assert(false);
                return -1; 
            }

            public void Set(Person o, int val) 
            {
                if (o.GetType() == typeof(Men))
                    m_men = val;
                else if (o.GetType() == typeof(Women))
                    m_women = val;
                else if (o.GetType() == typeof(Dog))
                    m_dog = val;
                else 
                    Debug.Assert(false);
            }

            public triple_t DeepClone()
            {
                return (triple_t) MemberwiseClone();
            }
        };

        /********************************************************************
         *          Static members 
         ********************************************************************/
        // data base 
        static int m_capacity = 50;

        static ArrayList m_men_arr;
        static ArrayList m_women_arr;
        static ArrayList m_dogs_arr;

        /********************************************************************
         *          Members 
         ********************************************************************/ 

        List<triple_t> m_triples = new List<triple_t>();
        int m_fitness = -1;


        /********************************************************************
         *          Acceessesors 
         ********************************************************************/

        public static int Capacity { get { return m_capacity; } }
        public int Fitness { get { return m_fitness; } }

        /********************************************************************
         *          Methods 
         ********************************************************************/ 

        public Chromosome()
        {
        }

        public Chromosome DeepCopy()
        {
            Chromosome copy = (Chromosome)MemberwiseClone();
            copy.m_triples = new List<triple_t>();
            for (int i = 0; i < m_capacity; i++)
                copy.m_triples.Add(m_triples[i].DeepClone());

            return copy;
        }

        static public void StaticInitialize()
        {
            m_men_arr = new ArrayList(m_capacity);
            m_women_arr = new ArrayList(m_capacity);
            m_dogs_arr = new ArrayList(m_capacity);

            for (int i = 0; i < m_capacity; i++)
            {
                m_men_arr.Add(new Men(i));
                m_women_arr.Add(new Women(i));
                m_dogs_arr.Add(new Dog(i));
            }
        }

        public void Randomize()
        {
            m_triples.Clear();

            HashSet<int> menSet = new HashSet<int>();
            HashSet<int> womenSet = new HashSet<int>();
            HashSet<int> dogsSet = new HashSet<int>();

            int imen = -1, iwomen = -1, idog = -1;
            for (int i=0; i<m_capacity; i++)
            {
                // randomize triple 
                do imen = (int)(GA.NextRandom * m_capacity);
                    while (menSet.Contains(imen));
                do iwomen = (int)(GA.NextRandom * m_capacity);
                    while (womenSet.Contains(iwomen));
                do idog = (int)(GA.NextRandom * m_capacity);
                    while (dogsSet.Contains(idog));

                menSet.Add(imen);
                womenSet.Add(iwomen);
                dogsSet.Add(idog);

                triple_t triple = new triple_t();
                triple.Men = imen;
                triple.Women = iwomen;
                triple.Dog = idog;
                m_triples.Add(triple);
            }
        }

        public void ComputeFitness()
        {
            m_fitness = 0; 

            // iterate over all triplets, and sum all preferences differences from optimum 
            foreach (triple_t triple in m_triples)
            {
                Men men = (Men)m_men_arr[triple.Men];
                Women women = (Women)m_women_arr[triple.Women];
                Dog dog = (Dog)m_dogs_arr[triple.Dog];

                int fitness = 0;

                fitness += men.preference_first(triple.Women);
                fitness += men.preference_second(triple.Dog);
                fitness += women.preference_first(triple.Men);
                fitness += women.preference_second(triple.Dog);
                fitness += dog.preference_first(triple.Men);
                fitness += dog.preference_second(triple.Women);

                triple.Fitness = fitness;
                m_fitness += fitness;
            }
        }

        void FixErrors(Person type)
        {
            /*
             * Fix errors by: 
             *  1. detect if a person appear in two triplets 
             *  2. in the second triplet, change its index to a one not used 
             */

            SortedSet<int> indexes = new SortedSet<int>();
            Dictionary<int, int> errors = new Dictionary<int, int>();
            for (int i = 0; i < m_capacity; i++)
            {
                triple_t triple = m_triples[i];
                if (indexes.Contains(triple.Get(type)))
                {
                    Debug.Assert(!errors.ContainsKey(triple.Get(type)));
                    errors[triple.Get(type)] = i;
                }
                else
                    indexes.Add(triple.Get(type));
            }

            foreach (KeyValuePair<int, int> pair in errors)
            {
                // find first unused index 
                int unused = 0;
                while (indexes.Contains(unused))
                    unused++;
                indexes.Add(unused);
                Debug.Assert(unused < m_capacity);

                triple_t triple = m_triples[pair.Value];
                Debug.Assert(triple.Get(type) == pair.Key);
                triple.Set(type, unused);
            }
        }

        public void Crossover(Chromosome first, Chromosome second)
        {
            Debug.Assert(first.m_triples.Count == second.m_triples.Count);
            Debug.Assert(first.m_triples.Count == m_capacity);
            Debug.Assert(m_triples.Count == 0);

            int position = (int)(GA.NextRandom * m_capacity);

            // do single cut, cross over 
            for (int i = 0; i < position; i++)
                m_triples.Add(first.m_triples[i].DeepClone());
            for (int i = position; i < m_capacity; i++)
                m_triples.Add(second.m_triples[i].DeepClone());

            // fix errors 
            FixErrors(new Men());
            FixErrors(new Women());
            FixErrors(new Dog());
        }

        public void Mutate()
        {
            /*
             * Mutate by, 
             *  1. Randomalty select men, women or dog (i.e. person) 
             *  2. Randomalty select two triplets 
             *  3. swap the person in two triplets 
             */
            int index1 = (int)(GA.NextRandom * m_capacity);
            int index2 = (int)(GA.NextRandom * m_capacity);

            Person type = null;
            switch ((int)(GA.NextRandom * 3))
            {
                case 0: type = new Men(); break;
                case 1: type = new Women(); break;
                case 2: type = new Dog(); break;
                default: Debug.Assert(false); break;
            }

            int first = m_triples[index1].Get(type);
            int second = m_triples[index2].Get(type);
            m_triples[index1].Set(type, second);
            m_triples[index2].Set(type, first);
        }

        public void Sanity() // Debug 
        {
            SortedSet<int> indexes1 = new SortedSet<int>();
            SortedSet<int> indexes2 = new SortedSet<int>();
            SortedSet<int> indexes3 = new SortedSet<int>();

            foreach (triple_t triple in m_triples)
            {
                indexes1.Add(triple.Men);
                indexes2.Add(triple.Women);
                indexes3.Add(triple.Dog);
            }

            Debug.Assert(indexes1.Count == m_capacity);
            Debug.Assert(indexes2.Count == m_capacity);
            Debug.Assert(indexes3.Count == m_capacity);
            Debug.Assert(indexes1.Max() == m_capacity - 1);
            Debug.Assert(indexes2.Max() == m_capacity - 1);
            Debug.Assert(indexes3.Max() == m_capacity - 1);
        }

    }
}
