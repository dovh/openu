using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Threading;

namespace ApplicationSpace
{
    public partial class Form1 : Form
    {
        GA     m_ga;
        bool   m_running; 

        Series m_avg_series;
        Series m_max_series;
        Series m_min_series;
        int    m_refresh_rate; 
        int    m_refresh_count; 


        public Form1()
        {
            InitializeComponent();

            CrossoverProbabiltyTextBox.Text = "0.7";
            MutationProbabilityTextBox.Text = "0.02";

            m_avg_series = ChartControl.Series["Average"];
            m_max_series = ChartControl.Series["Max"];
            m_min_series = ChartControl.Series["Min"];

            m_ga = new GA();
            m_refresh_rate = 30;
            m_refresh_count = 0;

            clear();
            dump();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void clear()
        {
            m_avg_series.Points.Clear();
            m_max_series.Points.Clear();
            m_min_series.Points.Clear();
        }

        delegate void dumpCallback();
        void dump()
        {
            m_max_series.Points.AddY(m_ga.Max_fitness);
            m_min_series.Points.AddY(m_ga.Min_fitness);
            m_avg_series.Points.AddY(m_ga.Avg_fitness);
        }

        void read_controls()
        {
            double pc, pm;
            double.TryParse(CrossoverProbabiltyTextBox.Text, out pc);
            double.TryParse(MutationProbabilityTextBox.Text, out pm);
            m_ga.Pc = pc;
            m_ga.Pm = pm;
        }

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            read_controls();
            m_ga.Initialize();

            clear();
            dump();
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            read_controls();
            m_ga.Create_Generation();
            dump();
        }

        private void runThread()
        {
            while (m_running)
            {
                m_ga.Create_Generation();

                m_refresh_count++;
                if (m_refresh_count == m_refresh_rate)
                {
                    m_refresh_count = 0;

                    Debug.Assert(ChartControl.InvokeRequired);
                    dumpCallback d = new dumpCallback(dump);
                    Invoke(d, new object[] {});
                }
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            read_controls();

            m_running = true;
            Thread thread = new Thread(runThread);
            thread.Start();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            m_running = false;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        
    }
}
