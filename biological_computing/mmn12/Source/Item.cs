using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ApplicationSpace
{
    class Item
    {
        /********************************************************
         *              Members 
         ********************************************************/
        int m_id;  // number from 0 to 49 

        // key: item
        // value: preference order 
        Dictionary<int,int> m_first_preferences;
        Dictionary<int,int> m_second_preferences;

        /********************************************************
         *              Methods 
         ********************************************************/

        // Constructor 
        protected Item()
        {
        }

        protected Item(int id)
        {
            m_id = id;

            // create first preferences sets 
            int preference = 0, item;
            m_first_preferences = new Dictionary<int,int>();
            m_second_preferences = new Dictionary<int, int>();

            while (m_first_preferences.Count < Chromosome.Capacity)
            {
                do item = (int)(GA.NextRandom * Chromosome.Capacity);
                    while (m_first_preferences.ContainsKey(item));

                m_first_preferences[item] = preference;
                preference++; 
            }

            // create second preferences sets 
            preference = 0;
            while (m_second_preferences.Count < Chromosome.Capacity)
            {
                do item = (int)(GA.NextRandom * Chromosome.Capacity);
                while (m_second_preferences.ContainsKey(item));

                m_second_preferences[item] = preference;
                preference++;
            }
        }

        public int preference_first(int item)
        {
            return m_first_preferences[item];
        }

        public int preference_second(int item)
        {
            return m_second_preferences[item];
        }
    }

    class Men : Item
    {
        // Constructor 
        public Men() : base() { }
        public Men(int id) : base(id) { }
    }

    class Women : Item
    {
        // Constructor 
        public Women() : base() { }
        public Women(int id) : base(id) { }
    }

    class Dog : Item
    {
        // Constructor 
        public Dog() : base() { }
        public Dog(int id) : base(id) { }
    }

}
