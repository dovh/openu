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
        double m_NextValue; 

        Dictionary<Neuron, double> m_Inputs;

        public int X { get { return m_X; } }
        public int Y { get { return m_Y; } }
        public double Value { get { return m_Value; } }
        public double NextValue { get { return m_NextValue; } }

        public Neuron(int x, int y)
        {
            m_X = x;
            m_Y = y;
            m_Inputs = new Dictionary<Neuron, double>();
        }

        public void Randomize()
        {
            m_Value = RandomGen.Get * 2 - 1;
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

        public void CalculateNextValue()
        {
            m_NextValue = 0;
            foreach (KeyValuePair<Neuron, double> input in m_Inputs)
            {
                Neuron Ni = input.Key;
                double Weight = input.Value;
                m_NextValue += Ni.Value * Weight;
            }
        }

        public void NormalizeNextValueAndUpdate(double Norm)
        {
            m_Value = m_NextValue / Norm;
        }

        

    }
}
