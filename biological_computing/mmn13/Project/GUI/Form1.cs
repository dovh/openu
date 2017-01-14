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
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool m_running;
        bool m_batch;
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
                PreferencesGridView.Rows[i].Cells[0].Value = (i+1).ToString();
                PreferencesGridView.Rows[i].Cells[0].Style.ApplyStyle(m_BoldStyle);
            }

            m_running = false;
            m_batch = false;

            // m_network 
            m_Network = new Source.Network(10, 10);
            m_Network.Randomize();
            dump();
        }

        delegate void dumpCallback(bool Mark = false);
        void dump(bool Mark = false)
        {
            Source.Preferences data = Source.Preferences.GetInstance();
            for (int y = 0; y < 10; y++)
            {
                int Marked = -1; 
                for (int x = 0; x < 10; x++)
                {
                    Source.Neuron N = m_Network.Neuron(x, y);
                    NetworkGridView.Rows[y].Cells[x].Value = N.ToString(); 

                    if (Mark && N.Value > 0)
                    {
                        NetworkGridView.Rows[y].Cells[x].Style.ApplyStyle(m_RedStyle);
                        Marked = x + 1;
                    }
                    else
                        NetworkGridView.Rows[y].Cells[x].Style.ApplyStyle(m_DefaultStyle);
                }

                for (int x = 0; x < 10; x++)
                {
                    int Preference = data.GetPreferenceByIndex(y, x);
                    PreferencesGridView.Rows[y].Cells[x + 1].Value = Preference.ToString();

                    if (Mark && Marked == Preference)
                        PreferencesGridView.Rows[y].Cells[x + 1].Style.ApplyStyle(m_RedStyle);
                    else
                        PreferencesGridView.Rows[y].Cells[x + 1].Style.ApplyStyle(m_DefaultStyle);
                }
            }

            if (Mark)
            { 
                TotalHappinessTextBox.Text = String.Format("{0:00.00}%", m_Network.GetTotalHappines());
                if (m_Network.IsLegaySolution())
                {
                    LegalSolutionTextBox.Text = "Yes";
                    LegalSolutionTextBox.BackColor = Color.Green;
                }
                else
                {
                    LegalSolutionTextBox.Text = "NO";
                    LegalSolutionTextBox.BackColor = Color.Red;
                }
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            m_Network.Step(true);
            dump(true);
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            Source.Preferences Data = Source.Preferences.GetInstance();
            Data.Randomize();
            m_Network.Randomize();

            dump();
        }

        private void runThread()
        {
            m_running_start_time = DateTime.Now;
            DateTime time_sample = m_running_start_time;

            for(int batch_index = 0; batch_index<100; batch_index++)
            {
                bool Stable = false;
                while (m_running && !Stable)
                {
                    Stable = m_Network.Step(false);
                    dumpCallback d = new dumpCallback(dump);
                    if (d != null) Invoke(d, new object[] { false });
                }

                dumpCallback d2 = new dumpCallback(dump);
                if (d2 != null) Invoke(d2, new object[] { true });

                if (m_batch && m_running)
                {
                    Console.WriteLine("{0}: {1}, {2}", batch_index, TotalHappinessTextBox.Text, m_Network.IsLegaySolution());

                    if (!m_Network.IsLegaySolution())
                        Console.Write("");

                    Source.Preferences Data = Source.Preferences.GetInstance();
                    Data.Randomize();
                    m_Network.Randomize();
                }
                else
                    break;
            }

            m_running = false;
            m_batch = false;
        }

        private void RunBatchButton_Click(object sender, EventArgs e)
        {
            if (!m_running)
                m_batch = true;

            RunButton_Click(sender, e);
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (!m_running)
            {
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
            m_batch = false;
            m_running = false;
            Application.Exit();
        }

        

        
    }
}
