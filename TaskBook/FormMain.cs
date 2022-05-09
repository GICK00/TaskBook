using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;
using System.Reflection;

namespace TaskBook
{
    public partial class FormMain : MaterialForm
    {        
        public static string path;
        private static StreamReader streamReader;
        public string connSrring;
        public static SqlConnection connection;

        public static readonly MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
        private readonly Interaction.InteractionData interactionData = new Interaction.InteractionData();
        private readonly Interaction.InteractionTool interactionTool = new Interaction.InteractionTool();
        private readonly WebClient client = new WebClient();

        public SaveFileDialog saveFileDialogBack = new SaveFileDialog();
        public OpenFileDialog openFileDialogSQL = new OpenFileDialog();
        public OpenFileDialog openFileDialogRes = new OpenFileDialog();

        public static bool SQLStat = false;
        public string ver = "Ver. Alpha 0.2.0 T_B";

        public FormMain()
        {
            Program.formMain = this;
            InitializeComponent();

            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);

            this.ButtonReconnectionBD.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonReload.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            foreach (var toolItem in toolStrip1.Items)
                if (toolItem is ToolStripDropDownButton) (toolItem as ToolStripDropDownButton).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            foreach (var statusItem in statusStrip1.Items)
                if (statusItem is ToolStripStatusLabel) (statusItem as ToolStripStatusLabel).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            foreach (var panel in panel1.Controls)
                if (panel is Button) (panel as Button).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControl1.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panelBackround.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label30.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            saveFileDialogBack.FileName = "TaskBook";
            saveFileDialogBack.DefaultExt = ".bak";
            saveFileDialogBack.Filter = "Bak files(*bak)|*bak";

            openFileDialogSQL.Filter = "Sql files(*.sql)|*.sql| Text files(*.txt)|*.txt| All files(*.*)|*.*";
            openFileDialogRes.Filter = "Bak files(*bak)|*bak";

            UpdateApp();
            Visibl();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (CheckConfig(null, null, null, null) != true) return;
            PanelLoad();
            if (Test() != true) return;
        }

        public void DataTable()
        {
            if (SQLStat != true) return;
            try
            {
                const string sql = "SELECT name FROM sys.objects WHERE type in (N'U')";
                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        List<string> names = new List<string>();
                        foreach (DataRow row in dataTable.Rows)
                            names.Add(row["name"].ToString());
                        names.Remove("sysdiagrams");
                        if (FormLogin.Login != "admin") 
                            names.Remove("Autorization");
                        comboBox.DataSource = names;
                        dataReader.Close();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                connection.Close();
            }
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Test() != true) return;
            if (LoginGuest() != true) return;
            try
            {
                Reload(comboBox.Text);
                Visibl();
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
        }

        public void Visibl()
        {
            foreach (var ctrl in panelDefault.Controls)
                if (ctrl is Panel) (ctrl as Panel).Visible = false;
            panelBackround.Visible = true;
            panelDefault.Visible = true;
            switch (comboBox.Text)
            {
                case "Employee":
                    panelEmployee.Visible = true;
                    break;
                case "Programmer":
                    panelProgrammer.Visible = true;
                    break;
                case "Task":
                    panelTask.Visible = true;
                    break;
                case "Departament":
                    panelDepartament.Visible = true;
                    break;
                case "TaskDescription":
                    panelTaskDescription.Visible = true;
                    break;
                case "Employee_Task":
                    panelEmployee_Task.Visible = true;
                    break;
                case "Task_Programmer":
                    panelTask_Programmer.Visible = true;
                    break;
                case "Autorization":
                    panelAutorization.Visible = true;
                    break;
            }
            toolStripStatusLabel2.Text = "Выбрана таблица " + comboBox.Text;
        }

