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
        DateTime m_running_start_time;
        int m_refreash_rate;

        Series m_avg_series;
        Series m_max_series;
        Series m_min_series;
        Series m_min_ever_series;


        public Form1()
        {
            InitializeComponent();

            RefreashRateTextBox.Text = "15";
            PopulationSizeTextBox.Text = "250";
            CrossoverProbabiltyTextBox.Text = "0.7";
            MutationProbabilityTextBox.Text = "0.02";
            SelectionRangeTextBox.Text = "80";
            LocalMinimumDetectionTextBox.Text = "100";
            ElitesTextBox.Text = "5";

            m_avg_series = ChartControl.Series["Average"];
            m_max_series = ChartControl.Series["Max"];
            m_min_series = ChartControl.Series["Min"];
            m_min_ever_series = ChartControl.Series["Min ever"];

            m_ga = new GA();

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
            m_min_ever_series.Points.Clear();
        }

        delegate void dumpCallback();
        void dump()
        {
            m_max_series.Points.AddY(m_ga.Max_fitness);
            m_avg_series.Points.AddY(m_ga.Avg_fitness);
            m_min_series.Points.AddY(m_ga.Min_fitness);
            m_min_ever_series.Points.AddY(m_ga.Min_fitness_Ever);

            if (m_max_series.Points.Count > 80)
            {
                m_max_series.Points.RemoveAt(0);
                m_avg_series.Points.RemoveAt(0);
                m_min_series.Points.RemoveAt(0);
                m_min_ever_series.Points.RemoveAt(0);
            }

            InstantMinimumTextBox.Text = m_ga.Min_fitness.ToString();
            MinimumFitnessTextBox.Text = m_ga.Min_fitness_Ever.ToString();
            GenerationsTextBox.Text = m_ga.Generations.ToString();

            if (m_running)
            {
                TimeSpan time_diff = DateTime.Now - m_running_start_time;
                RunningTimeTextBox.Text = time_diff.ToString("mm\\:ss");
            }
            else
            {
                RunningTimeTextBox.Text = "";
            }
        }

        void read_controls()
        {
            double pc, pm;
            int selection_range, local_minimum_detection_period;
            int elites;
            int population_size;
            double.TryParse(CrossoverProbabiltyTextBox.Text, out pc);
            double.TryParse(MutationProbabilityTextBox.Text, out pm);
            int.TryParse(SelectionRangeTextBox.Text, out selection_range);
            int.TryParse(LocalMinimumDetectionTextBox.Text, out local_minimum_detection_period);
            int.TryParse(ElitesTextBox.Text, out elites);
            int.TryParse(PopulationSizeTextBox.Text, out population_size);
            int.TryParse(RefreashRateTextBox.Text, out m_refreash_rate);

            m_ga.Pc = pc;
            m_ga.Pm = pm;
            m_ga.SelectionRange = selection_range;
            m_ga.LocalMinimumDetectionPeriod = local_minimum_detection_period;
            m_ga.Elites = elites;

            if (m_ga.Population != population_size)
            {
                clear();
                m_ga.Population = population_size;
            }
        }

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            if (!m_running)
            {
                read_controls();
                m_ga.Initialize();
                m_ga.Randomize();
                clear();
                dump();
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            if (!m_running)
            {
                read_controls();
                m_ga.Create_Generation();
                dump();
            }
        }

        private void runThread()
        {
            m_running_start_time = DateTime.Now;
            DateTime time_sample = m_running_start_time;

            while (m_running)
            {
                // create a new generation 
                m_ga.Create_Generation();

                // detect and start a local minimum escape astrategy 
                m_ga.Local_Minimum_Escape();

                // refresh chart every 30 seconds
                TimeSpan time_diff = DateTime.Now - time_sample;
                if (time_diff.Seconds >= m_refreash_rate)
                {
                    time_sample = DateTime.Now;

                    Debug.Assert(ChartControl.InvokeRequired);
                    dumpCallback d = new dumpCallback(dump);
                    Invoke(d, new object[] {});

                    // stop after 3 minutes 
                    time_diff = DateTime.Now - m_running_start_time;
                    if (time_diff.Minutes >= 3)
                        m_running = false;
                }
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (!m_running)
            {
                read_controls();
                dump();

                m_running = true;
                Thread thread = new Thread(runThread);
                thread.Start();
            }
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            m_running = false;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            m_running = false;
            Application.Exit();
        }

        
        
    }
}
