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
    public partial class Authorise : Form
    {
        public Authorise()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "пароль") textBox1.Text = "";
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            if (comboBox1.Text == "логин") comboBox1.Text = "";
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") comboBox1.Text = "логин";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "") textBox1.Text = "пароль";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientCommands.sendRequest();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* открыть форму регистрации */
            /*  */
        }
    }
}
