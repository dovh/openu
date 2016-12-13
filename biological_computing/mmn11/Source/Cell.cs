using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ApplicationSpace
{

    public class Cell
    {
        /********************************************************
         *              Typedefs 
         ********************************************************/
        public enum state { 
            Select1,
            Select2,
            Select3,
            Select4,
            Move,
        };

        public enum direction { None, 
            UpLeft,   Up,   UpRight,
            Left,           Right,
            DownLeft, Down, DownRight,
        };

        public class neighbor : Object
        {
            public direction dir;
            public int x;
            public int y;

            // constructor 
            public neighbor(direction d, int xo, int yo) { dir = d; x = xo; y=yo; }
            public neighbor Clone() { return (neighbor)MemberwiseClone(); }
        };

        /********************************************************
         *              Static Members 
         ********************************************************/

        public static int NumOfStages = 5;

        public static neighbor[] neighbors = new neighbor[] 
        {
            new neighbor(direction.UpLeft,    0, 0),
            new neighbor(direction.Up,        1, 0),
            new neighbor(direction.UpRight,   2, 0),
            new neighbor(direction.Left,      0, 1),
            new neighbor(direction.Right,     2, 1),
            new neighbor(direction.DownLeft,  0, 2),
            new neighbor(direction.Down,      1, 2),
            new neighbor(direction.DownRight, 2, 2),
        };

        /********************************************************
         *              Members 
         ********************************************************/

        state       m_state = state.Select1;
        bool        m_border = false; 
        direction   m_direction = direction.None;
        int         m_male = -1;
        int         m_female = -1;

        /********************************************************
         *              Accessors 
         ********************************************************/

        public int Male         { get { return m_male; } set { m_male = value; } }
        public int Female       { get { return m_female; } set { m_female = value; } }
        public direction Direction { get { return m_direction; } set { m_direction = value; } }
        public state State      { get { return m_state; } set { m_state = value; } }
        public bool Border      { get { return m_border; } set { m_border = value; } }

        public bool IsEmpty()  { return m_male == -1 && m_female == -1; }
        public bool IsMale()   { return m_male != -1; }
        public bool IsFemale() { return m_female != -1; }
        public bool IsSingle() { return m_male == -1 ^ m_female == -1; }
        public bool IsCouple() { return m_male != -1 && m_female != -1; }
        public bool IsBorder() { return m_border ; }

        /********************************************************
         *              Methods 
         ********************************************************/

        public void Initialize()
        {
            m_state = state.Select1;
            m_border = false;
            m_direction = direction.None;
            m_male = -1;
            m_female = -1;
        }

        public void Empty()    
        { 
            m_male = -1; 
            m_female = -1; 
        }

        public void Copy(Cell other)
        {
            m_male = other.Male;
            m_female = other.Female;
        }

        // claculate self match 
        public int Match()
        {
            if (IsCouple())
                return 100 - Math.Abs(Male - Female);
            else
                return 0;
        }

        public int Match(Cell other)
        {
            int retval = 0; 

            // Note that, in case of matching couple to couple 
            //   the match is the maximum match between female to male and male to female.

            if (IsMale() && other.IsFemale())
                retval = Math.Max(retval, 100 - Math.Abs(Male - other.Female));

            if (IsFemale() && other.IsMale())
                retval = Math.Max(retval, 100 - Math.Abs(Female - other.Male));

            // Uncomment it, to disable couple to single matching 
            //if ( (IsSingle() && other.IsCouple()) || (IsCouple() && other.IsSingle()))
            //    return 0;

            return retval;
        }

        public void Merge(Cell center, Cell second)
        {
            if (center.IsEmpty())
            {
                // movment from neighbor to center 

                Copy(second);
            }
            else if (second.IsEmpty())
            {
                // movment from center to neighbor 

                Empty();
            }
            else if (center.IsSingle() && second.IsSingle())
            {
                // merge single to single

                if (center.IsMale() && second.IsFemale() && center.Male > second.Female)
                {
                    Male = center.Male;
                    Female = second.Female;
                }
                else if (center.IsFemale() && second.IsMale() && center.Female > second.Male)
                {
                    Male = second.Male;
                    Female = center.Female;
                }
            }
            else if (center.IsSingle() && second.IsCouple())
            {
                // merge single to couple 

                if (center.IsMale())
                {
                    Male = second.Male;
                }
                else if (center.IsFemale())
                {
                    Female = second.Female;
                }
            }
            else if (center.IsCouple() && second.IsSingle())
            {
                // merge couple to single
                
                if (second.IsFemale())
                {
                    Male = center.Male;
                    Female = second.Female;
                }
                else if (second.IsMale())
                {
                    Male = second.Male;
                    Female = center.Female;
                }
            }
            else if (center.IsCouple() && second.IsCouple())
            {
                // merge couple to couple 

                Male = center.Male;
                Female = second.Female;
            }
            else
            {
                Debug.Assert(false);
            }
        }

        public override string ToString()
        {
            string retval = "";

            if (m_male != -1)
                retval += m_male.ToString();

            if (m_female != -1)
            {
                if (retval.Length != 0) retval += ",";
                retval += m_female.ToString();
            }

            if (m_direction != direction.None)
            {
                if (retval.Length != 0) retval += ",";
                retval += m_direction.ToString();
            }

            return retval;
        }

    }
}
