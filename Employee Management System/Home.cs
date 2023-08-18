using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
namespace Employee_Management_System
{
    public partial class Home : Form
    {
        SpeechSynthesizer ssynthize = new SpeechSynthesizer();
        int xlabel = 5;
        public Home()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            movelabel.Left -= xlabel;
            if(movelabel.Left==0||movelabel.Left==this.ClientSize.Width){
                xlabel = -xlabel;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
            this.Hide();
            ssynthize.SpeakAsync("Welcome To Employee Form");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult islogout;
            islogout = MessageBox.Show("Confirm If You Want To LogOut","Employee Management Administrator",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(islogout==DialogResult.Yes){
                Login log = new Login();
                log.Show();
                this.Hide();
                ssynthize.SpeakAsync("Welcome To Login Form");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            View_Form view = new View_Form();
            view.Show();
            this.Hide();
            ssynthize.SpeakAsync("Welcome To View Form");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Salary_Form salary = new Salary_Form();
            salary.Show();
            this.Hide();
            ssynthize.SpeakAsync("Welcome To Salary Form");
        }
    }
}
