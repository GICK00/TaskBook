using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace TaskBook
{
    public partial class FormInfo : MaterialForm
    {
        public FormInfo()
        {
            InitializeComponent();

            new Thread(() =>
            {
                Action action = () =>
                {
                    var materialSkinManager = MaterialSkinManager.Instance;
                    materialSkinManager.AddFormToManage(this);
                    materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
                    this.richTextBox1.Font = new System.Drawing.Font(Program.RobotoRegular, 9.75F, System.Drawing.FontStyle.Regular);
                    richTextBox1.Text += FormMain.ver;
                };

                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }).Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://t.me/gick85");

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/GICK00");

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/GICK00/TaskBook");
    }
}