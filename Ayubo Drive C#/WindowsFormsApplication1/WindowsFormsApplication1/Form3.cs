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
    public partial class Package : Form
    {
        public Package()
        {
            InitializeComponent();
            fill_combo_box();     
        }

        SqlConnection con = new SqlConnection (@"Data Source=LAPTOP-94LQA6HK\SQLEXPRESS;Initial Catalog=AyuboDrive;Integrated Security=True");

        string p_id, p_type, v_type, V_model;
        int m_km, m_hour,p_price;
        
        private void label30_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void label29_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
            return;
        }
        private void label6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            btn3.Visible = false;
            btn2.Visible = true;
        }

        private void Package_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = false;

        }
        //To load details onto the datagrid from package details table
        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;

            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * from Package_Details", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            txtPID.Clear();
            cmbptype.ResetText();
            txtVmodel.Clear();
            cmbvtype.ResetText();
            txtMhour.Clear();
            txtMkm.Clear();
            txtprice.Clear();
        }
        //To fill data into package details form from the datagrid 
        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            try
            {
                panel3.Visible = true;
                panel1.Visible = false;
                btn3.Visible = true;
                btn2.Visible = false;

                txtPID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cmbptype.SelectedItem = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cmbvtype.SelectedItem = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtVmodel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtMhour.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtMkm.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtprice.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To filldata into the package details table
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
            p_id = txtPID.Text;
            p_type = cmbptype.SelectedItem.ToString();
            v_type = cmbvtype.SelectedValue.ToString();
            V_model = txtVmodel.Text;
            m_km = int.Parse(txtMkm.Text);
            m_hour = int.Parse(txtMhour.Text);
            p_price = int.Parse(txtprice.Text);

            con.Open();

            string insert = "INSERT into Package_Details values ('" + p_id + "','" + p_type 
                + "','" + v_type + "','" + V_model + "','" + m_hour + "','" + m_km + "','"+p_price+"')";
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
        //To delete records from package details table
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            try
            {
            p_id = txtPID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            con.Open();
            string delete = "DELETE from Package_Details where Package_ID = ('" + p_id + "')";
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
        //To update records in package details tables
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {
                p_id = txtPID.Text;
                p_type = cmbptype.SelectedItem.ToString();
                v_type = cmbvtype.SelectedValue.ToString();
                V_model = txtVmodel.Text;
                m_km = int.Parse(txtMkm.Text);
                m_hour = int.Parse(txtMhour.Text);
                p_price = int.Parse(txtprice.Text);

                con.Open();

                string update = "UPDATE Package_Details set P_Type = '" + p_type + "', V_Type = '" + v_type
                    + "', V_Model = '" + V_model + "', Max_Hours = '" + m_hour + "', Max_Km = '" + m_km + "', P_Price = '" + p_price + "' where Package_ID = '" + p_id + "'";
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

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            p_id = txtsearch.Text;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * from Package_Details where Package_ID = '" + p_id
                + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        // To fill vehicle type combobox
        private void fill_combo_box()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT distinct V_Type from Vehicle_Details", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbvtype.DataSource = dt;
            cmbvtype.DisplayMember = "V_Type";
            cmbvtype.ValueMember = "V_Type";
        }

    }
}

