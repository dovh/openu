using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ApplicationSpace
{
    class Person
    {
        /********************************************************
         *              Members 
         ********************************************************/
        int m_id;  // number from 0 to 49 

        // key: person index (i.e. men, women or dog) 
        // value: preference order 
        Dictionary<int,int> m_first_preferences;
        Dictionary<int,int> m_second_preferences;

        /********************************************************
         *              Methods 
         ********************************************************/

        // Constructor 
        protected Person() 
        {
        }

        protected Person(int id)
       {
            m_id = id;

            /*
             * Randomize first and second preferences lists 
             */

            int preference_index = 0, index;
            m_first_preferences = new Dictionary<int,int>();
            m_second_preferences = new Dictionary<int, int>();

            // create first preferences sets 
            while (m_first_preferences.Count < Chromosome.Capacity)
            {
                do index = (int)(GA.NextRandom * Chromosome.Capacity);
                    while (m_first_preferences.ContainsKey(index));

                m_first_preferences[index] = preference_index;
                preference_index++; 
            }

            // create second preferences sets 
            preference_index = 0;
            while (m_second_preferences.Count < Chromosome.Capacity)
            {
                do index = (int)(GA.NextRandom * Chromosome.Capacity);
                    while (m_second_preferences.ContainsKey(index));

                m_second_preferences[index] = preference_index;
                preference_index++;
            }
        }

        public int preference_first(int index)
        {
            return m_first_preferences[index];
        }

        public int preference_second(int index)
        {
            return m_second_preferences[index];
        }
    }

    class Men : Person
    {
        public Men() : base() { }
        public Men(int id) : base(id) { }
    }

    class Women : Person
    {
        public Women() : base() { }
        public Women(int id) : base(id) { }
    }

    class Dog : Person
    {
        public Dog() : base() { }
        public Dog(int id) : base(id) { }
    }

}
