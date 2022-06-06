using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TaskBook
{
    public partial class FormSettings : MaterialForm
    {
        private readonly Interaction.Services services = new Interaction.Services();

        public FormSettings()
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
                };

                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }).Start();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            services.CheckConfig(null, null, null, null);
            string[] mas = new string[4];
            StreamReader streamReader = new StreamReader(Interaction.Services.path);
            for (int i = 0; i < mas.Length; i++)
            {
                string str = streamReader.ReadLine();
                string strNew = str.Replace(";", null).Split(' ').Last();
                mas[i] = strNew;
            }
            streamReader.Close();
            textBox1.Text = mas[0];
            textBox4.Text = mas[1];
            textBox5.Text = mas[2];
            textBox6.Text = mas[3];
        }

        private void buttonSaves_Click(object sender, EventArgs e)
        {
            if (services.CheckConfig(textBox1.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim()) != true)
                return;
            File.WriteAllText(Interaction.Services.path, $"Data Source = {textBox1.Text};\r\nInitial Catalog = {textBox4.Text};\r\nIntegrated Security = {textBox5.Text};\r\nConnect Timeout = {textBox6.Text};");
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Параметры успешно сохранены.", "Настройки", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.OK)
                this.Close();
        }

        private void buttonSettingsExe_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Settings.exe");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка зауска службы!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}