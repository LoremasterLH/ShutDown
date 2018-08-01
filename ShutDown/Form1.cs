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
        private int time_left = 7;  // Default value for creating the .cfg file.
        private Timer timer;
        public Form1()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = 1000;

            if (!System.IO.File.Exists(file_name))  // If we don't want to have a config file, simply leave this if clause blank and the application will use the default value.
            {
                System.IO.File.WriteAllText(file_name, time_left.ToString());
            }
            else
            {
                try
                {
                    string time = System.IO.File.ReadAllText(file_name);
                    time_left = Int32.Parse(time);
                }
                catch
                {
                    MessageBox.Show(String.Format("The file '{0}' is not properly formatted. Falling back to default value of {1} seconds. Countdown starts when this window is closed.", file_name, time_left));
                }
            }

            timer.Start();

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
