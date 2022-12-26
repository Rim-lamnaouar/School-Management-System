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

namespace School_Management_System1
{
    public partial class Student : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = SchoolDBB; Integrated Security = True; Pooling=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        //ID variable used in Updating and Deleting Record  
        int StId;
        public Student()
        {
            InitializeComponent();
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && textBox2.Text != "" && textBox3.Text != "")
            {
                cmd = new SqlCommand("insert into Student (StName, StGen, StDDB, StClass, StFees, StAdr) values (@SName, @SGen, @SDDB, @SClass, @SFees, @SAdr)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@SName", textBox1.Text);
                cmd.Parameters.AddWithValue("@SGen", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SDDB", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@SClass", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SFees", textBox2.Text);
                cmd.Parameters.AddWithValue("@SAdr", textBox3.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
        //Display Data in DataGridView  
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Student", con);
            adapt.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            StId = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StId != 0)
            {
                cmd = new SqlCommand("delete Student where  StId=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", StId);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StId = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBox1.SelectedItem = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            dateTimePicker1.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox2.SelectedItem = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox2.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox3.Text = guna2DataGridView1.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Update Student set StName=@Sname, StGen=@SGen, StDDB=@SDDB, StClass=@SClass, StFees=@SFees, StAdr=@SAdr where StId=@Id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Id", StId);
                cmd.Parameters.AddWithValue("@SName", textBox1.Text);
                cmd.Parameters.AddWithValue("@SGen", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SDDB", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@SClass", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@SFees", textBox2.Text);
                cmd.Parameters.AddWithValue("@SAdr", textBox3.Text);
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Student_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu Obj = new Menu();
            Obj.Show();
            this.Hide();
        }
    }
}
