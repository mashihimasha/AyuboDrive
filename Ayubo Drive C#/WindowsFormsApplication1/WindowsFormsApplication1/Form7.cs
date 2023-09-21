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
    public partial class Rates_Rent : Form
    {
        public Rates_Rent()
        {
            InitializeComponent();
            fill_combo_box();
            fill_combo_box1();
        }

        static string connection = @"Data Source=LAPTOP-94LQA6HK\SQLEXPRESS;Initial Catalog=AyuboDrive;Integrated Security=True";
        SqlConnection con = new SqlConnection(connection);

        string rate_id, v_num,v_model;
        int driver_charges, per_day_rate, per_week_rate, per_month_rate;

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
            if (txtpdr.Text == "Per day rent")
            {
                txtpdr.Text = "";
                txtpdr.ForeColor = Color.Black;
            }
        }

        private void txtpdr_Leave(object sender, EventArgs e)
        {
            if (txtpdr.Text == "")
            {
                txtpdr.Text = "Per day rent";
                txtpdr.ForeColor = Color.DarkGray;
            }
        }

        private void txtpwr_Enter(object sender, EventArgs e)
        {
            if (txtpwr.Text == "Per week rent")
            {
                txtpwr.Text = "";
                txtpwr.ForeColor = Color.Black;
            }
        }

        private void txtpwr_Leave(object sender, EventArgs e)
        {
            if (txtpwr.Text == "")
            {
                txtpwr.Text = "Per week rent";
                txtpwr.ForeColor = Color.DarkGray;
            }
        }

        private void txtpmr_Enter(object sender, EventArgs e)
        {
            if (txtpmr.Text == "Per month rent")
            {
                txtpmr.Text = "";
                txtpmr.ForeColor = Color.Black;
            }
        }

        private void txtpmr_Leave(object sender, EventArgs e)
        {
            if (txtpmr.Text == "")
            {
                txtpmr.Text = "Per month rent";
                txtpmr.ForeColor = Color.DarkGray;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel4.Visible = false;
            btndone.Visible = false;
            btnsave.Visible = true;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel3.Visible = false;
            btndone.Visible = true;
            btnsave.Visible = false;

            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from Rent_Rates", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void Rates_Rent_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            txtpdr.Clear();
            txtpmr.Clear();
            txtpwr.Clear();
            txtRID.Clear();
            txtDcharges.Clear();
            cmbVnum.ResetText();
            cmbVtype.ResetText();            
        }
        //To insert rent rates into the table
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
            rate_id = txtRID.Text;
            v_num = cmbVnum.SelectedValue.ToString();
            v_model = cmbVtype.SelectedValue.ToString();
            driver_charges = int.Parse(txtDcharges.Text);
            per_day_rate = int.Parse(txtpdr.Text);
            per_week_rate = int.Parse(txtpwr.Text);
            per_month_rate = int.Parse(txtpmr.Text);


            con.Open();

            string insert = "INSERT into Rent_Rates values ('" + rate_id + "','" + per_day_rate + "','" + per_week_rate 
                + "','" + per_month_rate + "','" + driver_charges + "','" + v_num + "','"+v_model+"')";
            if (MessageBox.Show("Are you sure you want to add new record?",
               "Confirmation", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question) == DialogResult.No)
            {
                con.Close();
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
        //To update rent rates into the table
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        { 
            try
            {
            rate_id = txtRID.Text;
            v_num = cmbVnum.SelectedValue.ToString();
            v_model = cmbVtype.SelectedValue.ToString();
            driver_charges = int.Parse(txtDcharges.Text);
            per_day_rate = int.Parse(txtpdr.Text);
            per_week_rate = int.Parse(txtpwr.Text);
            per_month_rate = int.Parse(txtpmr.Text);

            con.Open();

            string update = "UPDATE Rent_Rates set per_day = '" + per_day_rate + "', per_week = '" + per_week_rate 
                + "', per_month = '" + per_month_rate + "', per_day_driver = '" + driver_charges + "', V_No= '" + v_num 
                + "',V_Model = '"+v_model+"' where RRate_ID = '" + rate_id + "'";
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

        private void bunifuFlatButton6_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel4.Visible = false;
            btndone.Visible = true;
            btnsave.Visible = false;

            txtRID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtpdr.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtpwr.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtpmr.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtDcharges.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbVnum.SelectedValue = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cmbVtype.SelectedValue = dataGridView1.CurrentRow.Cells[6].Value.ToString();

        }
        //To delete records from rental rates table
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            try
            {
            rate_id = txtRID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            con.Open();
            string delete = "DELETE from Rent_Rates where RRate_ID = ('" + rate_id + "')";
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

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            rate_id = txtsearch.Text;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Rent_Rates where RRate_ID = '" + rate_id + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        //To view all details from the rent rates table on the datagrid
        private void fill_combo_box()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select V_No from Vehicle_Details", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbVnum.DataSource = dt;
            cmbVnum.DisplayMember = "V_No";
            cmbVnum.ValueMember = "V_No";  
        }
        //To fill vehicle models combobox
        private void fill_combo_box1()
        {
            SqlDataAdapter daa = new SqlDataAdapter("Select V_Model from Vehicle_Details", con);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            cmbVtype.DataSource = dtt;
            cmbVtype.DisplayMember = "V_Model";
            cmbVtype.ValueMember = "V_Model";
        }
    }
}

