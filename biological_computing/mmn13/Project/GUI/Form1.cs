using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
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
        }

        public Form1()
        {
            InitializeComponent();

            InitializeGrid(PreferencesGridView, 11, 10);
            InitializeGrid(NetworkGridView, 10, 10);

            // Initialize left preference col 
            DataGridViewCellStyle DefaultStyle = PreferencesGridView.DefaultCellStyle;
            DataGridViewCellStyle Style = new DataGridViewCellStyle();
            Style.Font = new Font(DefaultStyle.Font.FontFamily, DefaultStyle.Font.Size, FontStyle.Bold);
            for (int i = 0; i < 10; i++) {
                PreferencesGridView.Rows[i].Cells[0].Value = i.ToString();
                PreferencesGridView.Rows[i].Cells[0].Style.ApplyStyle(Style);
            }

            // m_network 
            m_Network = new Source.Network(10, 10);
            m_Network.Randomize();
            dump();
        }

        void dump()
        {
            Source.Preferences data = Source.Preferences.GetInstance();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    PreferencesGridView.Rows[y].Cells[x + 1].Value = data.GetPreferenceByIndex(y, x).ToString();

                    int value = m_Network.Neuron(x, y).Value;
                    NetworkGridView.Rows[y].Cells[x].Value = value.ToString(); // String.Format("{0:N}", value);
                }
            }
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            m_Network.UpdateWeights();
            m_Network.Step();
            dump();
        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            m_Network.Randomize();
            dump();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        
    }
}
