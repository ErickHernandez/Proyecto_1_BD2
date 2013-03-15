using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proyecto_I
{
    public partial class Menu : Form
    {
        string currentCity;

        public Menu(string city)
        {
            InitializeComponent();
            currentCity = city;
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            Students st = new Students(currentCity);
            st.Show();
        }

        private void btnSections_Click(object sender, EventArgs e)
        {

        }

   

     
    

    }
}
