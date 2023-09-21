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
    public partial class Payments : Form
    {
        public Payments()
        {
            InitializeComponent();
            fill_combo_box();
            fill_combo_box1();
            fill_combo_box2();
            fill_combo_box3();
        }
        static string connection = @"Data Source=LAPTOP-94LQA6HK\SQLEXPRESS;Initial Catalog=AyuboDrive;Integrated Security=True";
        SqlConnection con = new SqlConnection(connection);

        double driver_charge, max_km, max_hour, extra_hour_charge, distance, extra_km_charge, day_tour_charge
            , long_tour_charge;
        string tour_type, package_id, v_model, p_id, c_id, payment_type;
        int basic_charge, total;

        private void label30_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label29_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
            return;
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel1.Visible = false;
            tour_type = "Rent";
        }

        private void btnHire_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel4.Visible = false;
        }

        private void Rent_Hire_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel4.Visible = false;
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel1.Visible = false;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel1.Visible = false;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            txtbasiccharger.Clear();
            txtdrivercharge.Clear();
            txttotalcharge.Clear();
            txtstart.Clear();
            txtend.Clear();
            cmbmodel.ResetText();
            cmbV_No.ResetText();
            dtp1.ResetText();
            dtp2.ResetText();
            rbrent.Checked = false;
            rbrentwdriver.Checked = false;
        }
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            txtwaitingcharge.Clear();
            txtonscharge.Clear();
            txtendkmh.Clear();
            txtstartkmh.Clear();
            txttotal.Clear();
            txtextrakm.Clear();
            txtbasicchargeh.Clear();
            dtpetime.ResetText();
            dtpstime.ResetText();
            cmbpackage.ResetText();
            cmbVmodel.ResetText();
        }
        private void bunifuFlatButton13_Click(object sender, EventArgs e)
        {
            txtPID.Clear();
            txtCID.Clear();
            cmbpaytype.ResetText();
        }
        //To calculate basic hire charge
        public double basic_hire(string package_id, DateTime start_time, DateTime end_time, int start_km, int end_km)
        {
            double basic_charge = 0.0;
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Package_Details where Package_ID=@001", con);
                cmd.Parameters.AddWithValue("@001", package_id);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    double p_price = double.Parse(r["P_Price"].ToString());
                    max_km = double.Parse(r["Max_Km"].ToString());
                    max_hour = double.Parse(r["Max_Hours"].ToString());

                    TimeSpan tsh = end_time.Date - start_time.Date;
                    int total_days = tsh.Days;
                    if (total_days > 0)
                    {
                        basic_charge = p_price * total_days;
                    }
                    else
                    {
                        basic_charge = p_price;
                    }

                    con.Close();

                    if (total_days > 2)
                    {

                        double long_tour_extra_charge = long_tour(package_id, start_time, end_time, start_km, end_km);
                        double total_charge = long_tour_extra_charge + basic_charge;
                        txttotal.Text = total_charge.ToString();
                        tour_type = "Hire,Long Tour";
                        txtwaitingcharge.Text = "0.0";

                    }
                    else
                    {
                        txtonscharge.Text = "0.0";

                        double day_tour_extra_charge = day_tour(package_id, start_time, end_time, start_km, end_km);
                        double total_charge = day_tour_extra_charge + basic_charge;
                        txttotal.Text = total_charge.ToString();
                        tour_type = "Hire,Day Tour";

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return basic_charge;
        }
        //Function to calculate long tour hire charges(overnight charge, extra km charge)
        public double long_tour(string package_id, DateTime start_time, DateTime end_time, int start_km, int end_km)
        {
 
           try
            {
                SqlCommand cmd = new SqlCommand("select*from Hire_Rates where Package_ID =@001", con);
                cmd.Parameters.AddWithValue("@001", package_id);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();


                if (r.Read())
                {

                    double driver_night = double.Parse(r["Driver_per_night"].ToString());
                    double extra_km_rate = double.Parse(r["Extra_per_Km"].ToString());
                    double vehicle_park = double.Parse(r["V_parking"].ToString());

                    TimeSpan ts = end_time.Date - start_time.Date;
                    int total_Days = ts.Days + 1;
                    int days = total_Days;

                    double over_night_charge = days * (driver_night + vehicle_park);
                    txtonscharge.Text = over_night_charge.ToString();
                    distance = end_km - start_km;

                    if (distance > max_km)
                    {
                        double extra_distance = distance - max_km;
                        extra_km_charge = extra_distance * extra_km_rate;
                        txtextrakm.Text = extra_km_charge.ToString();
                    }
                    else
                    {
                        txtextrakm.Text = "0.0";

                    }
                    long_tour_charge = over_night_charge + extra_km_charge;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return long_tour_charge;
        }
        //Function to calculate day tour hire charge(waiting charge, extra km charge)
        public double day_tour(string package_id, DateTime start_time, DateTime end_time, int start_km, int end_km)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("select*from Hire_Rates where Package_ID =@001", con);
                cmd.Parameters.AddWithValue("@001", package_id);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {

                    double extra_hour = double.Parse(r["Extra_per_hour"].ToString());
                    double driver_night = double.Parse(r["Driver_per_night"].ToString());
                    double extra_km_rate = double.Parse(r["Extra_per_Km"].ToString());
                    double vehicle_park = double.Parse(r["V_parking"].ToString());

                    TimeSpan ts = end_time.Date - start_time.Date;
                    int total_Days = ts.Days + 1;
                    int days = total_Days;
                    int hours = days * 24;
                    double max_hours_total = max_hour * days;

                    if (hours > max_hours_total)
                    {
                        double extra_hours = hours - max_hours_total;

                        extra_hour_charge = extra_hours * extra_hour;
                        txtwaitingcharge.Text = extra_hour_charge.ToString();
                    }
                    else
                    {
                        txtwaitingcharge.Text = "0.0";


                    }
                    if (distance > max_hour)
                    {
                        distance = end_km - start_km;
                        double extra_distance = distance - max_km;
                        extra_km_charge = extra_distance * extra_km_rate;
                        txtextrakm.Text = extra_km_charge.ToString();

                    }
                    else
                    {
                        txtextrakm.Text = "0.0";

                    }
                    day_tour_charge = extra_km_charge + extra_hour_charge;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return day_tour_charge;
        }

        //Function calling (rent calculation) 
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime start_date = dtp1.Value;
                DateTime end_date = dtp2.Value;
                int end_km = int.Parse(txtend.Text);
                int start_km = int.Parse(txtstart.Text);
                string veh_id = cmbV_No.SelectedValue.ToString();

                double basic_charge = 0.0;
                if (MessageBox.Show("Are you sure you want to calculate?",
            "Confirmation", MessageBoxButtons.YesNo,
        MessageBoxIcon.Question) == DialogResult.No)
                {
                    con.Close();
                }
                else
                {
                    if (rbrent.Checked)
                    {
                        basic_charge = rent_calculation(veh_id, start_date, end_date, false);

                    }
                    else if (rbrentwdriver.Checked)
                    {
                        basic_charge = rent_calculation(veh_id, start_date, end_date, true);
                    }
                    txtbasiccharger.Text = String.Format("{0:.00}", basic_charge);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To fill vehicle number combobox
        private void fill_combo_box()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select V_No from Rent_Rates", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbV_No.DataSource = dt;
            cmbV_No.DisplayMember = "V_No";
            cmbV_No.ValueMember = "V_No";
        }
        //To fill vehicle model combobox
        private void fill_combo_box1()
        {
            SqlDataAdapter daa =
                new SqlDataAdapter("Select V_Model from Rent_Rates", con);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            cmbmodel.DataSource = dtt;
            cmbmodel.DisplayMember = "V_Model";
            cmbmodel.ValueMember = "V_Model";
        }
        //Function to calculate rental charges
        public double rent_calculation(string veh_id, DateTime start_date, DateTime end_date, bool with_driver)
        {

            double rent = 0.0;
            try
            {
                SqlCommand cmd = new SqlCommand("Select*from Rent_Rates where V_No=@001", con);
                cmd.Parameters.AddWithValue("@001", veh_id); 
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read())
                {
                    double day_charge = double.Parse(r["per_day"].ToString());
                    double week_charge = double.Parse(r["per_week"].ToString());
                    double monthly_charge = double.Parse(r["per_month"].ToString());
                    driver_charge = double.Parse(r["per_day_driver"].ToString());
                    TimeSpan ts = end_date.Date - start_date.Date;
                    int total_days = ts.Days + 1;
                    int days = total_days;
                    int month = (int)days / 30;
                    days = days % 30;
                    int week_count = (int)days / 7;
                    days = days % 7;
                    rent = month * monthly_charge + week_count * week_charge + days * day_charge;

                    if (with_driver)
                    {
                        driver_charge = total_days * driver_charge;
                        txtdrivercharge.Text = driver_charge.ToString();

                        double total_charge = rent + driver_charge;
                        txttotalcharge.Text = total_charge.ToString();
                    }
                    else
                    {
                        txtdrivercharge.Text = "0.0";
                        txttotalcharge.Text = rent.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return rent;
        }
        //To fill data into the package id combobox
        private void fill_combo_box2()
        {
            SqlDataAdapter dada =
                new SqlDataAdapter("Select Package_ID from Hire_Rates", con);
            DataTable dtdt = new DataTable();
            dada.Fill(dtdt);
            cmbpackage.DataSource = dtdt;
            cmbpackage.DisplayMember = "Package_ID";
            cmbpackage.ValueMember = "Package_ID";
        }
        //To fill data into the vehicle model combobox
        private void fill_combo_box3()
        {
            SqlDataAdapter vm =
                new SqlDataAdapter("Select V_Model from Rent_Rates", con);
            DataTable vmt = new DataTable();
            vm.Fill(vmt);
            cmbVmodel.DataSource = vmt;
            cmbVmodel.DisplayMember = "V_Model";
            cmbVmodel.ValueMember = "V_Model";
        }
        //Function calling (basic hire charge)
        private void bunifuFlatButton6_Click_1(object sender, EventArgs e)
        {
            try
            {
                DateTime start_time = dtpstime.Value;
                DateTime end_time = dtpetime.Value;
                int start_km = int.Parse(txtstartkmh.Text);
                int end_km = int.Parse(txtendkmh.Text);
                string package_id = cmbpackage.SelectedValue.ToString();


                double basic_hire_charge = 0.0;
                if (MessageBox.Show("Are you sure you want calculate ?",
                 "Confirmation", MessageBoxButtons.YesNo,
             MessageBoxIcon.Question) == DialogResult.No)
                {
                    con.Close();
                }
                else
                {
                    basic_hire_charge = basic_hire(package_id, start_time, end_time, start_km, end_km);
                    txtbasicchargeh.Text = basic_hire_charge.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To insert payment details of hire charges into the table
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            try
            {
                package_id = cmbpackage.SelectedValue.ToString();
                v_model = cmbVmodel.SelectedValue.ToString();
                basic_charge = int.Parse(txtbasicchargeh.Text);
                total = int.Parse(txttotal.Text);
                p_id = txtPID.Text;
                c_id = txtCID.Text;
                payment_type = cmbpaytype.SelectedItem.ToString();


                con.Open();

                string insert = "Insert into Payment_Details values ('" + p_id + "','" + total + "','" + basic_charge
                    + "','" + payment_type + "','" + tour_type + "','" + v_model + "','" + package_id + "','" + c_id + "')";
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
                    con.Close();

                    MessageBox.Show("Record added successfully", "Success"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information); con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //To insert rental charges data into the payment details
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                double basic_charge = double.Parse(txtbasiccharger.Text);
                v_model = cmbVmodel.SelectedValue.ToString();
                total = int.Parse(txttotalcharge.Text);
                p_id = txtPID.Text;
                c_id = txtCID.Text;
                payment_type = cmbpaytype.SelectedItem.ToString();
                con.Open();

                string insert1 = "Insert into Payment_Details values ('" + p_id + "','" + total + "','" + basic_charge
                    + "','" + payment_type + "','" + tour_type + "','" + v_model + "','" + null + "','" + c_id + "')";
                if (MessageBox.Show("Are you sure you want to add new record?",
             "Confirmation", MessageBoxButtons.YesNo,
         MessageBoxIcon.Question) == DialogResult.No)
                {
                    con.Close();
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand(insert1, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
    
