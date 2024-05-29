﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beablies.Model
{
    public partial class frmCategoryAdd : SampleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {

        }
        public int  id = 0;

        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if (id == 0)
            {
                qry = "Insert into category (name) Values(@Name)";
            }
            else
            {
                qry = "Update category set name = @Name where id = @id";
            }


            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);

            if(MainClass.SQL(qry, ht) > 0)
            {
                MessageBox.Show("Saved successfully..");
                id = 0;
                txtName.Text = "";
                txtName.Focus();

            }
        }

        public override void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
