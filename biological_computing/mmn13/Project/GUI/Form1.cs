using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool m_running;
        DateTime m_running_start_time;
        DataGridViewCellStyle m_DefaultStyle, m_RedStyle, m_BoldStyle;

        Source.Network m_Network;

        void InitializeGrid(DataGridView Grid, int width, int height)
        {
            Grid.RowHeadersVisible = false;
            Grid.ColumnHeadersVisible = false;
            for (int i = 0; i < width; i++)
            {
                Grid.Columns.Add("col" + i.ToString(), i.ToString());
                Grid.Columns[i].Width = Grid.Width / width;
            }
            Grid.Rows.Add(height);
            for (int i = 0; i < height; i++)
                Grid.Rows[i].Height = Grid.Height / height;

            m_DefaultStyle = PreferencesGridView.DefaultCellStyle;

            m_RedStyle = new DataGridViewCellStyle();
            m_RedStyle.Font = new Font(m_DefaultStyle.Font.FontFamily, m_DefaultStyle.Font.Size, m_DefaultStyle.Font.Style);
            m_RedStyle.BackColor = Color.Red;

            m_BoldStyle = new DataGridViewCellStyle();
            m_BoldStyle.Font = new Font(m_DefaultStyle.Font.FontFamily, m_DefaultStyle.Font.Size, FontStyle.Bold);
        }

        public Form1()
        {
            InitializeComponent();

            InitializeGrid(PreferencesGridView, 11, 10);
            InitializeGrid(NetworkGridView, 10, 10);

            // Initialize left preference col 
            for (int i = 0; i < 10; i++) {
                PreferencesGridView.Rows[i].Cells[0].Value = i.ToString();
                PreferencesGridView.Rows[i].Cells[0].Style.ApplyStyle(m_BoldStyle);
            }

            // m_network 
            m_Network = new Source.Network(10, 10);
            m_Network.Randomize();
            dump();
        }

        delegate void dumpCallback(bool mark = false);
        void dump(bool mark = false)
        {
            Source.Preferences data = Source.Preferences.GetInstance();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    PreferencesGridView.Rows[y].Cells[x + 1].Value = data.GetPreferenceByIndex(y, x).ToString();

                    Source.Neuron N = m_Network.Neuron(x, y);
                    NetworkGridView.Rows[y].Cells[x].Value = N.ToString(); 

                    if (mark && N.Value > 0)
                        NetworkGridView.Rows[y].Cells[x].Style.ApplyStyle(m_RedStyle);
                    else
                        NetworkGridView.Rows[y].Cells[x].Style.ApplyStyle(m_DefaultStyle);
                }
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            //m_Network.UpdateWeights();
            m_Network.Step(true);
            dump();
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            m_Network.Randomize();
            dump();
        }

        private void runThread()
        {
            m_running_start_time = DateTime.Now;
            DateTime time_sample = m_running_start_time;

            dumpCallback d = new dumpCallback(dump);

            while (m_running)
            {
                // create a new generation 
                //m_ga.Create_Generation();

                // detect and start a local minimum escape astrategy 
                //m_ga.Local_Minimum_Escape();

                // refresh chart every 30 seconds
                //TimeSpan time_diff = DateTime.Now - time_sample;
                //if (time_diff.Seconds >= m_refreash_rate)
                {
                    //time_sample = DateTime.Now;

                    m_Network.Step(false);

                    //Debug.Assert(ChartControl.InvokeRequired);
                    
                    Invoke(d, new object[] { false });

                    // stop after 3 minutes 
                    //time_diff = DateTime.Now - m_running_start_time;
                    //if (time_diff.Minutes >= 3)
                    //    m_running = false;
                }
            }

            Invoke(d, new object[] { true });
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (!m_running)
            {
                //read_controls();
                //dump();

                m_running = true;
                Thread thread = new Thread(runThread);
                thread.Start();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            m_running = false;
            
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
