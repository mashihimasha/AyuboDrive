using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Vehicles : Form
    {
        public Vehicles()
        {
            InitializeComponent();
        }


        static string connection = @"Data Source=LAPTOP-94LQA6HK\SQLEXPRESS;Initial Catalog=AyuboDrive;Integrated Security=True";
        SqlConnection con = new SqlConnection(connection);

        string v_num,v_type,v_model;

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            this.Close();
        }

        private void label29_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label29_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            btndone.Visible = false;
            btnsave.Visible = true;
        }
        //To load data on to the datagrid
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel3.Visible = false;

            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from Vehicle_Details", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = false;
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            txtVnum.Clear();
            txtVmodel.Clear();
            txtVtype.Clear();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
        }
        //To save data into the vehicle details table
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
            v_num = txtVnum.Text;
            v_type = txtVtype.Text;
            v_model = txtVmodel.Text;

            con.Open();

            string insert = "INSERT into Vehicle_Details values ('" + v_num+ "','" +v_type + "','" + v_model + "')";
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
        //To update records into the vehicle details table
        private void btndone_Click(object sender, EventArgs e)
        {
            try
            {
            v_num = txtVnum.Text;
            v_type = txtVtype.Text;
            v_model = txtVmodel.Text;

            con.Open();

            string update = "UPDATE Vehicle_Details set V_Type = '" + v_type + "', V_Model = '" +v_model
                + "' where V_No = '" + v_num+ "'";
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
        //To select specific records from the datagrid 
        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            v_num = txtsearch.Text;

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Vehicle_Details where V_No= '" + v_num + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        //To delete records from vehicle details
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            try
            {

            v_num = txtVnum.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
            con.Open();
            string delete = "DELETE from Vehicle_Details where V_No = ('" + v_num+ "')";
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

            txtVnum.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtVmodel.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtVtype.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        }

        
    }

