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
    public partial class Teachers : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = SchoolDBB; Integrated Security = True; Pooling=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        //ID variable used in Updating and Deleting Record  
        int TId = 0;
        public Teachers()
        {
            InitializeComponent();
            DisplayData();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Insert Data
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && textBox2.Text != "" && textBox3.Text != "")
            {
                cmd = new SqlCommand("insert into Teacher (Tname, TGen, TPhone, TSub, TAdd, TDBb) values (@name, @Gen, @Phone, @Sub, @Add, @DBb)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Gen", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Phone", textBox2.Text);
                cmd.Parameters.AddWithValue("@Sub", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Add", textBox3.Text);
                cmd.Parameters.AddWithValue("@DBb", dateTimePicker1.Value.Date);
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
            adapt = new SqlDataAdapter("select * from Teacher", con);
            adapt.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            TId = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TId = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBox1.SelectedItem = guna2DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox2.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox2.SelectedItem = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox3.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            dateTimePicker1.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

        }
        //Update Record 
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && textBox2.Text != "" && textBox3.Text != "")
            {
                cmd = new SqlCommand("Update Teacher set Tname=@name, TGen=@Gen, TPhone=@Phone, TSub=@Sub, TAdd=@Add, TDBb=@DBb where TId=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", TId);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Gen", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Phone", textBox2.Text);
                cmd.Parameters.AddWithValue("@Sub", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Add", textBox3.Text);
                cmd.Parameters.AddWithValue("@DBb", dateTimePicker1.Value.Date);
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
        //Delete Record
        private void button3_Click(object sender, EventArgs e)
        {
            if (TId != 0)
            {
                cmd = new SqlCommand("delete Teacher where  TId=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", TId);
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

        private void button4_Click(object sender, EventArgs e)
        {
            Menu Obj = new Menu();
            Obj.Show();
            this.Hide();
        }
    }
}
