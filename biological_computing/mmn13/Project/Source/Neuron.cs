using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WindowsFormsApplication1.Source
{
    class Neuron
    {
        int m_X;
        int m_Y;
        protected double m_Value;
        HashSet<Neuron> m_Inputs;

        public int X { get { return m_X; } }
        public int Y { get { return m_Y; } }
        public double Value { get { return m_Value; } }

        public Neuron(int x, int y)
        {
            m_X = x;
            m_Y = y;
            m_Inputs = new HashSet<Neuron>();
        }

        public override string ToString()
        {
            return String.Format("{0}", m_Value);
        }

        public void Randomize()
        {
            m_Value = RandomGen.Get; 
        }
        
        public void ConnectInput(Neuron N)
        {
            Debug.Assert(X != N.X || Y != N.Y);
            Debug.Assert(!m_Inputs.Contains(N));
            m_Inputs.Add(N);
        }

        public virtual void Calculate(bool verbose)
        {
            int dbgX = 0;
            int dbgY = 0;

            double A = 1;
            double B = 1;
            double C = 1;
            double dbgValue1;
            double dbgValue2 = 0;
            double dbgValue3 = 0;
            double dbgValue4;
            double dbgValue5;

            dbgValue1 = m_Value;

            m_Value = 0;
            double Sum = 0; 
            foreach (Neuron Ni in m_Inputs)
            {
                if (Ni.Y == Y && 0 < Ni.Value)
                {
                    m_Value -= B * Ni.Value;
                    if (verbose && X == dbgX && Y == dbgY)
                        Console.WriteLine("({0},{1}): {2}", X, Y, Ni.Value);
                    dbgValue2 -= B * Ni.m_Value;
                }

                if (Ni.X == X && 0 < Ni.Value)
                {
                    m_Value -= A * Ni.Value;
                    if (verbose && X == dbgX && Y == dbgY)
                        Console.WriteLine("({0},{1}): {2}", X, Y, Ni.Value);  
                    dbgValue3 -= A * Ni.m_Value;
                }

                if (0 < Ni.Value)
                    Sum += Ni.Value;
            }

            m_Value -= C*(Sum - 10);
            dbgValue4 = -C*(Sum - 10);
            m_Value = Math.Atan(m_Value);
            dbgValue5 = m_Value;

            m_Value = 0.9 * dbgValue1 + 0.1 * dbgValue5;

            if (verbose && X == dbgX && Y == dbgY)
                Console.WriteLine("({0},{1}) {2}:  {3},{4},{5},{6},{7}", X, Y, Sum, dbgValue1, dbgValue2, dbgValue3, dbgValue4, dbgValue5);  
        }
    }

}
