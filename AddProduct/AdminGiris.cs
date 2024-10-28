using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AddProduct
{
    public partial class AdminGiris : Form
    {

        SqlConnection connection = new SqlConnection("Data source=LAPTOP-FHN7GUE1\\SQLEXPRESS; Initial Catalog=DpProduct; Integrated Security=TRUE");
        public AdminGiris()
        {
            InitializeComponent();
        }

        private void AdminGiris_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
         bool varmi;
        private void button4_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text, pass = textBox3.Text, email = textBox2.Text;
           
            connection.Open();
            SqlCommand command = new SqlCommand("Select *from İnputTable", connection);
            SqlDataReader rdr = command.ExecuteReader();//tablodan gelen tüm verileri okur

            while (rdr.Read())
            {
                if (username == rdr["Username"].ToString().TrimEnd() && pass == rdr["Pass"].ToString().TrimEnd() && email == rdr["E-mail"].ToString().TrimEnd())
                {
                    varmi = true;
                   
                    break;
                }
                else
                {
                    varmi = false;
                }
            }
            
            connection.Close();

            if (varmi)
            {
                MessageBox.Show("Successful login :)");
                this.Hide();
                   Form1 form = new Form1();
                   form.Show();
            }
            else
            {
                MessageBox.Show("Failed login :(");
            }

            
        }
        

    }
}
