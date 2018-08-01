/**
 * @author  LoremasterLH
 * @date    1.8.2018
 * @email   loremasterlittlehero@gmail.com
 * @git     https://github.com/LoremasterLH
 **/

using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ShutDownPC
{
    public partial class Form1 : Form
    {
        private const string file_name = "timer.cfg";
        private int time_left = 5;  // Default value for creating the .cfg file.
        private Timer timer;
        public Form1()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = 1000;
            timer.Start();

            if(!System.IO.File.Exists(file_name))
            {
                System.IO.File.WriteAllText(file_name, time_left.ToString());
                MessageBox.Show(String.Format("File {0} not found. Created the file with a default setting of {1} seconds", file_name, time_left));
            }
            try
            {
                string time = System.IO.File.ReadAllText(file_name);
                time_left = Int32.Parse(time);
            }
            catch
            {
                MessageBox.Show(String.Format("The file {0} is not properly formatted. Make sure it only includes a number in seconds until shutdown", file_name));
            }

            InitializeComponent();

            labelWelcome.Text = "Pozdravljen, " + Environment.UserName + ". Računalnik se bo izklopil.";
            labelTimeLeft.Text = String.Format("Izklop čez {0} {1}", time_left.ToString(), SecondsFormat(time_left));
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string SecondsFormat(int time_left)
        {
            switch(time_left)
            {
                case 1:
                    return "sekundo";
                case 2:
                    return "sekundi";
                case 3:
                case 4:
                    return "sekunde";
                default:
                    return "sekund";
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            time_left--;
            if (time_left == 0)
            {
                timer.Stop();
                Close();
                ShutDownNow();
            }
            labelTimeLeft.Text = String.Format("Izklop čez {0} {1}", time_left.ToString(), SecondsFormat(time_left));
        }

        private void ShutDownNow()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /f /t 0")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(psi);
        }
    }
}
