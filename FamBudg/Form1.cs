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
        public BindingSource bs3 = new BindingSource();

        public string nameField = "Имя категории";
        public bool inc = false;

        // проверка на авторизованность в программе
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
            // обновление таблицы на текущей вкладке
            refreshGrids(tabControl1.TabPages.IndexOf(tabControl1.SelectedTab));
            refreshCategories();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double sum = Convert.ToDouble(textBox1.Text);
            string cat = comboBox1.Text;
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
            string cat = comboBox2.Text;
            string comment = textBox3.Text;
            if (checkInputs(INCOME_QUERY_TYPE)) // если входные данные корректны
            {
                ClientCommands.addIncome(sum, cat, comment); // выполнить запрос
                refreshGrids(tabControl1.TabPages.IndexOf(tabControl1.SelectedTab)); // и обновить таблицу
            }
            else MessageBox.Show("Ошибка входных данных"); // добавить доход
        }

        public bool checkInputs(string inc) // проверка правильности ввода
        {
            double d = new double(); // образец типа double
            int i = new int(); // образец типа int
            string s = ""; // образец типа string
            bool isGood = true; // возвращаемая переменная, флаг корректности
            switch (inc)
            {
                case "income":
                    if (!Object.ReferenceEquals(d.GetType(), Convert.ToDouble(textBox4.Text).GetType())) /* можно ли привести к double */
                    {
                        isGood = false;
                    }
                    if (!Object.ReferenceEquals(s.GetType(), comboBox2.Text.GetType())) /* можно ли привести к string */
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
                    if (!Object.ReferenceEquals(s.GetType(), comboBox1.Text.GetType())) /* можно ли привести к string */
                    {
                        isGood = false;
                    }
                    if (!Object.ReferenceEquals(s.GetType(), textBox2.Text.GetType())) /* можно ли привести к string */
                    {
                        isGood = false;
                    }
                    break;

                default:
                    MessageBox.Show("Неправильный тип запроса.");
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
                    bs2 = ClientCommands.refreshIncomes();
                    dataGridView2.DataSource = bs2.DataSource;
                    bs1 = ClientCommands.refreshCosts();
                    dataGridView1.DataSource = bs1.DataSource;
                    break;
            }
        }

        public void refreshCategories()
        {
            bs3 = ClientCommands.refreshCategories(nameField);
            comboBox1.DataSource = comboBox2.DataSource = bs3.DataSource;
            comboBox1.DisplayMember = comboBox2.DisplayMember = nameField;
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

        // автовыделение при фокусе инпутов
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
            // при изменении размеров формы, масштабируем dataGridView
            dataGridView1.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.6);
            dataGridView2.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.6);
        }

        private void FamilyBudget_Paint(object sender, PaintEventArgs e)
        {
            // при отрисовке формы, масштабируем dataGridView
            //label7.Text = "Form Width: " + this.Size.Width.ToString() + " DataGrid Width: " + dataGridView1.Width.ToString();
            dataGridView1.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.6);
            dataGridView2.Width = Convert.ToInt32(Convert.ToDouble(this.Size.Width) * 0.6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // edit categories;
            CategoriesEdit f = new CategoriesEdit(this);
            f.ShowDialog();
            refreshCategories();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options o = new Options(this);
            o.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // edit categories;
            CategoriesEdit f = new CategoriesEdit(this);
            f.ShowDialog();
            refreshCategories();
        }
    }
}
