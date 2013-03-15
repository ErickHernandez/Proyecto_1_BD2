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
    public partial class Students : Form
    {
        private string currentCity;
        private int currentRow;

        public Students(string city)
        {
            InitializeComponent();
            
            currentCity = city;
            studentInit();
            txtCity.Text = currentCity;
        }

        private void studentInit()
        {           
            GrdStudents.DataSource = DBLogic.getStudents(currentCity);
            GrdStudents.ClearSelection();
            currentRow = GrdStudents.SelectedRows.Count > 0 ? 0 : -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (fieldsAreEmpty())
            {
                MessageBox.Show("Some fields are empty!");
                return;
            }

            if (DBLogic.StudentExists(txtAccount.Text))
            {                

                DBLogic.UpdateStudent(currentCity, txtAccount.Text, txtName.Text, txtLastName.Text, txtCountry.Text,
                    txtEmail.Text, txtPassword.Text);

                MessageBox.Show("Student Updated!");
                clearFields();

            }
            else
            {
                if (fieldsAreEmpty() || txtConfirmPassword.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Some fields are empty!");
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("The password does not match with confirmation password!");
                    return;
                }

                DBLogic.InsertStudent(currentCity, txtAccount.Text, txtName.Text, txtLastName.Text, txtCountry.Text,
                    txtEmail.Text, txtPassword.Text);

                MessageBox.Show("Student Saved!");

                clearFields();

            }

            GrdStudents.DataSource = DBLogic.getStudents(currentCity);
        }
        
        private void GrdStudents_RowEnter(object sender, DataGridViewCellEventArgs e)
        {                        
            currentRow = GrdStudents.SelectedRows.Count > 0 ? GrdStudents.SelectedRows[0].Index : -1;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentRow < 0)
                return;

            txtAccount.Text = GrdStudents.SelectedRows[0].Cells[0].Value.ToString();
            txtName.Text = GrdStudents.SelectedRows[0].Cells[1].Value.ToString();
            txtLastName.Text = GrdStudents.SelectedRows[0].Cells[2].Value.ToString();
            txtCountry.Text = GrdStudents.SelectedRows[0].Cells[5].Value.ToString();
            txtCity.Text = GrdStudents.SelectedRows[0].Cells[4].Value.ToString();
            txtEmail.Text = GrdStudents.SelectedRows[0].Cells[3].Value.ToString();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentRow < 0)
                return;

            DBLogic.DeleteStudent(GrdStudents.SelectedRows[0].Cells[0].Value.ToString());
            MessageBox.Show("Student Deleted!");

            GrdStudents.DataSource = DBLogic.getStudents(currentCity);
        }

        private bool fieldsAreEmpty()
        {
            if (txtAccount.Text == "" || txtName.Text == "" || txtLastName.Text == "" || txtCity.Text == "" || txtCountry.Text == "" ||
                txtEmail.Text == "")
                return true;
            else
                return false;
        }

        private void clearFields()
        {
            txtAccount.Clear();
            txtName.Clear();
            txtLastName.Clear();
            txtCountry.Clear();
            txtEmail.Clear();
            txtConfirmPassword.Clear();
            txtPassword.Clear();
        }
    }    
}
