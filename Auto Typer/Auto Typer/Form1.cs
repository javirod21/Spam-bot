using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        string[] phrases = File.ReadAllLines("phrases.txt");

        private void createChecklist()
        //updates the check list on the GUI (called on program startup and when an item is added/deleted
        {
            phrases = File.ReadAllLines(textFile);

            //clear the list (if there is one already)
            phrasesList.Items.Clear();

            //create a new list with the items in the text file
            foreach (string phrase in phrases)
            {
                phrasesList.Items.Add(phrase, false);
            }
        }

        public Form1()
        //what to do on startup
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
            //var phrases = File.ReadAllLines(textFile);
            var r = new Random();
            var randPhrase = r.Next(0, phrases.Length - 0);
            return phrases[randPhrase];
        }

        private void timer1_Tick(object sender, EventArgs e)
        //Timer- Calls function to select random phrase and sends it
        {
            string randPhrase = selectPhrase();
            SendKeys.Send(randPhrase);
            SendKeys.Send("{ENTER}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timeBox.Text = "1";
        }

        private void button4_Click(object sender, EventArgs e)
        //Add button- Adds line in text box to the file
        {
            File.AppendAllText(textFile, textBox.Text + Environment.NewLine);
            textBox.Clear();
            createChecklist();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> toDelete = new List<string>();
            
            for(int i = 0; i <= phrasesList.Items.Count - 1; i++)
            {
                if (phrasesList.GetItemChecked(i))
                    toDelete.Add(phrasesList.Items[i].ToString());
            }

            string[] toDeleteArr = toDelete.ToArray();

            string tempFile = Path.GetTempFileName();

            using (var sr = new StreamReader(textFile))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                int j = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    bool deleteMe = false;
                    for(int i = j; i < toDeleteArr.Length; i++)
                    {
                        if (line == toDeleteArr[i])
                        {
                            deleteMe = true;
                            j++;
                            break;
                        }
                    }
                    if (!deleteMe)
                        sw.WriteLine(line);
                }
            }

            File.Delete(textFile);
            File.Move(tempFile, textFile);

            createChecklist();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        //Allows user to press enter instead of clicking the "Add" button while on the textbox
        {
            Button btn = button4;
            if(e.KeyCode == Keys.Enter)
            {
                btn.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void timeBox_KeyDown(object sender, KeyEventArgs e)
        //Allows user to press enter instead of clicking the "Start" button while on the timebox
        {
            Button btn = button1;
            if(e.KeyCode == Keys.Enter)
            {
                btn.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
