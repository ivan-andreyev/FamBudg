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
        public const string INCOME_QUERY_TYPE = "income"; // константа запроса на добавление дохода
        public const string CONSUMPTION_QUERY_TYPE = "consumption"; // константа запроса на добавление расхода

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
            if (checkInputs(CONSUMPTION_QUERY_TYPE))
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
            if (checkInputs(INCOME_QUERY_TYPE))
            {
                ClientCommands.addIncome(sum, cat, comment);
                refreshGrids(tabControl1.TabPages.IndexOf(tabControl1.SelectedTab));
            }
            else MessageBox.Show("Ошибка входных данных"); // добавить доход
        }

        public bool checkInputs(string inc) // проверка правильности ввода
        {
            double d = new double();
            int i = new int();
            string s = "";
            bool isGood = true;
            switch (inc)
            {
                case "income":
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
                    break;

                case "consumption":
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
                    break;

                default:
                    MessageBox.Show("Wrong query type.");
                    isGood = false;
                    break;
            }
            return isGood;
        }


        void refreshGrids(int n)
        {
            //MessageBox.Show(n.ToString());
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

        private void FamilyBudget_ResizeEnd(object sender, EventArgs e)
        {
            //this.Size.Width
            dataGridView1.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.6);
            dataGridView2.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.6);
        }

        private void FamilyBudget_Paint(object sender, PaintEventArgs e)
        {
            label7.Text = "Form Width: " + this.Size.Width.ToString() + " DataGrid Width: " + dataGridView1.Width.ToString();
            dataGridView1.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.6);
            dataGridView2.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // edit categories;
        }
    }
}