        private void buttonAdd_Click(object sender, EventArgs e) => interactionData.Add(comboBox.Text);

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (Test() != true) return;
            if (LoginGuest() != true) return;
            FormDeletedAndSearch formDeletedAndSearch = new FormDeletedAndSearch("sea");
            formDeletedAndSearch.ShowDialog();
        }

        private void buttonDeleted_Click(object sender, EventArgs e)
        {
            if (Test() != true) return;
            if (LoginAdmin() != true) return;
            FormDeletedAndSearch formDeletedAndSearch = new FormDeletedAndSearch("del");
            formDeletedAndSearch.ShowDialog();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (Test() != true) return;
            if (LoginGuest() != true) return;
            Reload(comboBox.Text);
        }

        private void buttonReconnection_Click(object sender, EventArgs e)
        {
            if (CheckConfig(null, null, null, null) != true) return;
            PanelLoad();
            if (SQLStat != false)
            {
                MessageBox.Show("Подключение к базе данных установленно", "Проверка подключения", MessageBoxButtons.OK);
                toolStripStatusLabel2.Text = "Готово к работе";
                if (FormLogin.Login != null)
                {
                    DataTable();
                    Visibl();
                    Reload(comboBox.Text);
                }
            }
            else
            {
                toolStripStatusLabel2.Text = $"Ошибка подключения к базе данных!";
                MessageBox.Show("Ошибка подключения к базе данных!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonInfo_Click(object sender, EventArgs e)
        {
            FormInfo formInfo = new FormInfo();
            formInfo.ShowDialog();
        }

        private void ButtonUpdateApp_Click(object sender, EventArgs e) => UpdateApp();

        private void выполнитьЗапросToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.выполнитьЗапросToolStripMenuItem();

        private void очиститьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.очиститьБазуДанныхToolStripMenuItem();

        private void создатьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.создатьРезервнуюКопиюToolStripMenuItem();

        private void восстановитьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.восстановитьБазуДанныхToolStripMenuItem();

        private void войтиКакАдминистраторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Test() != true) return;
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog();
        }

        private void войтиКакГостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Test() != true) return;
            if (FormLogin.Login != null)
            {
                MessageBox.Show("Вы уже вошли в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormLogin.Login = "user";
            DataTable();
            Visibl();
            Reload(comboBox.Text);
            toolStripStatusLabel2.Text = "Выполнен вход под логином " + FormLogin.Login;
            this.Text = "Учёт задач штатного программиста - " + FormLogin.Login;
            materialSkinManager.AddFormToManage(this);
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormLogin.Login != null)
            {
                FormLogin.Login = null;
                this.Text = "Учёт задач штатного программиста";
                materialSkinManager.AddFormToManage(this);
                comboBox.DataSource = null;
                Visibl();
                dataGridView1.DataSource = null;
                toolStripStatusLabel2.Text = "Произведен выход из системы";
                return;
            }
            MessageBox.Show("Не выполнен вход в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) e.Cancel = true;
        }

        public static void PanelLoad()
        {
            FormLoad formLoad = new FormLoad(null, null);
            formLoad.progressBar.Value = 0;
            formLoad.ShowDialog();
        }

        public bool LoginGuest()
        {
            if (FormLogin.Login != null)
            {
                if (FormLogin.Login == "user") return true;
                return true;
            }
            MessageBox.Show("Вы не вошли в систему!\r\nВойдите в систему во вкладке Пользователи.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public bool LoginAdmin()
        {
            if (FormLogin.Login != null)
            {
                if (FormLogin.Login == "admin") return true;
                MessageBox.Show("Вы не являетесь Администратором!", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            MessageBox.Show("Вы не вошли в систему!\r\nВойдите в систему во вкладке Пользователи.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public bool Test()
        {
            if (SQLStat != true)
            {
                toolStripStatusLabel2.Text = $"Ошибка подключения к базе данных!";
                MessageBox.Show("Ошибка подключения к базе данных!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public void Reload(string comboBox)
        {
            string sql = "SELECT * FROM " + comboBox;
            using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    dataGridView1.DataSource = dataTable;
                    dataReader.Close();
                }
                connection.Close();
            }
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Index;
        }

        public bool CheckConfig(string arg1, string arg2, string arg3, string arg4)
        {
            path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).ToString() + "\\config.ini";
            if (File.Exists(path) != true)
            {
                MessageBox.Show("Файл конфигурации отсуствует! Будет создан новый файл шаблон в корне программы.", "Критическая ошибка конфигурации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Create(path).Close();
                File.WriteAllText(path, $"Data Source = {arg1};\r\nInitial Catalog = {arg2};\r\nIntegrated Security = {arg3};\r\nConnect Timeout = {arg4};");
                streamReader = new StreamReader(path);
                connSrring = streamReader.ReadToEnd();
                streamReader.Close();
                connection = new SqlConnection(connSrring);
                toolStripStatusLabel2.Text = "Критическая ошибка конфигурации!";
                return false;
            }
            else
            {
                streamReader = new StreamReader(path);
                connSrring = streamReader.ReadToEnd();
                streamReader.Close();
                connection = new SqlConnection(connSrring);
                toolStripStatusLabel2.Text = "Готово к работе";
                return true;
            }
        }

        private void UpdateApp()
        {
            try
            {
                Uri uri = new Uri("https://github.com/GICK00/TaskBook/blob/main/Ver.txt");
                if (client.DownloadString(uri).Contains(ver))
                {
                    toolStripStatusLabel2.Text = "Устновленна послденяя версия приложения " + ver;
                    return;
                }
                else
                {
                    string text = "Доступна новая версия приложения.\r\nВаша текущая версия." + ver + "\r\nОбновить программу?";
                    DialogResult result = MessageBox.Show(text, "Достуно новое обновление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://github.com/GICK00/TaskBook");
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Ошибка проверки обновлений! {ex.Message}";
            }
        }

        private void buttonClearStr_Click(object sender, EventArgs e)
        {
            foreach (var panelDefault in panelDefault.Controls)
                if (panelDefault is Panel) foreach (var panData in (panelDefault as Panel).Controls)
                        if (panData is Panel)
                        {
                            foreach (var panTextBox in (panData as Panel).Controls)
                                if (panTextBox is TextBox) (panTextBox as TextBox).Clear();
                        }
                        else if (panData is ComboBox) (panData as ComboBox).Text = null;
        }
    }
}