using beablies.Model;
using beablies.View;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace beablies
{
    public partial class frmMain : Form
    {
        string conn = "server=localhost;database=db_mk2uas;uid=root;pwd=";
        MySqlConnection mySqlConnection;
        public frmMain()
        {
            InitializeComponent();
        }

        static frmMain _obj;

        public static frmMain Instance
        {
            get { if (_obj == null) { _obj = new frmMain(); } return _obj; }
        }


        //add control in mainform

        public   void  AddControls(Form f)
        {
            CenterPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            CenterPanel.Controls.Add(f);
            f.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = MainClass.USER;
            _obj = this;
            GetData();
            LoadDailyChartData();
            LoadMonthlyChartData();
        }
        private void LoadDailyChartData()
        {
            string query = "SELECT date, total FROM main";
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(conn))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }

            // Clear existing data points
            chartDaily.Series.Clear();

            // Create a new series
            Series series = new Series("Daily Total");
            series.ChartType = SeriesChartType.Column;
            chartDaily.Series.Add(series);

            // Group the data by date and calculate the total per day
            var groupedData = dataTable.AsEnumerable()
                .GroupBy(row => row.Field<DateTime>("date").Date)
                .Select(group => new
                {
                    Date = group.Key,
                    Total = group.Sum(row => row.Field<float>("total")) // Cast to float
                });

            // Add the grouped data to the chart
            foreach (var data in groupedData)
            {
                series.Points.AddXY(data.Date, data.Total);
            }

            // Configure chart area
            chartDaily.ChartAreas[0].AxisX.LabelStyle.Format = "dd-MM-yyyy";
            chartDaily.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
            chartDaily.ChartAreas[0].AxisX.Interval = 1;
            chartDaily.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartDaily.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        }
        private void LoadMonthlyChartData()
        {
            string query = "SELECT date, total FROM main";
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(conn))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }

            // Clear existing data points
            chart1.Series.Clear();

            // Create a new series
            Series series = new Series("Monthly Total");
            series.ChartType = SeriesChartType.Column;
            chart1.Series.Add(series);

            // Group the data by month and year and calculate the total per month
            var groupedData = dataTable.AsEnumerable()
                .GroupBy(row => new { Month = row.Field<DateTime>("date").Month, Year = row.Field<DateTime>("date").Year })
                .Select(group => new
                {
                    MonthYear = new DateTime(group.Key.Year, group.Key.Month, 1),
                    Total = group.Sum(row => row.Field<float>("total")) // Cast to float
                });

            // Add the grouped data to the chart
            foreach (var data in groupedData)
            {
                series.Points.AddXY(data.MonthYear.ToString("MMM yyyy"), data.Total);
            }

            // Configure chart area
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        }


        public void GetData()
        {
            string qry = "SELECT COUNT(*) FROM staff";
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlCommand cmd = new MySqlCommand(qry, mySqlConnection);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    label1.Text = count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }

            string qry2 = "SELECT COUNT(*) FROM product";
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlCommand cmd = new MySqlCommand(qry2, mySqlConnection);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    label3.Text = count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
            string qry3 = "SELECT COUNT(*) FROM category";
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlCommand cmd = new MySqlCommand(qry3, mySqlConnection);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    label5.Text = count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
            string qry4 = "SELECT COUNT(*) FROM tables";
            using (MySqlConnection mySqlConnection = new MySqlConnection(conn))
            {
                try
                {
                    mySqlConnection.Open();
                    MySqlCommand cmd = new MySqlCommand(qry4, mySqlConnection);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    label7.Text = count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CenterPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome());
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnCategories_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView());
        }

        private void BtnTables_Click(object sender, EventArgs e)
        {
            AddControls(new frmTable());
        }

        private void BtnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffView());
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            AddControls(new frmProductView());
        }

        private void BtnPOS_Click(object sender, EventArgs e)
        {
            frmPOS frm = new frmPOS();
            frm.Show();
        }

        private void BtnKitchen_Click(object sender, EventArgs e)
        {
            frmKitchenView frm = new frmKitchenView();
            frm .Show();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            AddControls(new frmReportcs());
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panelStaff_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chartDaily_Click(object sender, EventArgs e)
        {
            LoadDailyChartData();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            LoadMonthlyChartData();
        }
    }
}
