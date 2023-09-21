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
    public partial class Rates : Form
    {
        public Rates()
        {
            InitializeComponent();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label29_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Home H = new Home();
            H.Show();
            this.Hide();
            return;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Rates_hire rh = new Rates_hire();
            rh.Show();
            this.Hide();
            return;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Rates_Rent rr = new Rates_Rent();
            rr.Show();
            this.Hide();
            return;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
    }
}
