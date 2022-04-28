using System;
using System.Windows.Forms;

namespace TaskBook
{
    static class Program
    {
        public static FormMain formMain;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
