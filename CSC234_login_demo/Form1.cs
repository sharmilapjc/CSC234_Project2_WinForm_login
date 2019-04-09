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


namespace CSC234_login_demo
{
    public partial class Form1 : Form
    {
        static int numAttempts = 2;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(@"..\..\images\login.png");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (numAttempts == 0)
            {
                label_invisible.Text = ("MAXIMUM ATTEMPTS REACHED. ACCESS DENIED!");
                return;
            }

            SqlConnection scn = new SqlConnection();
            scn.ConnectionString = @"Data Source=LAPTOP-A2QS35FN\SQLEXPRESS;Initial Catalog=credentials;database=CSC_234_LOGIN;integrated security=SSPI";
            SqlCommand scmd = new SqlCommand("select count (*) as cnt from credentials where username=@usr and password=@pwd", scn);
            scmd.Parameters.Clear();
            scmd.Parameters.AddWithValue("@usr", txtbox_username.Text);
            scmd.Parameters.AddWithValue("@pwd", txtbox_password.Text);
            scn.Open();

            if (scmd.ExecuteScalar().ToString() == "1")
            {
                pictureBox1.Image = new Bitmap(@"..\..\images\granted.jpg");
                label_invisible.Text = "";
                MessageBox.Show("ACCESS IS GRANTED!");
            }
            else
            {
                pictureBox1.Image = new Bitmap(@"..\..\images\denied.jpg");
                MessageBox.Show("ACCESS IS DENIED!");
                label_invisible.Text = (Convert.ToString(numAttempts) + " Attempts Left");
                --numAttempts;
                txtbox_username.Clear();
                txtbox_password.Clear();
            }
            scn.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
