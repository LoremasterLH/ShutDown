/**
 * @author  LoremasterLH
 * @date    1.8.2018
 * @email   loremasterlittlehero@gmail.com
 * @git     https://github.com/LoremasterLH
 **/

using System;
using System.Windows.Forms;

namespace ShutDownPC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
