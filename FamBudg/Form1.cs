using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FamBudg
{
    public partial class FamilyBudget : Form
    {
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
            this.testcTableAdapter.Fill(this.fambudgetDataSet.testc);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(checkInputs())ClientCommands.addСonsumption(); // добавить расход
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientCommands.addIncome(); // добавить доход
        }

        public bool checkInputs() // проверка правильности ввода
        {
            /*donothing*/
            return true;
        }
    }
}
