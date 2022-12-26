using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace School_Management_System1
{
    public partial class Attendance : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = SchoolDBB; Integrated Security = True; Pooling=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        DataTable d = new DataTable();
        SqlDataReader dr;
        int AttStId;

        public Attendance()
        {
            InitializeComponent();
            FillStudent();
            DisplayData();
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
            cmd.Parameters.AddWithValue("@Id",comboBox3.SelectedValue.ToString());
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter(cmd);
            adapt.Fill(dt);
            foreach(DataRow dar in dt.Rows)
            {
                textBox1.Text = dar["StName"].ToString();
            }
            con.Close();
        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Attendance", con);
            adapt.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            comboBox2.SelectedIndex = -1;
            textBox1.Text = "";
            comboBox3.SelectedIndex = -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox2.SelectedIndex != -1)
            {
                cmd = new SqlCommand("insert into Attendance (AttStuId, AttStuName, AttDate, AttStatus) values (@AttId, @AttName, @AtDate, @AtStatus)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@AttId", comboBox3.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@AttName", textBox1.Text);
                cmd.Parameters.AddWithValue("@AtDate", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@AtStatus", comboBox2.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Attendance Taken");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStudentName();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox3.SelectedValue = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            dateTimePicker1.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox2.SelectedItem = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox2.SelectedIndex != -1)
            {
                //cmd = new SqlCommand("insert into Attendance (AttStuId, AttStuName, AttDate, AttStatus) values (@AttId, @AttName, @AtDate, @AtStatus)", con);
                cmd = new SqlCommand("Update Attendance set AttStuId=@AttId, AttStuName=@AttName, AttDate=@AtDate, AttStatus=@AtStatus where AttStuName=@AttName", con);
                con.Open();
                cmd.Parameters.AddWithValue("@AttId", comboBox3.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@AttName", textBox1.Text);
                cmd.Parameters.AddWithValue("@AtDate", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@AtStatus", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@AName", AttStId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                DisplayData();
                ClearData();

            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu Obj = new Menu();
            Obj.Show();
            this.Hide();
        }
    }
}
