using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TaskBook
{
    static class Program
    {
        public static FormMain formMain;
        public static FontFamily RobotoRegular;
        [STAThread]
        static void Main()
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            var fontdataRoboto_Regular = Properties.Resources.Roboto_Regular;
            var fontdataRoboto_Bold = Properties.Resources.Roboto_Bold;
            var lengthRoboto_Regular = fontdataRoboto_Regular.Length;
            var lengthRoboto_Bold = fontdataRoboto_Bold.Length;
            var unsafeDataRoboto_Regular = Marshal.AllocCoTaskMem(lengthRoboto_Regular);
            var unsafeDataRoboto_Bold = Marshal.AllocCoTaskMem(lengthRoboto_Bold);
            Marshal.Copy(fontdataRoboto_Regular, 0, unsafeDataRoboto_Regular, lengthRoboto_Regular);
            Marshal.Copy(fontdataRoboto_Bold, 0, unsafeDataRoboto_Bold, lengthRoboto_Bold);
            fontCollection.AddMemoryFont(unsafeDataRoboto_Regular, lengthRoboto_Regular);
            fontCollection.AddMemoryFont(unsafeDataRoboto_Bold, lengthRoboto_Bold);
            Marshal.FreeCoTaskMem(unsafeDataRoboto_Regular);
            Marshal.FreeCoTaskMem(unsafeDataRoboto_Bold);
            RobotoRegular = fontCollection.Families[0];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
