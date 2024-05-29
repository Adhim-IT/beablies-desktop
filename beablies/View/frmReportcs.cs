using beablies.Report;
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

namespace beablies.View
{
    public partial class frmReportcs : Form
    {
        public frmReportcs()
        {
            InitializeComponent();
        }

        private void BtnCategories_Click(object sender, EventArgs e)
        {
            string qry = "SELECT * FROM product";
            MySqlCommand cmd = new MySqlCommand(qry, MainClass.con);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            frmPrint frm = new frmPrint();
            rptMenu cr = new rptMenu();

            cr.SetDatabaseLogon("", "");
            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            string qry = "SELECT * FROM staff";
            MySqlCommand cmd = new MySqlCommand(qry, MainClass.con);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            frmPrint frm = new frmPrint();
            rptStaff cr = new rptStaff();

            cr.SetDatabaseLogon("", "");
            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            frmsalebycategory frm = new frmsalebycategory();
            frm.ShowDialog();
        }
    }
}
