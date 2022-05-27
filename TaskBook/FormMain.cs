using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

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
        private readonly FormSettings formSettings = new FormSettings();
        private readonly FormInfo formInfo = new FormInfo();
        private readonly FormLogin formLogin = new FormLogin();
        private readonly FormRequest formRequest = new FormRequest();

        public SaveFileDialog saveFileDialogBack = new SaveFileDialog();
        public OpenFileDialog openFileDialogSQL = new OpenFileDialog();
        public OpenFileDialog openFileDialogRes = new OpenFileDialog();

        public static bool SQLStat = false;
        public static string ver = "Ver. Alpha 0.3.0 T_B";

        public static bool flag = false;
        private int n = 0;

        public FormMain()
        {
            InitializeComponent();
            Program.formMain = this;

            new Thread(() => 
            {
                Action action = () =>
                {
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
                };

                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }).Start();
            
            UpdateApp();
            Visibl();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (CheckConfig(null, null, null, null) != true)
                return;
            PanelLoad();
            if (Test() != true)
                return;
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Test() != true)
                return;
            if (LoginGuest() != true)
                return;
            flag = false;
            ClearStr();
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

        private void buttonAdd_Click(object sender, EventArgs e) => interactionData.AddAndUpdate("Add");

        private void buttonUpdate_Click(object sender, EventArgs e) => interactionData.AddAndUpdate("Update");

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (Test() != true)
                return;
            if (LoginGuest() != true)
                return;
            FormDeletedAndSearch formDeletedAndSearch = new FormDeletedAndSearch("sea");
            formDeletedAndSearch.ShowDialog();
        }

        private void buttonDeleted_Click(object sender, EventArgs e)
        {
            if (Test() != true)
                return;
            if (LoginAdmin() != true)
                return;
            FormDeletedAndSearch formDeletedAndSearch = new FormDeletedAndSearch("del");
            formDeletedAndSearch.ShowDialog();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (Test() != true) 
                return;
            if (LoginGuest() != true) 
                return;
            Reload(comboBox.Text);
        }

        private void buttonReconnection_Click(object sender, EventArgs e)
        {
            if (CheckConfig(null, null, null, null) != true) 
                return;
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

        private void ButtonInfo_Click(object sender, EventArgs e) => formInfo.ShowDialog();

        private void ButtonUpdateApp_Click(object sender, EventArgs e) => UpdateApp();

        private void выполнитьЗапросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.formMain.Test() != true) 
                return;
            if (Program.formMain.LoginAdmin() != true) 
                return;
            formRequest.ShowDialog();
        }

        private void очиститьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.очиститьБазуДанныхToolStripMenuItem();

        private void создатьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.создатьРезервнуюКопиюToolStripMenuItem();

        private void восстановитьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.восстановитьБазуДанныхToolStripMenuItem();

        private void войтиКакАдминистраторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Test() != true) 
                return;
            formLogin.ShowDialog();
        }

        private void войтиКакГостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Test() != true) 
                return;
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

        private void toolStripButtonSettings_Click(object sender, EventArgs e) => formSettings.ShowDialog();

        private void dataGridView1_MouseDoubleClick(object sender, EventArgs e)
        {
            TextViewTextBox(ArrayUpdate());
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            flag = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            n = dataGridView1.CurrentRow.Index;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }

        // Служит для получения списка всех таблиц в БД и выгрузки их в textBox
        public void DataTable()
        {
            if (SQLStat != true) 
                return;
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

        // Отвечает за отображение панелий с элементами соответсвующими каждой таблице
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

        // Вызов панели загрузки
        public static void PanelLoad()
        {
            FormLoad formLoad = new FormLoad(null, null);
            formLoad.progressBar.Value = 0;
            formLoad.ShowDialog();
        }

        // Проверка под какой учетной записью вошел пользователь
        public bool LoginGuest()
        {
            if (FormLogin.Login != null)
            {
                if (FormLogin.Login == "user") 
                    return true;
                return true;
            }
            MessageBox.Show("Вы не вошли в систему!\r\nВойдите в систему во вкладке Пользователи.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        // Проверка под какой учетной записью вошел пользователь
        public bool LoginAdmin()
        {
            if (FormLogin.Login != null)
            {
                if (FormLogin.Login == "admin") 
                    return true;
                MessageBox.Show("Вы не являетесь Администратором!", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            MessageBox.Show("Вы не вошли в систему!\r\nВойдите в систему во вкладке Пользователи.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        // Выполнение проверки соединения с сервером с БД
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

        // Служит для обновления данных в dataGridView1
        public void Reload(string comboBox)
        {
            flag = false;
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
            dataGridView1.ClearSelection();
            dataGridView1.FirstDisplayedScrollingRowIndex = n;
            dataGridView1.Rows[n].Selected = true;
        }

        // Проверка файла конфигурации и их применения 
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
                return true;
            }
        }

        // Проверка версий приложения которое находится на GitHub
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

        private void buttonClearStr_Click(object sender, EventArgs e) => ClearStr();

        // Очиста всех полей ввода данных на главной форме
        private void ClearStr()
        {
            foreach (var panelDefault in panelDefault.Controls)
                if (panelDefault is Panel)
                    foreach (var panData in (panelDefault as Panel).Controls)
                        if (panData is Panel)
                        {
                            foreach (var panTextBox in (panData as Panel).Controls)
                                if (panTextBox is TextBox) (panTextBox as TextBox).Clear();
                        }
                        else if (panData is ComboBox) (panData as ComboBox).Text = null;
        }

        // Получение массива данных из строки dataGridView1
        public string[] ArrayUpdate()
        {
            int index = dataGridView1.CurrentRow.Index;
            string[] array = new string[dataGridView1.ColumnCount];
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                array[i] = dataGridView1.Rows[index].Cells[i].Value.ToString();
            }
            return array;
        }

        // Выгрузка данных в textBox из массива для их изменения
        private void TextViewTextBox(string[] array)
        {
            switch (comboBox.Text)
            {
                case "Employee":
                    textBox1.Text = $"{array[1].Trim()} {array[2].Trim()} {array[3].Trim()}";
                    textBox4.Text = array[4];
                    textBox5.Text = array[5];
                    textBox6.Text = array[6];
                    textBox7.Text = array[7];
                    break;
                case "Programmer":
                    textBox13.Text = $"{array[1].Trim()} {array[2].Trim()} {array[3].Trim()}";
                    textBox11.Text = array[4];
                    textBox10.Text = array[5];
                    textBox2.Text = array[6];
                    break;
                case "Task":
                    textBox15.Text = array[1];
                    textBox14.Text = array[2];
                    textBox12.Text = array[3];
                    textBox17.Text = array[4];
                    textBox16.Text = array[5];
                    break;
                case "Departament":
                    textBox9.Text = array[1];
                    textBox8.Text = array[2];
                    textBox3.Text = array[3];
                    break;
                case "Employee_Task":
                    textBox25.Text = array[1];
                    textBox22.Text = array[2];
                    break;
                case "Task_Programmer":
                    textBox20.Text = array[1];
                    textBox19.Text = array[2];
                    break;
                case "Autorization":
                    textBox23.Text = array[1];
                    textBox21.Text = array[2];
                    comboBox1.Text = array[3];
                    break;
            }
        }
    }
}