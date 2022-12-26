using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Management_System1
{
    public partial class Dashboard : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = SchoolDBB; Integrated Security = True; Pooling=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public Dashboard()
        {
            InitializeComponent();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Menu Obj = new Menu();
            Obj.Show();
            this.Hide();
        }

        private void CountStudents()
        {
            con.Open();
            adapt= new SqlDataAdapter("select COUNT(*) from Student",con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            label9.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void CountTeachers()
        {
            con.Open();
            adapt = new SqlDataAdapter("select COUNT(*) from Teacher", con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            label10.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void CountEvents()
        {
            con.Open();
            adapt = new SqlDataAdapter("select COUNT(*) from Events", con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            label11.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void SumFees()
        {
            con.Open();
            adapt = new SqlDataAdapter("select Sum(Amt) from Fees", con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            label8.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            CountStudents();
            CountTeachers();
            CountEvents();
            SumFees();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
