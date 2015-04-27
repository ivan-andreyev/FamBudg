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
            refreshGrids();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double sum = Convert.ToDouble(textBox1.Text);
            int cat = Convert.ToInt32(comboBox1.Text);
            string comment = textBox2.Text;
            if (checkInputs(false)) ClientCommands.addСonsumption(sum, cat, comment); // добавить расход
            else MessageBox.Show("Ошибка входных данных"); // добавить доход
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double sum = Convert.ToDouble(textBox4.Text);
            int cat = Convert.ToInt32(comboBox2.Text);
            string comment = textBox3.Text;
            if (checkInputs(true)) ClientCommands.addIncome(sum, cat, comment); 
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


        void refreshGrids()
        {
            //refreshCosts();
            //refreshIncomes();
            //dataGridView1.DataSource;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
