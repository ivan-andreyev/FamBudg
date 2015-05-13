using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;

namespace FamBudg
{
    public partial class FamilyBudget : Form
    {
        private BindingSource bs1 = new BindingSource();
        private BindingSource bs2 = new BindingSource();
        public bool inc = false;
        public bool isAuthorised = ClientCommands.sendRequest("isAuthorised");
        public FamilyBudget()
        {
            InitializeComponent();
            if (!isAuthorised) // если не авторизован
            {
                this.Hide(); // скрыть главную форму
                Authorise f = new Authorise(); 
                f.ShowDialog(); // показать форму авторизации
                this.Show(); // после закрытия диалога авторизации, показать главную форму
            }
        }

        private void FamilyBudget_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fambudgetDataSet.testc' table. You can move, or remove it, as needed.
            refreshGrids(tabControl1.TabPages.IndexOf(tabControl1.SelectedTab));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double sum = Convert.ToDouble(textBox1.Text);
            int cat = Convert.ToInt32(comboBox1.Text);
            string comment = textBox2.Text;
            if (checkInputs(false))
            {
                ClientCommands.addСonsumption(sum, cat, comment); // добавить расход
                refreshGrids(tabControl1.TabPages.IndexOf(tabControl1.SelectedTab));
            }
            else MessageBox.Show("Ошибка входных данных"); // добавить доход
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double sum = Convert.ToDouble(textBox4.Text);
            int cat = Convert.ToInt32(comboBox2.Text);
            string comment = textBox3.Text;
            if (checkInputs(true))
            {
                ClientCommands.addIncome(sum, cat, comment);
                refreshGrids(tabControl1.TabPages.IndexOf(tabControl1.SelectedTab));
            }
            else MessageBox.Show("Ошибка входных данных"); // добавить доход
        }

        public bool checkInputs(bool inc) // проверка правильности ввода
        {
            double d = new double();
            int i = new int();
            string s = "";
            bool isGood = true;
            if (inc)
            {
                if (!Object.ReferenceEquals(d.GetType(), Convert.ToDouble(textBox4.Text).GetType())) /* можно ли привести к double */
                {
                    isGood = false;
                }
                if (!Object.ReferenceEquals(i.GetType(), Convert.ToInt32(comboBox2.Text).GetType())) /* можно ли привести к int */
                {
                    isGood = false;
                }
                if (!Object.ReferenceEquals(s.GetType(), textBox3.Text.GetType())) /* можно ли привести к string */
                {
                    isGood = false;
                }
            }
            else
            {
                if (!Object.ReferenceEquals(d.GetType(), Convert.ToDouble(textBox1.Text).GetType())) /* можно ли привести к double */
                {
                    isGood = false;
                }
                if (!Object.ReferenceEquals(i.GetType(), Convert.ToInt32(comboBox1.Text).GetType())) /* можно ли привести к int */
                {
                    isGood = false;
                }
                if (!Object.ReferenceEquals(s.GetType(), textBox2.Text.GetType())) /* можно ли привести к string */
                {
                    isGood = false;
                }
            }
            return isGood;
        }


        void refreshGrids(int n)
        {
            MessageBox.Show(n.ToString());
            switch (n)
            {
                case 0:
                    
                    bs1 = ClientCommands.refreshCosts();
                    dataGridView1.DataSource = bs1.DataSource;
                    break;
                case 1:
                    bs2 = ClientCommands.refreshIncomes();
                    dataGridView2.DataSource = bs2.DataSource;
                    break;
                default:
                    bs1 = ClientCommands.refreshCosts();
                    dataGridView1.DataSource = bs1.DataSource;
                    bs2 = ClientCommands.refreshIncomes();
                    dataGridView2.DataSource = bs2.DataSource;
                    break;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FamilyBudget_Activated(object sender, EventArgs e)
        {
            //refreshGrids(tabControl1.TabIndex);
            //dataGridView1
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGrids(tabControl1.TabPages.IndexOf(tabControl1.SelectedTab));
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            comboBox1.SelectAll();
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.SelectAll();
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            comboBox2.SelectAll();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }
    }
}
