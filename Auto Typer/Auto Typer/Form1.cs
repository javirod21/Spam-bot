using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Typer
{
    public partial class Form1 : Form
    {
        string textFile = "phrases.txt";

        public void createCkeclist();
        {

        }

        public Form1()
        {
            InitializeComponent();
            createChecklist();
        }

        private void button1_Click(object sender, EventArgs e)
        //Start button- Starts timer
        {
            try
            {
                timer1.Enabled = true;
                int interval = int.Parse(timeBox.Text);
                timer1.Interval = interval * 1000;
            }
            catch
            {
                timer1.Enabled = false;
                MessageBox.Show("No input interval");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        //Stop button- Stops timer
        {
            timer1.Enabled = false;
        }

        private string selectPhrase()
        //Returns a random line from the text file
        {
            var phrases = File.ReadAllLines(textFile);
            var r = new Random();
            var randPhrase = r.Next(0, phrases.Length - 1);
            return phrases[randPhrase];
        }

        private void timer1_Tick(object sender, EventArgs e)
        //Timer- Calls function to select random phrase and sends it
        {
            string randPhrase = selectPhrase();//textBox.Text;
            SendKeys.Send(randPhrase);
            SendKeys.Send("{ENTER}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timeBox.Text = "3";
        }
    }
}
