using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Rates_hire : Form
    {
        public Rates_hire()
        {
            InitializeComponent();
            fill_combo_box1();
            fill_combo_box2();
        }
        static string connection = @"Data Source=LAPTOP-94LQA6HK\SQLEXPRESS;Initial Catalog=AyuboDrive;Integrated Security=True";
        SqlConnection con = new SqlConnection(connection);

        string rate_id, v_model,package_id;
        int d_o_charges, per_kmr, per_hourr, veh_parking;     

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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Rates r = new Rates();
            r.Show();
            this.Hide();
            return;
        }

        private void bunifuTextBox1_Enter(object sender, EventArgs e)
        {
            if (txtpkmr.Text == "Per Km Rate")
            {
                txtpkmr.Text = "";
                txtpkmr.ForeColor = Color.Black;
                
            }
        }

        private void txtpkmr_Leave(object sender, EventArgs e)
        {
            if (txtpkmr.Text == "")
            {
                txtpkmr.Text = "Per Km Rate";
                txtpkmr.ForeColor = Color.DarkGray;

            }
        }

        private void txtphr_Enter(object sender, EventArgs e)
        {
            if (txtphr.Text == "Per Hour Rate")
            {
                txtphr.Text = "";
                txtphr.ForeColor = Color.Black;

            }
        }

        private void txtphr_Leave(object sender, EventArgs e)
        {
            if (txtphr.Text == "")
            {
                txtphr.Text = "Per Hour Rate";
                txtphr.ForeColor = Color.DarkGray;

            }
        }

        private void txtvpr_Enter(object sender, EventArgs e)
        {
            if (txtvpr.Text == "Vehicle Parking Rate")
            {
                txtvpr.Text = "";
                txtvpr.ForeColor = Color.Black;

            }
        }

        private void txtvpr_Leave(object sender, EventArgs e)
        {
            if (txtvpr.Text == "")
            {
                txtvpr.Text = "Vehicle Parking Rate";
                txtvpr.ForeColor = Color.DarkGray;

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            btndone.Visible = false;
            btnsave.Visible = true;
        }
        //used to load data onto datagrid
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
            panel1.Visible = true;

            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * from Hire_Rates", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Rates_hire_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = true;
        }
        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
        }
        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            txtRID.Clear();
            txtONcharges.Clear();
            txtpkmr.Clear();
            txtphr.Clear();
            txtvpr.Clear();            
            cmbvmodel.ResetText();
            cmbpackage.ResetText();

        }
        //To load a selected value from the table onto the datagrid
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            try
            {
            rate_id = txtsearch.Text;
            
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * from Hire_Rates where HRate_ID = '" + rate_id + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To insert data into the hire rates table
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {
            rate_id = txtRID.Text;            
            d_o_charges = int.Parse(txtONcharges.Text);
            per_kmr =int.Parse(txtpkmr.Text);
            per_hourr = int.Parse(txtphr.Text);
            veh_parking = int.Parse(txtvpr.Text);
            package_id = cmbpackage.SelectedValue.ToString();
            v_model = cmbvmodel.SelectedValue.ToString();

            con.Open();

            string insert = "INSERT into Hire_Rates values ('" + rate_id + "','" + per_kmr + "','" + per_hourr 
                + "','" + d_o_charges + "','" + veh_parking + "','"+v_model+"','"+package_id+"')";
            if (MessageBox.Show("Are you sure you want to save?",
               "Confirmation", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();            
                MessageBox.Show("Record added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); 
               
            }
            con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To delete records from the hire rates table
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            try
            {
            rate_id = txtRID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
            con.Open();
            string delete = "DELETE from Hire_Rates where HRate_ID = ('" + rate_id + "')";
            if (MessageBox.Show("Are you sure you want to delete?",
               "Confirmation", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question) == DialogResult.No)
            {
                con.Close();
            }
            else
            {
                SqlCommand cmd = new SqlCommand(delete, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton6_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            btndone.Visible = true;            
            btnsave.Visible = false;

            txtRID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtpkmr.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtphr.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtONcharges.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtvpr.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbvmodel.SelectedValue = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cmbpackage.SelectedValue = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            
        }
        //To update data into the hire rates table
        private void btndone_Click(object sender, EventArgs e)
        {
            try
            {
            rate_id = txtRID.Text;
            d_o_charges = int.Parse(txtONcharges.Text);
            per_kmr = int.Parse(txtpkmr.Text);
            per_hourr = int.Parse(txtphr.Text);
            veh_parking = int.Parse(txtvpr.Text);
            package_id = cmbpackage.SelectedValue.ToString();
            v_model = cmbvmodel.SelectedValue.ToString();
            
            con.Open();

            string update = "UPDATE Hire_Rates set Extra_per_Km = '" + per_kmr + "', Extra_per_hour= '" + per_hourr
                + "', Driver_per_night = '" + d_o_charges + "', V_parking = '" + veh_parking + "', V_Model = '" + v_model
                + "', Package_ID = '" + package_id + "' where HRate_ID = '" + rate_id + "'";
            if (MessageBox.Show("Are you sure you want to update?",
               "Confirmation", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question) == DialogResult.No)
            {
                con.Close();
            }
            else
            {
                SqlCommand cmd = new SqlCommand(update, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To fill vehicle model combobox
        private void fill_combo_box1()
        {
            SqlDataAdapter daa = new SqlDataAdapter("SELECT V_model from Package_Details", con);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            cmbvmodel.DataSource = dtt;
            cmbvmodel.DisplayMember = "V_Model";
            cmbvmodel.ValueMember = "V_Model";
        }
        //To fill package id combobox
        private void fill_combo_box2()
        {
            SqlDataAdapter dada = new SqlDataAdapter("SELECT Package_ID from Package_Details", con);
            DataTable dtdt = new DataTable();
            dada.Fill(dtdt);
            cmbpackage.DataSource = dtdt;
            cmbpackage.DisplayMember = "Package_ID";
            cmbpackage.ValueMember = "Package_ID";
        }
    }
}

