using beablies.Model;
using beablies.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beablies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            var userValidation = MainClass.IsValidUser(txtName.Text, txtPass.Text);
            if (userValidation.isValid == false)
            {
                guna2MessageDialog1.Show("Invalid username and password");
                return;
            }
            else
            {
                this.Hide();
                if (userValidation.role == "manager")
                {
                    frmMain frm = new frmMain();
                    frm.Show();
                }
                else if (userValidation.role == "cashier")
                {
                    frmPOS frm = new frmPOS();
                    frm.Show();
                }
                else if (userValidation.role == "kitchen")
                {
                    frmKitchenView frm = new frmKitchenView();
                    frm.Show();
                }
                else
                {
                    guna2MessageDialog1.Show("Unknown user role");
                    this.Show(); 
                }
            }
        }

    }
}
