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
    public partial class View_Form : Form
    {
        SpeechSynthesizer ssynthize = new SpeechSynthesizer();
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\el mahdi pc\Documents\Employee Management System.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public View_Form()
        {
            InitializeComponent();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
            ssynthize.SpeakAsync("Welcome To Home");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void View_Form_Load(object sender, EventArgs e)
        {
            empidlabel.Visible = false;
            empaddresslbl.Visible = false;
            empnamelbl.Visible = false;
            empphonelbl.Visible = false;
            empedulbl.Visible = false;
            empdoplbl.Visible = false;
            empgenderlbl.Visible = false;
            emppositionlbl.Visible = false;
        }
        private void checkid() {
            con.Open();
            string query = "select empid from employeetbl where empid='"+txtid.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows[0][0].ToString()=="1"){
                MessageBox.Show("This ID Dosnt Exist ", "Employee Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ssynthize.SpeakAsync("This ID Dosnt Exist");
            }
            con.Close();
        }
        private void fetchdata() {
            if (txtid.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill TextBox Id ", "Employee Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ssynthize.SpeakAsync("Please Fill TextBox Id");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "select * from employeetbl where empid='" + txtid.Text + "'";
                    SqlCommand sqlcmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    foreach( DataRow dr in dt.Rows ){
                        empidlabel.Text = dr["empid"].ToString();
                        empnamelbl.Text = dr["empname"].ToString();
                        empaddresslbl.Text = dr["empadd"].ToString();
                        emppositionlbl.Text = dr["emppos"].ToString();
                        empdoplbl.Text = dr["empdop"].ToString();
                        empphonelbl.Text = dr["empphone"].ToString();
                        empedulbl.Text = dr["empedu"].ToString();
                        empgenderlbl.Text = dr["empgen"].ToString();
                        empidlabel.Visible = true;
                        empaddresslbl.Visible = true;
                        empnamelbl.Visible = true;
                        empphonelbl.Visible = true;
                        empedulbl.Visible = true;
                        empdoplbl.Visible = true;
                        empgenderlbl.Visible = true;
                        emppositionlbl.Visible = true;
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           // checkid();
            fetchdata();
            ssynthize.SpeakAsync("Done Process");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("_______Employee Page_______", new Font("Arial", 22, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Employee ID::" + "\t" + empidlabel.Text + "\t" + "Employee Name::" + "\t" + empnamelbl.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Blue, new Point(100, 80));
            e.Graphics.DrawString("Employee Address" + "\t" + empaddresslbl.Text + "\t" + "Employee Gender\t" + empgenderlbl.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Green, new Point(100, 160));
            e.Graphics.DrawString("Employee Position" + "\t" + emppositionlbl.Text + "\t" + "Employee Dop\t" + empdoplbl.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Navy, new Point(100, 240));
            e.Graphics.DrawString("Employee Phone" + "\t" + empphonelbl.Text + "\t" + "Employee Education\t" + empedulbl.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Navy, new Point(100, 320));
            e.Graphics.DrawString("===========EmpiSoft==========", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(100, 400));
        }
    }
}
