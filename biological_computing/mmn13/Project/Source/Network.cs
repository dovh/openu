using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Source
{
    class Network
    {
        static double m_A, m_B, m_C, m_D;

        int m_width, m_height;
        Neuron[,] m_neurons;

        public Neuron Neuron(int x, int y)
        {
            return m_neurons[x, y];
        }

        public Network(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_neurons = new Neuron[m_width, m_height + 1];

            m_A = 1;
            m_B = 1;
            m_C = 1;
            m_D = 1;

            // Create matrix neurons 
            for (int y = 0; y < m_height; y++)
                for (int x = 0; x < m_width; x++)
                    m_neurons[x, y] = new Neuron(x, y);

            // Create N0 neuron - Represent Factor C - Number of total set neurons 
            Neuron N0 = new NeuronN0(0, m_height + 1);
            m_neurons[0, m_height] = N0;

            // Create N1 neuron - Represent Factor D - Total happiness 
            Neuron N1 = new Neuron(1, m_height + 1);
            m_neurons[1, m_height] = N1;

            // connect matrix neurons 
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    Neuron N = m_neurons[x, y];

                    for (int y2 = 0; y2 < m_height; y2++)
                    {
                        for (int x2 = 0; x2 < m_width; x2++)
                        {
                            if (x == x2 && y == y2)
                                continue;

                            Neuron Ni = m_neurons[x2, y2];
                            N.ConnectInput(Ni);
                        }
                    }

                    N.ConnectInput(N0);
                    N0.ConnectInput(N);

                    //N.ConnectInput(N1);
                    //N1.ConnectInput(N);
                }
            }
        }

        public void Randomize()
        {
            Neuron N0 = m_neurons[0, m_height];
            Neuron N1 = m_neurons[1, m_height];

            // randomize neurons values 
            for (int y = 0; y < m_height; y++)
                for (int x = 0; x < m_width; x++)
                    m_neurons[x, y].Randomize();

            N0.Randomize();
            N1.Randomize();
        }

        public void UpdateWeights()
        {
            Neuron N0 = m_neurons[0, m_height];
            Neuron N1 = m_neurons[1, m_height];

            // Update matrix neurons weight 
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    Neuron N = m_neurons[x, y];

                    for (int y2 = 0; y2 < m_height; y2++)
                    {
                        for (int x2 = 0; x2 < m_width; x2++)
                        {
                            if (x == x2 && y == y2)
                                continue;

                            Neuron Ni = m_neurons[x2, y2];

                            double Weight = 0;
                            if (x == x2 || y == y2)
                            {
                                if (N.Value == 1 && Ni.Value == 1)
                                    Weight = -1;
                                else if (N.Value == 1 && Ni.Value == -1)
                                    Weight = -1.1;
                                else if (N.Value == -1 && Ni.Value == 1)
                                    Weight = -1.1;
                                else // if (N.Value == -1 && Ni.Value == -1)
                                    Weight = 0;
                            }

                            N.UpdateWeight(Ni, 0.5 * m_B * Weight);
                        }
                    }

                    N.UpdateWeight(N0, m_C * N.Value);
                    N0.UpdateWeight(N, m_C * N.Value);

                    //N.UpdateWeight(N1, m_D);
                    //N1.UpdateWeight(N, m_D);
                }
            }
        }

        public void Step()
        {
            Neuron N0 = m_neurons[0, m_height];
            Neuron N1 = m_neurons[1, m_height];

            // Calculate matrix neurons weight 
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    Neuron N = m_neurons[x, y];
                    N.Calculate();
                }
            }

            N0.Calculate();
            //N1.Calculate();

        }

        
    }
}
