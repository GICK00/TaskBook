using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace TaskBook.Interaction
{
    internal class Services
    {
        public static string path;
        private static StreamReader streamReader;
        public string connSrring;

        private readonly WebClient client = new WebClient();
              
        public SaveFileDialog saveFileDialogBack = new SaveFileDialog();
        public OpenFileDialog openFileDialogSQL = new OpenFileDialog();
        public OpenFileDialog openFileDialogRes = new OpenFileDialog();

        // Получения списка всех таблиц в БД.
        // Удаление из этого списка системной диаграммы и таблицы Autorization,
        // если пользователь не является администратором.
        // Выгрузка списка в comboBox.
        public void DataTable()
        {
            if (FormMain.SQLStat != true)
                return;
            try
            {
                const string sql = "SELECT name FROM sys.objects WHERE type in (N'U')";
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    FormMain.connection.Open();
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
                        Program.formMain.comboBox.DataSource = names;
                        dataReader.Close();
                    }
                    FormMain.connection.Close();
                }
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
            }
        }

        // Отображение и скрытие панелий с элементами соответсвующими каждой таблице
        public void Visibl()
        {
            foreach (var ctrl in Program.formMain.panelDefault.Controls)
                if (ctrl is Panel) (ctrl as Panel).Visible = false;
            Program.formMain.panelBackround.Visible = true;
            Program.formMain.panelDefault.Visible = true;
            switch (Program.formMain.comboBox.Text)
            {
                case "Employee":
                    Program.formMain.panelEmployee.Visible = true;
                    break;
                case "Programmer":
                    Program.formMain.panelProgrammer.Visible = true;
                    break;
                case "Task":
                    Program.formMain.panelTask.Visible = true;
                    break;
                case "Departament":
                    Program.formMain.panelDepartament.Visible = true;
                    break;
                case "Employee_Task":
                    Program.formMain.panelEmployee_Task.Visible = true;
                    break;
                case "Task_Programmer":
                    Program.formMain.panelTask_Programmer.Visible = true;
                    break;
                case "Autorization":
                    Program.formMain.panelAutorization.Visible = true;
                    break;
            }
            Program.formMain.toolStripStatusLabel2.Text = "Выбрана таблица " + Program.formMain.comboBox.Text;
        }

        // Вызов панели загрузки (просто упращение кода).
        public static void PanelLoad()
        {
            FormLoad formLoad = new FormLoad(null, null);
            formLoad.progressBar.Value = 0;
            formLoad.ShowDialog();
        }

        // Проверка поля login пользователя на соответсвие логину гостя.
        // Гость может только выполнять просмотр и поиск записей в таблицах БД через приложение.
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

        // Проверка поля login пользователя на соответсвие логину администратора.
        // Администратор исеет все права для взаимодействия с БД через приложение.
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

        // Проверяет поля SQLStat на bool значение для вывода соответвующих уведомлений
        // для пользоватля.
        public bool Test()
        {
            if (FormMain.SQLStat != true)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка подключения к базе данных!";
                MessageBox.Show("Ошибка подключения к базе данных!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Выводит содержимое таблицы d dataGridView1 которые находятся в БД таблица
        // определяется пользователем или находится по стандарту в comboBox.
        // Вызов обновляет данные в dataGridView1 и сбрасывает выделенную строку.
        public void Reload(string comboBox)
        {
            string sql = "SELECT * FROM " + comboBox;
            using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
            {
                FormMain.connection.Open();
                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    Program.formMain.dataGridView1.DataSource = dataTable;
                    dataReader.Close();
                }
                FormMain.connection.Close();
            }
            FormMain.flagUpdate = false;
            FormMain.n = 0;
            Program.formMain.dataGridView1.ClearSelection();
        }

        // Проверяет файл конфигурации на доступность. Если файл не найден то создается новый файл,
        // в который записывается стандартный тип записи данного файла конфигурации.
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
                FormMain.connection = new SqlConnection(connSrring);
                Program.formMain.toolStripStatusLabel2.Text = "Критическая ошибка конфигурации!";
                return false;
            }
            else
            {
                streamReader = new StreamReader(path);
                connSrring = streamReader.ReadToEnd();
                streamReader.Close();
                FormMain.connection = new SqlConnection(connSrring);
                return true;
            }
        }

        // Выполняет загрузку текстового файла Var.txt находящегося на GitHub.
        // Выводит соответсвующте уведомления пользователю.
        public void UpdateApp()
        {
            try
            {
                Uri uri = new Uri("https://github.com/GICK00/TaskBook/blob/main/Ver.txt");
                if (client.DownloadString(uri).Contains(FormMain.ver))
                {
                    Program.formMain.toolStripStatusLabel2.Text = "Устновленна послденяя версия приложения " + FormMain.ver;
                    return;
                }
                else
                {
                    string text = "Доступна новая версия приложения.\r\nВаша текущая версия." + FormMain.ver + "\r\nОбновить программу?";
                    DialogResult dialogResult = MessageBox.Show(text, "Достуно новое обновление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://github.com/GICK00/TaskBook");
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка проверки обновлений! {ex.Message}";
            }
        }

        // Получает массив разбытых данных из строки которая была выделенна.
        public string[] ArrayUpdate()
        {
            int index = Program.formMain.dataGridView1.CurrentRow.Index;
            string[] array = new string[Program.formMain.dataGridView1.ColumnCount];
            for (int i = 0; i < Program.formMain.dataGridView1.ColumnCount; i++)
                array[i] = Program.formMain.dataGridView1.Rows[index].Cells[i].Value.ToString();
            return array;
        }

        // Выгружает массив разбытых данных по соответсвующем TextBox.
        public void TextViewTextBox(string[] array)
        {
            switch (Program.formMain.comboBox.Text)
            {
                case "Employee":
                    Program.formMain.textBox1.Text = $"{array[1].Trim()} {array[2].Trim()} {array[3].Trim()}";
                    Program.formMain.textBox4.Text = array[4];
                    Program.formMain.textBox5.Text = array[5];
                    Program.formMain.textBox6.Text = array[6];
                    Program.formMain.textBox7.Text = array[7];
                    break;
                case "Programmer":
                    Program.formMain.textBox13.Text = $"{array[1].Trim()} {array[2].Trim()} {array[3].Trim()}";
                    Program.formMain.textBox11.Text = array[4];
                    Program.formMain.textBox10.Text = array[5];
                    Program.formMain.textBox2.Text = array[6];
                    break;
                case "Task":
                    Program.formMain.textBox15.Text = array[1];
                    Program.formMain.textBox14.Text = array[2];
                    Program.formMain.Text = array[3];
                    Program.formMain.Text = array[4];
                    Program.formMain.textBox16.Text = array[5];
                    break;
                case "Departament":
                    Program.formMain.textBox9.Text = array[1];
                    Program.formMain.textBox8.Text = array[2];
                    Program.formMain.textBox3.Text = array[3];
                    break;
                case "Employee_Task":
                    Program.formMain.textBox25.Text = array[1];
                    Program.formMain.textBox22.Text = array[2];
                    break;
                case "Task_Programmer":
                    Program.formMain.textBox20.Text = array[1];
                    Program.formMain.textBox19.Text = array[2];
                    break;
                case "Autorization":
                    Program.formMain.textBox23.Text = array[1];
                    Program.formMain.textBox21.Text = array[2];
                    Program.formMain.comboBox1.Text = array[3];
                    break;
            }
        }

        // Очищяает все TextBox начиная с panelDefault и все включенные в нее Controls.
        public void ClearStr()
        {
            foreach (var panelDefault in Program.formMain.panelDefault.Controls)
                if (panelDefault is Panel)
                    foreach (var panData in (panelDefault as Panel).Controls)
                        if (panData is Panel)
                        {
                            foreach (var panTextBox in (panData as Panel).Controls)
                                if (panTextBox is TextBox) (panTextBox as TextBox).Clear();
                        }
                        else if (panData is ComboBox) (panData as ComboBox).Text = null;
        }
    }
}