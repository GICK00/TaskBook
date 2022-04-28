using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace TaskBook
{
    public partial class FormLogin : MaterialForm
    {
        public static string Login;
        public FormLogin()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (Login != null)
            {
                MessageBox.Show("Вы уже вошли в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (textBox1.Text != "admin" | textBox2.Text != "admin")
            {
                MessageBox.Show("Нет администратора с таким логином и паролем!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                Login = textBox1.Text;
                Program.formMain.toolStripStatusLabel2.Text = "Произведен вход под логином " + textBox1.Text;
                Program.formMain.Text = "Туристическая компания - Администратор";
                this.Close();
            }
        }
    }
}