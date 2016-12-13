using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ApplicationSpace
{

    public class CA
    {
        /********************************************************
         *              Static Members 
         ********************************************************/

        static Random m_rnd = new Random();
        static int m_match_threshold;
        static int m_random_seed; 

        /********************************************************
         *              Members 
         ********************************************************/

        //  properties
        int m_layout;       // size of layout
        int m_N;            // neighborhod 
        int m_population;   // population size 

        // Data base 
        Cell[,]             m_cells;
        Transition_rules    m_transition_rules = new Transition_rules();

        // settings 
        int m_step_number;
        int m_num_singles;
        int m_num_couples;
        int m_total_happiness;
        bool m_show_stages;

        /********************************************************
         *              Accessors 
         ********************************************************/

        public int  Population      { get { return m_population; } }
        public int  Layout          { get { return m_layout; } }
        public int  NumSingles      { get { return m_num_singles; } }
        public int  NumCouples      { get { return m_num_couples; } }
        public int  TotalHappiness  { get { return m_total_happiness; } }
        public int  StepNo          { get { return m_step_number; } }
        public bool ShowStages      { get { return m_show_stages; } set { m_show_stages = value; } }
        public static int MatchThreshold { get { return m_match_threshold; } set { m_match_threshold = value; } }
        public static int RandomSeed { get { return m_random_seed; } set { m_random_seed = value; } }
        public static double NextRandom     { get { return m_rnd.NextDouble(); } }

        /********************************************************
         *              Methods 
         ********************************************************/

        /* Constructor */
        public CA(int layout, int population)
        {
            m_layout = layout;
            m_N = 1;
            m_population = population;

            m_cells = new Cell[m_layout, m_layout];
            for (int i = 0; i < m_layout; i++)
                for (int j = 0; j < m_layout; j++)
                    m_cells[i, j] = new Cell();

            Initialize();
        }

        public void Initialize()
        {
            // use random seed, if defined 
            if (m_random_seed != 0)
                m_rnd = new Random(m_random_seed);

            // Empty layout 
            for (int i = 0; i < m_layout; i++)
                for (int j = 0; j < m_layout; j++)
                    m_cells[i, j].Initialize();

            // allocate new indexes and positions 
            HashSet<int> indexes = new HashSet<int>();
            while (indexes.Count < m_population)
            {
                int index = 0, x = 0, y = 0; 

                // find random free index 
                bool found = false;
                while (!found)
                {
                    index = (int)(NextRandom * m_population);
                    found = indexes.Add(index);
                }
                
                // find random free position 
                found = false;
                while (!found)
                {
                    x = (int)(NextRandom * m_layout);
                    y = (int)(NextRandom * m_layout);
                    x = Math.Max(x, 1);
                    y = Math.Max(y, 1);
                    x = Math.Min(x, m_layout - 2);
                    y = Math.Min(y, m_layout - 2);

                    found = m_cells[x, y].IsEmpty();
                }

                if (indexes.Count <= m_population / 2)
                    m_cells[x, y].Male = index;
                else
                    m_cells[x, y].Female = index;
            }

            m_step_number = 0;
            set_borders();
            update_statistic();
        }

        public Cell Cells(int col, int row) 
        { 
            return m_cells[col, row]; 
        }

        void set_borders()
        {
            for (int i = 0; i < m_layout; i++)
            {
                m_cells[0, i].Border = true;
                m_cells[m_layout - 1, i].Border = true;
                m_cells[i, 0].Border = true;
                m_cells[i, m_layout - 1].Border = true;
            }
        }

        void update_statistic()
        {
            m_num_singles = 0;
            m_num_couples = 0;
            m_total_happiness = 0;
            for (int i = 0; i < m_layout; i++)
            for (int j = 0; j < m_layout; j++)
            {
                Cell cell = m_cells[i, j];

                if (cell.IsSingle())
                    m_num_singles++;
                else if (cell.IsCouple())
                    m_num_couples++;

                m_total_happiness += cell.Match();
            }
        }

        public void sanity_check()
        {
            for (int n = 0; n < m_population; n++)
            {
                int count = 0;
                for (int j = 1; j < m_layout - 1; j++)
                    for (int i = 1; i < m_layout - 1; i++)
                    {
                        Cell cell = m_cells[i, j];
                        if (cell.Male == n || cell.Female == n)
                            count++;
                    }

                Debug.Assert(count == 1);
            }
        }

        public void StepState()
        {
            // allocate next step layout 
            Cell[,] next_cells = new Cell[m_layout, m_layout];
            for (int i = 0; i < m_layout; i++)
                for (int j = 0; j < m_layout; j++)
                    next_cells[i, j] = new Cell();

            for (int j = 1; j < m_layout - 1; j++)
            for (int i = 1; i < m_layout - 1; i++)
            {
                // sample current neighbors
                Cell[,] neighbors = new Cell[1 + m_N*2, 1 + m_N*2];
                for (int y = -m_N; y <= m_N; y++)
                for (int x = -m_N; x <= m_N; x++)
                    neighbors[x + m_N, y + m_N] = m_cells[i + x, j + y];

                // point to next step cell
                Cell next_cell = next_cells[i, j];

                // run transition rules 
                m_transition_rules.Step(neighbors, ref next_cell);
            }

            // swap to next layout 
            m_cells = next_cells;

            // set layout borders 
            set_borders();
        }

        public void Step()
        {
            if (m_show_stages)
            {
                StepState();
            }
            else
            {
                for (int i = 0; i < Cell.NumOfStages; i++)
                    StepState();
            }

            update_statistic();
            m_step_number++;
        }

        
    }
}
