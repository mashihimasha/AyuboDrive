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
    public partial class Reservation_Hire : Form
    {
        public Reservation_Hire()
        {
            InitializeComponent();
            fill_combo_box();
            fill_combo_box1();
        }

        static string connection = @"Data Source=LAPTOP-94LQA6HK\SQLEXPRESS;Initial Catalog=AyuboDrive;Integrated Security=True";
        SqlConnection con = new SqlConnection(connection);


        DateTime end_time, start_time;
        string reserve_id, driver_stat, tour_type,v_model,p_type;
        int days,hours;
        //To clear filled data in the form
        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            txtreserveID.Clear();
            dtpstime.ResetText();
            dtpetime.ResetText();
            cmbpackage.ResetText();
            rbrent.Checked = false;
            rbrentwdriver.Checked = false;
            cmbvmodel.ResetText();
        }
        //To insert data into the reservation details table
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {
                end_time = dtpetime.Value;
                start_time = dtpstime.Value;
                reserve_id = txtreserveID.Text;
                v_model = cmbvmodel.SelectedValue.ToString();
                p_type = cmbpackage.SelectedValue.ToString();

                TimeSpan ts = end_time.Date - start_time.Date;
                int total_days = ts.Days + 1;
                days = total_days;
                hours = days * 24;

                con.Open();
                string insert = "INSERT into Reservation_Details values ('" + reserve_id + "','" + tour_type + "','" + days
                    + "','" + 0.0 + "','" + hours + "','" + driver_stat + "','" + p_type + "','" + v_model + "')";
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

        private void btnhire_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel5.Visible = true;
            tour_type = "Hire";
            driver_stat = "Yes";
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel5.Visible = false;
            panel3.Visible = false;
            btnDone.Visible = false;
        }

        private void btnrent_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel4.Visible = true;
            tour_type = "Rent";
        }

        private void rbrent_CheckedChanged(object sender, EventArgs e)
        {
            driver_stat = "No";
        }

        private void rbrentwdriver_CheckedChanged(object sender, EventArgs e)
        {
            driver_stat = "Yes";
        }
        //To fill package type combobox
        private void fill_combo_box()
        {
            SqlDataAdapter dada =
                new SqlDataAdapter("SELECT distinct P_Type from Package_Details", con);
            DataTable dtdt = new DataTable();
            dada.Fill(dtdt);
            cmbpackage.DataSource = dtdt;
            cmbpackage.DisplayMember = "P_Type";
            cmbpackage.ValueMember = "P_Type";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }
        //To fill vehicle model combobox
        private void fill_combo_box1()
        {  
            SqlDataAdapter da =
                new SqlDataAdapter("SELECT distinct V_Model from Package_Details", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbvmodel.DataSource = dt;
            cmbvmodel.DisplayMember = "V_Model";
            cmbvmodel.ValueMember = "V_Model";
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Reservations re = new Reservations();
            re.Show();
            this.Hide();
        }
        //To fill data onto the datagrid to view reservation details
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                panel3.Visible = true;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * from Reservation_Details", con);
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
        //To load data onto the reservation details form using datagrid
        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            
            panel3.Visible = false;
            btnDone.Visible = true;
            btnreserve.Visible = false;

            txtreserveID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cmbpackage.SelectedValue = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            cmbvmodel.SelectedValue = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            
        }
        //To update the table reservation details
        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                reserve_id = txtreserveID.Text;
                v_model = cmbvmodel.SelectedValue.ToString();
                p_type = cmbpackage.SelectedValue.ToString();

                string Update = "UPDATE Reservation_Details set Tour_Type = '"+tour_type+"',Number_Days = '" +
                    days + "',Number_Km='" + 0.0 + "',Number_Hours='" + hours + "',Driver_Parameter='" + driver_stat + "',Package_Type='" + p_type + "',V_Model='" + v_model + "' where Reservation_ID = '" + reserve_id + "'";
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
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
        
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            try
            {
                reserve_id = txtreserveID.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * from Reservation_Details where Reservation_ID = '" + reserve_id + "'", con);
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
        //To delete records from reservation details table
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            try
            {
                reserve_id = txtreserveID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                con.Open();
                string delete = "DELETE from Reservation_Details where Reservation_ID = ('" + reserve_id + "')";
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

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            txtreserveID.Clear();
            cmbpackage.ResetText();
            cmbvmodel.ResetText();
        }
        }
    }

