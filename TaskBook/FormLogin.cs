using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

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

            foreach (var label in this.Controls)
                if (label is Label) (label as Label).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            foreach (var panel in this.Controls)
                if (panel is Panel) foreach (var label in (panel as Panel).Controls)
                        if (label is Label) (label as Label).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            foreach (var button in this.Controls)
                if (button is Button) (button as Button).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (Login != null)
            {
                MessageBox.Show("Вы уже вошли в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    Login = null;
                    string sql = "SELECT POSITION FROM [Autorization] WHERE LOGIN = '" + textBox1.Text + "' AND PASSWORD = '" + textBox2.Text + "'";
                    Autorization(sql);
                    if (Login != null)
                    {
                        Program.formMain.DataTable();
                        Program.formMain.Visibl();
                        Program.formMain.Reload(Program.formMain.comboBox.Text);

                        Program.formMain.toolStripStatusLabel2.Text = "Произведен вход под логином " + textBox1.Text;
                        Program.formMain.Text = "Учёт задач штатного программиста - " + textBox1.Text;
                        FormMain.materialSkinManager.AddFormToManage(this);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Нет пользователя с таким логином и паролем!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Введите логин и пароль!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Autorization(string sql)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    FormMain.connection.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        reader.Read();
                        Login = reader["POSITION"].ToString().Trim();
                        reader.Close();
                    }
                }
            }
            catch
            {
                Login = null;
            }
            finally
            {
                FormMain.connection.Close();
            }
        }
    }
}