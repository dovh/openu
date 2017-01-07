﻿using System;
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

            dump();
        }

        void dump()
        {
            Source.Data data = Source.Data.GetInstance();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    PreferencesGridView.Rows[i].Cells[j + 1].Value = data.GetPreferences(i,j).ToString();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
