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
    public partial class Home : Form
    {
        public Home()
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Payments rh = new Payments();
            rh.Show();
            this.Hide();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            Rates r = new Rates();
            r.Show();
            this.Hide();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Vehicles v = new Vehicles();
            v.Show();
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Package p = new Package();
            p.Show();
            this.Hide();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            Drivers d = new Drivers();
            d.Show();
            this.Hide();
        }

        private void label29_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            Reservations r = new Reservations();
            r.Show();
            this.Hide();
        }
    }
}
