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
    public partial class Reservations : Form
    {
        public Reservations()
        {
            InitializeComponent();
        }

        static string connection = @"Data Source=LAPTOP-94LQA6HK\SQLEXPRESS;Initial Catalog=AyuboDrive;Integrated Security=True";
        SqlConnection con = new SqlConnection(connection);
       
        string customer_id, name, c_number, address;
      
        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            txtaddress.Clear();
            txtCID.Clear();
            txtCname.Clear();
            txtCnum.Clear();
            txtreserveID.Clear();
            
        }
      
        //To reserve a vehicle
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {
                customer_id = txtCID.Text;
                name = txtCname.Text;
                c_number = txtCnum.Text;
                address = txtaddress.Text;
                string reserve_id = txtreserveID.Text;

                con.Open();
                string insert = "INSERT into Customer_Details values ('" + customer_id + "','" + name + "','" + c_number
                    + "','" + address + "','" + reserve_id + "')";
                if (MessageBox.Show("Are you sure you want to reserve?",
               "Confirmation", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question) == DialogResult.No)
                {
                    con.Close();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(insert, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Reservation_Hire rese = new Reservation_Hire();
            rese.Show();
            this.Hide();    
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            btnreserve.Visible = true;
            btnDone.Visible = false;
            panel3.Visible = true;
        }
        //To load data onto the data adapter
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
                panel4.Visible = true;
                panel3.Visible = true;
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * from Customer_Details", con);
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
        //To clear filled data in the text boxes
        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
        }

        //To fill data in to the textboxes from datagrid
        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            try
            {
                panel4.Visible = false;
                btnDone.Visible = true;
                btnreserve.Visible = false;


                txtCID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtCname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtCnum.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtaddress.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtreserveID.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To delete records from the table
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            try
            {
                customer_id = txtCID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                con.Open();
                string delete = "DELETE from Customer_Details where CID = ('" + customer_id + "')";
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

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To fill datagrid view
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            try
            {
                customer_id = txtsearch.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * from Customer_Details where CID = '" + customer_id
                    + "'", con);
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
        //To update data in the table
        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                customer_id = txtCID.Text;
                name = txtCname.Text;
                c_number = txtCnum.Text;
                address = txtaddress.Text;
                string resID = txtreserveID.Text;


                string Update = "UPDATE Customer_Details set CID = '" + customer_id + "',C_name = '" + name
                    + "',Contact_Number = '" + c_number + "', Address = '" + address + "', Reservation_ID = '" + resID
                    + "' where CID = '" + customer_id + "'";
                if (MessageBox.Show("Are you sure you want to update?",
               "Confirmation", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question) == DialogResult.No)
                {
                    con.Close();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(Update, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Reservations_Load_1(object sender, EventArgs e)
        {
            btnDone.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

       
        
    }
}
