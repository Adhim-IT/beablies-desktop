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

namespace beablies.Report
{
    public partial class frmsalebycategory : Form
    {
        public frmsalebycategory()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string qry = "SELECT * FROM main INNER JOIN detail ON main.id = detail.main_id INNER JOIN product on product.pID = detail.product_id INNER JOIN category on category.id = product.category_id WHERE main.date between @sdate and @edate";
           
            MySqlCommand cmd = new MySqlCommand(qry, MainClass.con);
            cmd.Parameters.AddWithValue("@sdate" , Convert.ToDateTime(dateStart.Value).Date);
            cmd.Parameters.AddWithValue("@edate", Convert.ToDateTime(dateStart.Value).Date);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            frmPrint frm = new frmPrint();
            rptsalebycategory cr = new rptsalebycategory();

            cr.SetDatabaseLogon("", "");
            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }
    }
}
