using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FamBudg;

namespace FamBudg
{
    public partial class CategoriesEdit : Form
    {
        BindingSource bs = new BindingSource();

        public CategoriesEdit()
        {
            InitializeComponent();
            refreshCategoriesGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientCommands.addCategory(textBox1.Text, textBox2.Text);
            refreshCategoriesGrid();
        }

        void refreshCategoriesGrid()
        {
            bs = ClientCommands.refreshCategories();
            dataGridView1.DataSource = bs.DataSource;
        }
    }
}
