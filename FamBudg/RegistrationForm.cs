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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkInputs())
            {
                ClientCommands.sendRequest("registration" + "login" + textBox1.Text + "password" + textBox2);
            }
            else MessageBox.Show("Проверьте правильность введённых данных.");
        }

        public bool checkInputs()
        {
            bool valid = true;
            string s = ""; 

            if (!Object.ReferenceEquals(s.GetType(), textBox1.Text.GetType())) /* можно ли привести к string */
            {
                valid = false;
            }
            if (!Object.ReferenceEquals(s.GetType(), textBox2.Text.GetType())) /* можно ли привести к string */
            {
                valid = false;
            }

            return valid;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
                button1.Enabled = false;
            else button1.Enabled = true;
        }
    }
}
