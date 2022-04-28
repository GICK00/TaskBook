using System.Diagnostics;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace TaskBook
{
    public partial class FormInfo : MaterialForm
    {
        public FormInfo()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
            richTextBox1.Text += Program.formMain.ver;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://t.me/gick85");

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/GICK00");

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/GICK00/TaskBook");
    }
}