using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proyecto_I
{    
    public partial class Login : Form
    {        

        public Login()
        {
            InitializeComponent();                        
            initializeAll();
        }

        private void initializeAll()
        {                                    
            DBLogic.connections[0] = @"Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=Proyecto_1;User ID=sa;Password=Girardot8";
            DBLogic.connections[1] = @"Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=Proyecto_1;User ID=sa;Password=hola1";
            DBLogic.connections[2] = @"Data Source=ALEXHDZ-LAPTOP\TESTDATABASE;Initial Catalog=Proyecto_1;Integrated Security=True";
        }

        private bool validateCredentials()
        {
            if (txtUser.Text == "") { errorProvider.SetError(txtUser, "The user can't be blank!"); return false; }
            else { errorProvider.Clear(); }

            if (txtPassword.Text == "") { errorProvider.SetError(txtPassword, "The password can't be blank!"); return false;}
            else { errorProvider.Clear(); }

            if (cmbCampus.Text == "") { errorProvider.SetError(cmbCampus, "The campus can't be blank!"); return false;}
            else { errorProvider.Clear(); }

            return true;
        }

        private void cmbCampus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBLogic.con.ConnectionString = DBLogic.connections[cmbCampus.SelectedIndex];
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (validateCredentials())
            {
                try
                {
                    DBLogic.con.Open();
                    //DBLogic.con.Close();                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
        }

        

    }
}
