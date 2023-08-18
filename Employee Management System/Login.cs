using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Speech.Synthesis;
namespace Employee_Management_System
{
    public partial class Login : Form
    {
        SpeechSynthesizer ssynthize = new SpeechSynthesizer();
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\el mahdi pc\Documents\Employee Management System.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls){
            if(c is TextBox){
                c.Text = "";
                empid.Focus();
            }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (empid.Text.Trim() == "" || emppin.Text == "")
            {
                MessageBox.Show("Missing Data Required", "Employee Management Administrator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                try {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from employeetbl where empname='" + emppin.Text + "' and empid='"+empid.Text+"'",con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Home home = new Home();
                        home.Show();
                        this.Hide();
                        ssynthize.SpeakAsync("Welcome Sir"+emppin.Text);
                    }
                    else {
                        MessageBox.Show("Please Enter Valid User And Password", "Employee Management Administrator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ssynthize.SpeakAsync("Please Enter Valid User And Password");
                    }
                    con.Close();
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
