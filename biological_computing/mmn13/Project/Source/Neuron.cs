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
        protected int m_Value;

        protected Dictionary<Neuron, double> m_Inputs;

        public int X { get { return m_X; } }
        public int Y { get { return m_Y; } }
        public int Value { get { return m_Value; } }

        public Neuron(int x, int y)
        {
            m_X = x;
            m_Y = y;
            m_Inputs = new Dictionary<Neuron, double>();
        }

        public void Randomize()
        {
            if (0.5 < RandomGen.Get)
                m_Value = 1;
            else
                m_Value = -1;
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

        public virtual void Calculate()
        {
            double H = 0;
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
        }
    }

    class NeuronN0 : Neuron
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

    }
}
