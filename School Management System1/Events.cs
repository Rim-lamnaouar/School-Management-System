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
    public partial class Events : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source =.\sqlexpress; Initial Catalog = SchoolDBB; Integrated Security = True; Pooling=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        //ID variable used in Updating and Deleting Record  
        int EId;
        public Events()
        {
            InitializeComponent();
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                cmd = new SqlCommand("insert into Events (EDesc, EDate, EDuration) values (@Desc, @Date, @Duration)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Desc", textBox1.Text);
                cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@Duration", textBox2.Text);
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
            adapt = new SqlDataAdapter("select * from Events", con);
            adapt.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            EId = 0;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Update Events set EDesc=@Desc, EDate=@Date, EDuration=@Duration where EId=@Id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Id", EId);
                cmd.Parameters.AddWithValue("@Desc", textBox1.Text);
                cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@Duration", textBox2.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (EId != 0)
            {
                cmd = new SqlCommand("delete from Events where  EId=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", EId);
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
            EId = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            dateTimePicker1.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox2.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
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
