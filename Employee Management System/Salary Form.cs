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
    public partial class Salary_Form : Form
    {
        SpeechSynthesizer ssynthize = new SpeechSynthesizer();
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\el mahdi pc\Documents\Employee Management System.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Salary_Form()
        {
            InitializeComponent();
        }

        private void empidlabel_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
            ssynthize.SpeakAsync("Welcome To Home Form");
        }

        private void fetchdata() {
            if (empidlabel.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill Employee id ", "Employee Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "select empid,empname,emppos from employeetbl where empid='" + empidlabel.Text + "'";
                    SqlCommand sqlcmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        empnamelbl.Text = dr["empname"].ToString();
                        empposlbl.Text = dr["emppos"].ToString();
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            fetchdata();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int dailybase,total;
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(empposlbl.Text.Trim()==""){
                MessageBox.Show("Please Select An Employee", "Employee Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ssynthize.SpeakAsync("Please Select An Employee");
            }
            else if (txtweek.Text.Trim() == "" || Convert.ToInt32(txtweek.Text) > 28)
            {
                MessageBox.Show("Please Enter Valid Number Of Days", "Employee Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ssynthize.SpeakAsync("Please Enter Valid Number Of Days");
            }
            else {
                if (empposlbl.Text == "Manager") {
                    dailybase = 250;
                }
                else if (empposlbl.Text == "Senior Developer")
                {
                    dailybase = 230;
                }
                else if (empposlbl.Text == "Junior Developer")
                {
                    dailybase = 230;
                }
                else {
                    dailybase = 150;
                }
                total = dailybase * Convert.ToInt32(txtweek.Text);
                richTextBox1.Text = "Employee ID: "+empidlabel.Text + "\n" + "Employee Name: "+empnamelbl.Text + "\n" + "Employee Position: "+empposlbl.Text + "\n" + "Dailay Amount: "+dailybase + "\n" + "Week Days: "+txtweek.Text + "\n" + "Salary: "+total;
                ssynthize.SpeakAsync("View Person Data");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog()==DialogResult.OK){
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("_______Employee Page_______",new Font("Arial",15,FontStyle.Bold),Brushes.Red,new Point(230));
            e.Graphics.DrawString("Employee ID::" + "\t" + empidlabel.Text+"\t"+"Employee Name::"+"\t"+empnamelbl.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Blue, new Point(100,80));
            e.Graphics.DrawString("Employee Position" + "\t" + empposlbl.Text + "\t" + "Week Days\t" +  txtweek.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Green, new Point(100, 160));
            e.Graphics.DrawString("Daily Amount" + "\t" + dailybase + "\t" + "Total\t" + total, new Font("Arial", 15, FontStyle.Bold), Brushes.Navy, new Point(100, 240));
            e.Graphics.DrawString("===========EmpiSoft==========" , new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(100, 320));
        }
    }
}
