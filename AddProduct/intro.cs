using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddProduct
{
    public partial class intro : Form
    {
        public intro()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Assistan frmAs = new Assistan();
            frmAs.Show();
        }

        private void intro_Load(object sender, EventArgs e)
        {
            
        }
    }
}
