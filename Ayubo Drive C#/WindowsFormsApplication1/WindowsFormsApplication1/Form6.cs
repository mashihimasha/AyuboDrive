using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string user_name = txtusername.Text;
            string password = txtpassword.Text;
             
            if( user_name == "AyuboDrive" && password == "Ayubo123")
            {
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Invalid Username and Password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusername.Clear();
                txtpassword.Clear();
                
            }


        }

        private void txtusername_Enter(object sender, EventArgs e)
        {
            if (txtusername.Text == "Username")
            {
                txtusername.Text = "";
                txtusername.ForeColor = Color.Black;
            }
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtpassword.Focus();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bunifuProgressBar1.Value < 100)
            {
                bunifuProgressBar1.Value = bunifuProgressBar1.Value + 2;
            }
            else
            {
                timer1.Enabled = false;
                Home h = new Home();
                h.Show();
                this.Hide();
            }
        }

        private void bunifuCheckbox1_OnChange_1(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked == true)
            {
                txtpassword.PasswordChar = '\0';
            }
            if (bunifuCheckbox1.Checked == false)
            {
                txtpassword.PasswordChar = '*';
            }
                
        }

        private void txtusername_Leave(object sender, EventArgs e)
        {
            if (txtusername.Text == "")
            {
                txtusername.Text = "Username";
                txtusername.ForeColor = Color.DarkGray;
            }
        }

        private void txtpassword_Enter(object sender, EventArgs e)
        {
            if (txtpassword.Text == "Password")
            {
                txtpassword.Text = "";
                txtpassword.ForeColor = Color.Black;
                txtpassword.PasswordChar = '*';
                    
                    
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {
            if (txtpassword.Text == "")
            {
                txtpassword.Text = "Password";
                txtpassword.ForeColor = Color.DarkGray;
                txtpassword.PasswordChar = '\0';
            }

        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                bunifuThinButton21.Focus();
            }
        }

        private void label29_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
    }
}
