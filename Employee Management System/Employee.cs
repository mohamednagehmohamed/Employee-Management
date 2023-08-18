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
    public partial class Employee : Form
    {
        SpeechSynthesizer ssynthize = new SpeechSynthesizer();
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\el mahdi pc\Documents\Employee Management System.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Employee()
        {
            InitializeComponent();
        }
        private void popup() {
            con.Open();
            string query = "select * from employeetbl";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource=ds.Tables[0];
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (empidtbl.Text.Trim() == "" || empnametbl.Text.Trim() == "" || empaddresstbl.Text.Trim() == "" || empdoptbl.Text == "" || empedutbl.Text.Trim() == "" || empgendertbl.Text.Trim() == "" || empphonetbl.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill  All Data Required", "Employee Management Administrator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ssynthize.SpeakAsync("Please Fill All data required");
            }
            else {
                try {
                    con.Open();
                    string query = "insert into employeetbl values('"+empidtbl.Text+"','"+empnametbl.Text+"','"+empaddresstbl.Text+"','"+emppositiontbl.SelectedItem.ToString()+"','"+empdoptbl.Value.Date+"','"+empphonetbl.Text+"','"+empedutbl.SelectedItem.ToString()+"','"+empgendertbl.SelectedItem.ToString()+"')";
                    SqlCommand sqlcmd = new SqlCommand(query,con);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Person Data Is Added  Welcome");
                    ssynthize.SpeakAsync("Person Data Is Added  Welcome Sir" + empnametbl.Text);
                    con.Close();
                    popup();
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            popup();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
            ssynthize.SpeakAsync("Welcome To Home Form");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (empidtbl.Text.Trim() == "" || empnametbl.Text.Trim() == "" || empaddresstbl.Text.Trim() == "" || empdoptbl.Text == "" || empedutbl.Text.Trim() == "" || empgendertbl.Text.Trim() == "" || empphonetbl.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill  All Data Required", "Employee Management Administrator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ssynthize.SpeakAsync("Please Fill All data required");
            }
            else {
                try {
                    con.Open();
                    string query = "update employeetbl set empname='" + empnametbl.Text + "',empadd='" + empaddresstbl.Text + "',emppos='" + emppositiontbl.SelectedItem.ToString() + "',empdop='" + empdoptbl.Value.Date + "',empphone='" + empphonetbl.Text + "',empedu='" + empedutbl.SelectedItem.ToString() + "',empgen='"+empgendertbl.SelectedItem.ToString()+"'";
                    SqlCommand sqlcmd = new SqlCommand(query,con);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");
                    ssynthize.SpeakAsync("Updated Successfully sir"+empnametbl.Text);
                    con.Close();
                    popup();
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (empidtbl.Text.Trim() == "")
            {
                MessageBox.Show("Please Fill  ID TextBox", "Employee Management Administrator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ssynthize.SpeakAsync("Please Fill  ID TextBox");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from employeetbl where empid='"+empidtbl.Text+"'";
                    SqlCommand sqlcmd = new SqlCommand(query, con);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully........");
                    ssynthize.SpeakAsync("Deleted Successfully sir" + empnametbl.Text);
                    con.Close();
                    popup();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            empidtbl.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            empnametbl.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            empaddresstbl.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            emppositiontbl.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
           // empdoptbl.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            empphonetbl.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            empedutbl.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            empgendertbl.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }
    }
}
