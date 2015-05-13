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
        string nameField;
        public CategoriesEdit(FamilyBudget f)
        {
            nameField = f.nameField;
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
            bs = ClientCommands.refreshCategories(nameField);
            dataGridView1.DataSource = bs.DataSource;
        }

        private void CategoriesEdit_Paint(object sender, PaintEventArgs e)
        {
            //label3.Text = "Form Width: " + this.Size.Width.ToString() + " DataGrid Width: " + dataGridView1.Width.ToString();
            dataGridView1.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.5);
        }
    }
}
