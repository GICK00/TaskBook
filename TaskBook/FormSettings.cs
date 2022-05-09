using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace TaskBook
{
    public partial class FormSettings : MaterialForm
    {
        public FormSettings()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            Program.formMain.CheckConfig(null, null, null, null);
            string[] mas = new string[4];
            StreamReader streamReader = new StreamReader(FormMain.path);
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
            if (Program.formMain.CheckConfig(textBox1.Text.Trim(), textBox4.Text.Trim(), textBox5.Text.Trim(), textBox6.Text.Trim()) != true) return;
            File.WriteAllText(FormMain.path, $"Data Source = {textBox1.Text};\r\nInitial Catalog = {textBox4.Text};\r\nIntegrated Security = {textBox5.Text};\r\nConnect Timeout = {textBox6.Text};");
            MessageBox.Show("Параметры успешно сохранены.");
        }
    }
}