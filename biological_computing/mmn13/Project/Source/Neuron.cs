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
        double m_Value; 

        Dictionary<Neuron, double> m_Inputs;

        public int X { get { return m_X; } }
        public int Y { get { return m_Y; } }
        public double Value { get { return m_Value; } }

        public Neuron(int x, int y)
        {
            m_X = x;
            m_Y = y;
            m_Inputs = new Dictionary<Neuron, double>();
        }

        public void ConnectInput(Neuron N)
        {
            Debug.Assert(X != N.X || Y != N.Y);
            Debug.Assert(!m_Inputs.ContainsKey(N));

            m_Inputs[N] = 0;
        }

        public void UpdateWeight(Neuron Ni, double Weight)
        {
            Debug.Assert(X != Ni.X || Y != Ni.Y);
            Debug.Assert(m_Inputs.ContainsKey(Ni));
            m_Inputs[Ni] = Weight;
        }

        public void Calculate()
        {
            m_Value = 0;
            foreach (KeyValuePair<Neuron, double> input in m_Inputs)
            {
                Neuron Ni = input.Key;
                double Weight = input.Value;
                m_Value += Ni.Value * Weight;
            }
        }

        public void Randomize()
        {
            m_Value = RandomGen.Get;
        }

    }
}
