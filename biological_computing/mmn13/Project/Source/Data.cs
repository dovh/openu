using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WindowsFormsApplication1.Source
{
    class Data
    {
        static Data m_instance = null; 
        Random m_random;
        int[,] m_preferences;

        Data()
        {
            m_random = new Random(123);
            m_preferences = new int[10, 10];
            Randomize();
        }

        static public Data GetInstance()
        {
            if (m_instance == null)
                m_instance = new Data();
            return m_instance;
        }

        void Randomize()
        {
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < 10; i++)
            {
                int j = 0;
                set.Clear();
                while(set.Count < 10)
                {
                    int rnd = (int)(m_random.NextDouble() * 10);
                    if (!set.Contains(rnd))
                    {
                        set.Add(rnd);
                        m_preferences[i, j++] = rnd + 1;
                    }
                }
            }
        }

        public int GetPreference(int women, int index)  
        {
            Debug.Assert(0 <= index && index < 10);
            return m_preferences[women, index];
        }

        public int GetMenPreference(int women, int men)
        {
            Debug.Assert(1 <= men && men <= 10);

            for (int preference = 0; preference < 10; preference++)
                if (m_preferences[women, preference] == men)
                    return preference + 1;

            Debug.Assert(false);
            return -1;
        }

    }
}
