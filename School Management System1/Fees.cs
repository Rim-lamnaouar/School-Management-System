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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace School_Management_System1
{
    public partial class Fees : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = SchoolDBB; Integrated Security = True; Pooling=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        DataTable d = new DataTable();
        SqlDataReader dr;
        //int AttStId;
        public Fees()
        {
            InitializeComponent();
            DisplayData();
            FillStudent(); 
        }
        private void FillStudent()
        {
            con.Open();
            cmd = new SqlCommand("select * from Student", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StId", typeof(int));
            dt.Load(dr);
            comboBox3.ValueMember = "StId";
            comboBox3.DataSource = dt;
            con.Close();
        }

        private void GetStudentName()
        {
            con.Open();
            cmd = new SqlCommand("select * from Student where StId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", comboBox3.SelectedValue.ToString());
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter(cmd);
            adapt.Fill(dt);
            foreach (DataRow dar in dt.Rows)
            {
                textBox1.Text = dar["StName"].ToString();
            }
            con.Close();
        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Fees", con);
            adapt.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.Close();
        }
        private void ClearData()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            comboBox3.SelectedIndex = -1;
        }
        private void Fees_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                String paymentperiod;
                paymentperiod = dateTimePicker1.Value.Month.ToString() +"/"+ dateTimePicker1.Value.Year.ToString();
                con.Open();
                adapt = new SqlDataAdapter("select COUNT(*) from Fees where StuId = '" +comboBox3.SelectedValue.ToString()+ "' and StMonth = '" + paymentperiod.ToString()+"'",con);
                DataTable dt= new DataTable();
                adapt.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("There is No due for this Month");
                }
                else
                {
                    cmd = new SqlCommand("insert into Fees (StuId, StuName, StMonth, Amt) values (@StId, @StName, @SMonth, @SAmt)", con);
                    cmd.Parameters.AddWithValue("@StId", comboBox3.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@StName", textBox1.Text);
                    cmd.Parameters.AddWithValue("@SMonth", paymentperiod);
                    cmd.Parameters.AddWithValue("@SAmt", textBox2.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Fees Successfully Paid");
                }
                con.Close();
                DisplayData();
                ClearData();
            }
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStudentName();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu Obj = new Menu();
            Obj.Show();
            this.Hide();
        }
    }
}
