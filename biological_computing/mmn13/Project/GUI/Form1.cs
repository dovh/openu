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
        public Form1()
        {
            InitializeComponent();

            FormatGrid(PreferencesGridView, 10);
            FormatGrid(NetworkGridView, 10);
        }

        void FormatGrid(DataGridView Grid, int size)
        {
            Grid.RowHeadersVisible = false;
            Grid.ColumnHeadersVisible = false;
            for (int i = 0; i < size; i++)
            {
                Grid.Columns.Add("col" + i.ToString(), i.ToString());
                Grid.Columns[i].Width = Grid.Width / size;
            }
            Grid.Rows.Add(size);
            for (int i = 0; i < size; i++)
                Grid.Rows[i].Height = Grid.Height / size;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
