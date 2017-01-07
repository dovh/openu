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
        int m_x;
        int m_y;
        double m_value; 

        Dictionary<Neuron, double> m_inputs;

        public int X { get { return m_x; } }
        public int Y { get { return m_y; } }
        public double Value { get { return m_value; } }

        public Neuron(int x, int y)
        {
            m_x = x;
            m_y = y;
            m_inputs = new Dictionary<Neuron, double>();
        }

        public void ConnectInput(Neuron N)
        {
            Debug.Assert(X != N.X || Y != N.Y);
            Debug.Assert(!m_inputs.ContainsKey(N));

            m_inputs[N] = 0;
        }

        public void UpdateWeight(Neuron Ni, double Weight)
        {
            Debug.Assert(X != Ni.X || Y != Ni.Y);
            Debug.Assert(m_inputs.ContainsKey(Ni));
            m_inputs[Ni] = Weight;
        }

        public void Calculate()
        {
            double value = 0;
            foreach (KeyValuePair<Neuron, double> input in m_inputs)
            {
                Neuron Ni = input.Key;
                double weight = Ni.Value;
                value += Ni.Value * weight;
            }

            m_value = value;
        }

    }
}
