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
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();
        }

        static string connection = @"Data Source=LAPTOP-94LQA6HK\SQLEXPRESS;Initial Catalog=AyuboDrive;Integrated Security=True";
        SqlConnection con = new SqlConnection(connection);

        string driver_id,name,c_number,address;

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

        private void label6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void Drivers_Load(object sender, EventArgs e)
        {

            panel1.Visible = false;
            panel3.Visible = false;
        }
        //To load table records on to the  datagrid
        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;


            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from Driver_Details", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            btnsave.Visible = true;
            btndone.Visible = false;
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
        }

        //clear
        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            txtDID.Clear();
            txtDname.Clear();
            txtCnum.Clear();
            txtAddress.Clear();
        }

        //To save details into the driver details table
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
            driver_id = txtDID.Text;
            name = txtDname.Text;
            c_number = txtCnum.Text;
            address = txtAddress.Text;

            con.Open();

            string insert = "Insert into Driver_Details values ('" + driver_id + "','" + name + "','" + c_number 
                + "','" + address + "')";
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
        //To update Driver_Details table
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {
            driver_id = txtDID.Text;
            name = txtDname.Text;
            c_number = txtCnum.Text;
            address = txtAddress.Text;

            con.Open();

            string update = "UPDATE Driver_Details set D_Name = '" + name + "', Contact_Number = '" + c_number 
                + "', Address = '" + address + "' where Driver_ID = '" + driver_id + "'";
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

        //search
        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            driver_id = txtsearch.Text;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * from Driver_Details where Driver_ID = '" + driver_id + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        //To delete records from the driver details table
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            try
            {
            driver_id = txtDID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            con.Open();
            string delete = "DELETE from Driver_Details where Driver_ID = ('" + driver_id+ "')";
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

        //To fill data on to the driver deatils form
        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            btndone.Visible = true;
            btnsave.Visible = false;

            txtDID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtDname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtCnum.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }
    }
}

  
    

