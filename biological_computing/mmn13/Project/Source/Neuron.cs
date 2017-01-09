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

        //static double m_A = 1;
        //static double m_B = 1;
        //static double m_C = 1;
        //static double m_D = 1;

        //protected Dictionary<Neuron, double> m_Inputs;
        HashSet<Neuron> m_Inputs;

        public int X { get { return m_X; } }
        public int Y { get { return m_Y; } }
        public double Value { get { return m_Value; } }

        public Neuron(int x, int y)
        {
            m_X = x;
            m_Y = y;
            //m_Inputs = new Dictionary<Neuron, double>();
            m_Inputs = new HashSet<Neuron>();
        }

        public override string ToString()
        {
            return String.Format("{0}", m_Value);
        }

        public void Randomize()
        {
            //m_Value = RandomGen.Get;
            m_Value = RandomGen.Get; // *Math.PI / 2;// -Math.PI / 2;
        }

        
        public void ConnectInput(Neuron N)
        {
            Debug.Assert(X != N.X || Y != N.Y);
            //Debug.Assert(!m_Inputs.ContainsKey(N));
            Debug.Assert(!m_Inputs.Contains(N));

            //m_Inputs[N] = 0;
            m_Inputs.Add(N);
        }

        /*public void UpdateWeight(Neuron Ni, double Weight)
        {
            Debug.Assert(X != Ni.X || Y != Ni.Y);
            Debug.Assert(m_Inputs.ContainsKey(Ni));
            m_Inputs[Ni] = Weight;
        }*/

        public virtual void Calculate(bool verbose)
        {
            /*double H = 0;
            foreach (KeyValuePair<Neuron, double> input in m_Inputs)
            {
                Neuron Ni = input.Key;
                double Weight = input.Value;
                H += Ni.Value * Weight;
            }

            if (0 < H)
                m_Value = 1;
            else if (H < 0)
                m_Value = -1; 
             */

            int dbgX = 0;
            int dbgY = 0;

            double A = 1;
            double B = 1;
            double C = 1.8;
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

            if (verbose && X == dbgX && Y == dbgY)
                Console.WriteLine("({0},{1}) {2}:  {3},{4},{5},{6},{7}", X, Y, Sum, dbgValue1, dbgValue2, dbgValue3, dbgValue4, dbgValue5);  
        }
    }

    /*class NeuronN0 : Neuron
    {
        public NeuronN0(int x, int y) 
            : base(x,y) { }

        public override void Calculate()
        {
            double H = 0;
            foreach (KeyValuePair<Neuron, double> input in m_Inputs)
            {
                Neuron Ni = input.Key;
                double Weight = input.Value;
                H += Ni.Value * Weight;
            }

            if (H == 10)
                m_Value = 1;
            else 
                m_Value = -1; 

        }

    }*/
}
