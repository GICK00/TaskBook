using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Settings
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static FontFamily RobotoRegular;
        [STAThread]
        static void Main()
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            var fontdataRoboto_Regular = Properties.Resources.Roboto_Regular;
            var lengthRoboto_Regular = fontdataRoboto_Regular.Length;
            var unsafeDataRoboto_Regular = Marshal.AllocCoTaskMem(lengthRoboto_Regular);
            Marshal.Copy(fontdataRoboto_Regular, 0, unsafeDataRoboto_Regular, lengthRoboto_Regular);
            fontCollection.AddMemoryFont(unsafeDataRoboto_Regular, lengthRoboto_Regular);
            Marshal.FreeCoTaskMem(unsafeDataRoboto_Regular);
            RobotoRegular = fontCollection.Families[0];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormSettings());
        }
    }
}
