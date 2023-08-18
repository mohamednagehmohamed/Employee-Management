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
    public partial class Form1 : Form
    {
        SpeechSynthesizer ssynthize = new SpeechSynthesizer();
        int startpoint = 0;
        public Form1()
        {
            InitializeComponent();
            ssynthize.SelectVoiceByHints(VoiceGender.Female);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint++;
            progressBar1.Value = startpoint;
            lbl.ForeColor = Color.Green;
            lbl.Text = "%"+progressBar1.Value.ToString();
            if(progressBar1.Value==100){
                progressBar1.Value = 0;
                timer1.Stop();
                this.Hide();
                Login log = new Login();
                log.Show();
                ssynthize.SpeakAsync("Login Form");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            ssynthize.SpeakAsync("Welcome Employee, Please Wait For Loading Form");
        }
    }
}
