using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationSpace
{
    public partial class Form1 : Form
    {
        CA  m_ca;
        int m_stage; 

        public Form1()
        {
            m_ca = new CA(20, 100);
            CA.MatchThreshold = 50; 

            m_stage = 0; 

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;
            for (int i = 0; i < m_ca.Layout; i++)
                dataGridView.Columns.Add("col" + i.ToString(), "col" + i.ToString());
            dataGridView.Rows.Add(m_ca.Layout);
            dataGridView.AutoResizeColumns();

            MatchThresholdTextBox.Text = CA.MatchThreshold.ToString();

            dump();
        }

        private void read_controls()
        {
            // read match threshold 
            int outval = -1;
            if (!int.TryParse(MatchThresholdTextBox.Text, out outval))
                outval = -1;
            if (outval < 0 || 100 < outval)
                outval = -1;
            if (outval == -1)
            {
                MessageBox.Show("failed to parse 'Match Threshold', should range from 0 to 100");
                return;
            }

            CA.MatchThreshold = outval;

            outval = 0;
            if (RandomSeepTextBox.Text.Length != 0 && !int.TryParse(RandomSeepTextBox.Text, out outval))
            {
                MessageBox.Show("failed to parse 'Randm Seed'");
                outval = 0;
            }

            CA.RandomSeed = outval;
            
        }

        private void dump()
        {
            for (int i = 1; i < m_ca.Layout; i++)
            for (int j = 1; j < m_ca.Layout; j++)
            {
                Cell cell = m_ca.Cells(i,j);
                DataGridViewCell viewCell = dataGridView.Rows[j].Cells[i];

                viewCell.Value = cell.ToString();

                if (cell.IsSingle() && cell.IsMale())
                    viewCell.Style.ForeColor = Color.Blue;
                else if (cell.IsSingle() && cell.IsFemale())
                    viewCell.Style.ForeColor = Color.Red;
                else if (cell.IsCouple())
                    viewCell.Style.ForeColor = Color.Green;
                else
                    viewCell.Style.ForeColor = Color.Black;
            }

            for (int i = 0; i < m_ca.Layout; i++)
            {
                DataGridViewCell viewCell;
                viewCell = dataGridView.Rows[0].Cells[i];
                viewCell.Value = i.ToString();
                viewCell = dataGridView.Rows[i].Cells[0];
                viewCell.Value = i.ToString();

                if (m_ca.ShowStages)
                    dataGridView.Columns[i].Width = 80;
                else 
                    dataGridView.Columns[i].Width = 35;
            }


            StepNoTextBox.Text = m_ca.StepNo.ToString();
            NumSingelsTextBox.Text = m_ca.NumSingles.ToString();
            NumCouplesTextBox.Text = m_ca.NumCouples.ToString();

            int maximum_happiness = 99 * m_ca.Population / 2;
            TotalHappinessTextBox.Text = String.Format("{0} ({1:N0}%)", m_ca.TotalHappiness, ((double)m_ca.TotalHappiness / maximum_happiness) * 100.0);
        }

        private void TimerControl_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                m_ca.Step();
            }

            dump();
            m_ca.sanity_check();
        }

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            read_controls();
            m_ca.Initialize();
            dump();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            read_controls();
            TimerControl.Start();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            TimerControl.Stop();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            read_controls();

            m_ca.ShowStages = ShowStagesCheckBox.Checked;
            if (m_ca.ShowStages)
            {
                if (m_stage == 0)
                {
                    ShowStagesCheckBox.Enabled = false;
                    m_stage = Cell.NumOfStages - 1;
                }
                else
                {
                    m_stage--;
                    if (m_stage == 0)
                        ShowStagesCheckBox.Enabled = true;
                }
            }

            m_ca.Step();

            dump();
            m_ca.sanity_check();
        }

    }
}
