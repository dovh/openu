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

        public virtual bool Calculate(bool verbose)
        {
            Preferences Data = Preferences.GetInstance();
            double A = 1;
            double B = 1;
            double C = 1;
            double D = 1;
            double prevValue = m_Value;

            int selfPreference = Data.GetPreferenceByMen(Y, X + 1);

            double newValue = 0;
            double Sum = 0; 
            foreach (Neuron Ni in m_Inputs)
            {
                if (0 < Ni.Value)
                {
                    if (Ni.Y == Y)
                    {
                        newValue -= A * Ni.Value;

                        Debug.Assert(selfPreference != Data.GetPreferenceByMen(Ni.Y, Ni.X + 1));
                        if (selfPreference >  Data.GetPreferenceByMen(Ni.Y, Ni.X + 1))
                            newValue -= D * Ni.Value;
                        else
                            newValue += D * Ni.Value;
                    }

                    if (Ni.X == X)
                    {
                        newValue -= B * Ni.Value;

                        if ( (selfPreference == Data.GetPreferenceByMen(Ni.Y, Ni.X + 1) && Value < Ni.Value) || 
                              selfPreference > Data.GetPreferenceByMen(Ni.Y, Ni.X + 1))
                            newValue -= D * Ni.Value;
                        else
                            newValue += D * Ni.Value;
                    }

                    Sum += Ni.Value;
                }
            }

            newValue -= C * (Sum - 10);
            newValue = Math.Atan(newValue);

            //m_Value = newValue; 
            m_Value = 0.8 * prevValue + 0.2 * newValue;

            double Change = Math.Abs(m_Value / prevValue - 1);
            bool Stable = Change < 0.0005;
            return Stable;
        }
    }

}
