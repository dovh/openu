using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Source
{
    class Network
    {

        int m_step_x, m_step_y;

        int m_width, m_height;
        Neuron[,] m_neurons;

        public Neuron Neuron(int x, int y)
        {
            return m_neurons[y, x];
        }

        public Network(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_neurons = new Neuron[m_height, m_width];

            m_step_x = 0;
            m_step_y = 0;

            // Create matrix neurons 
            for (int y = 0; y < m_height; y++)
                for (int x = 0; x < m_width; x++)
                    m_neurons[y, x] = new Neuron(x, y);

            // connect matrix neurons 
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    Neuron N = m_neurons[y, x];

                    for (int y2 = 0; y2 < m_height; y2++)
                    {
                        for (int x2 = 0; x2 < m_width; x2++)
                        {
                            if (x == x2 && y == y2)
                                continue;

                            Neuron Ni = m_neurons[y2, x2];
                            N.ConnectInput(Ni);
                        }
                    }
                }
            }
        }

        public void Randomize()
        {
            // randomize neurons values 
            for (int y = 0; y < m_height; y++)
                for (int x = 0; x < m_width; x++)
                    m_neurons[y, x].Randomize();
        }

        public void Step(bool verbose)
        {
            int x = m_step_x;
            int y = m_step_y;

            m_step_x++;
            if (m_step_x == m_width)
            {
                m_step_x = 0;
                m_step_y++;
                if (m_step_y == m_height)
                    m_step_y = 0;
            }

            // Calculate matrix neurons weight 
            for ( y = 0; y < m_height; y++)
            {
                for ( x = 0; x < m_width; x++)
                {
                    Neuron N = m_neurons[y, x];
                    N.Calculate(verbose);
                }
            }
        }

        
    }
}
