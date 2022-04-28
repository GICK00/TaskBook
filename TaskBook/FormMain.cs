using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace TaskBook
{
    public partial class FormMain : MaterialForm
    {
        public string ver = "Ver. Alpha 0.1.0 T_B";

        public static SqlConnection connection = new SqlConnection(Properties.Settings.Default.connString);
        readonly Interaction.InteractionData interactionData = new Interaction.InteractionData();
        readonly Interaction.InteractionTool interactionTool = new Interaction.InteractionTool();
        readonly WebClient client = new WebClient();

        public SaveFileDialog saveFileDialogBack = new SaveFileDialog();
        public OpenFileDialog openFileDialogSQL = new OpenFileDialog();
        public OpenFileDialog openFileDialogRes = new OpenFileDialog();
        
        public static bool SQLStat = true;

        public FormMain()
        {
            Program.formMain = this;
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);

            saveFileDialogBack.FileName = "Travel_Company";
            saveFileDialogBack.DefaultExt = ".bak";
            saveFileDialogBack.Filter = "Bak files(*bak)|*bak";
            
            openFileDialogSQL.Filter = "Sql files(*.sql)|*.sql| Text files(*.txt)|*.txt| All files(*.*)|*.*";
            openFileDialogRes.Filter = "Bak files(*bak)|*bak";
            UpdateApp();
            DataTable();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            PanelLoad();
            Visibl();
            if (Test() != true) return;
            Reload(comboBox.Text);
            toolStripStatusLabel2.Text = "Готово к работе";
        }

        private void DataTable()
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

        private void Visibl()
        {
            foreach (var ctrl in this.Controls)
                if (ctrl is Panel) (ctrl as Panel).Visible = false;
            panelDefault.Visible = true;
            switch (comboBox.Text)
            {
                case "Tourist":
                    panelTourist.Visible = true;
                    break;
                case "TourOperator":
                    panelTourOperator.Visible = true;
                    break;
                case "TravelAgency":
                    panelTravelAgency.Visible = true;
                    break;
                case "Excursion":
                    panelExcursion.Visible = true;
                    break;
                case "Provides":
                    panelProvides.Visible = true;
                    break;
                case "Service":
                    panelService.Visible = true;
                    break;
                case "Promotes":
                    panelPromotes.Visible = true;
                    break;
                default:
                    break;
            }
            toolStripStatusLabel2.Text = "Выбрана таблица " + comboBox.Text;
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (Test() != true) return;
            if (LoginGuest() != true) return;
            Reload(comboBox.Text);
        }

        private void buttonAddTourist_Click(object sender, EventArgs e) => interactionData.buttonAddTourist();

        private void buttonSearchTourist_Click(object sender, EventArgs e)
        {
            const string sql = "SELECT * FROM Tourist WHERE TOURIST_SURNAEM = @TOURIST_SURNAEM AND TOURIST_NAME = @TOURIST_NAME AND TOURIST_PATRONYMIC = @TOURIST_PATRONYMIC";
            interactionData.Search(sql);
        }

        private void buttonAddExcursion_Click(object sender, EventArgs e) => interactionData.buttonAddExcursion();

        private void buttonSearchExcursion_Click(object sender, EventArgs e)
        {
            const string sql = "SELECT * FROM Excursion WHERE EXCURSION_NAME = @Name";
            interactionData.Search(sql);
        }

        private void buttonAddTravelAgency_Click(object sender, EventArgs e) => interactionData.buttonAddTravelAgency();

        private void buttonSearchTravelAgency_Click(object sender, EventArgs e)
        {
            const string sql = "SELECT * FROM TravelAgency WHERE TRAVELAGENCY_NAME = @Name";
            interactionData.Search(sql);
        }

        private void buttonAddlTourOperator_Click(object sender, EventArgs e) => interactionData.buttonAddlTourOperator();

        private void buttonSearchTourOperator_Click(object sender, EventArgs e)
        {
            const string sql = "SELECT * FROM TourOperator WHERE TOUROPERATOR_NAME = @Name";
            interactionData.Search(sql);
        }

        private void buttonAddService_Click(object sender, EventArgs e) => interactionData.buttonAddService();

        private void buttonSearchService_Click(object sender, EventArgs e)
        {
            const string sql = "SELECT * FROM Service WHERE SERVICE_ID = @ID";
            interactionData.Search(sql);               
        }

        private void buttonAddProvides_Click(object sender, EventArgs e) => interactionData.buttonAddProvides();

        private void buttonSearchProvides_Click(object sender, EventArgs e)
        {
            const string sql = "SELECT * FROM Provides WHERE PROVIDES_ID = @ID";
            interactionData.Search(sql);
        }

        private void buttonAddPromotes_Click(object sender, EventArgs e) => interactionData.buttonAddPromotes();

        private void buttonSearchPromotes_Click(object sender, EventArgs e)
        {
            const string sql = "SELECT * FROM Promotes WHERE PROMOTES_ID = @ID";
            interactionData.Search(sql);
        }

        private void buttonReconnection_Click(object sender, EventArgs e)
        {
            PanelLoad();
            if (SQLStat != false)
            {
                MessageBox.Show("Подключение к базе данных установленно", "Проверка подключения", MessageBoxButtons.OK);
                DataTable();
                Visibl();
                Reload(comboBox.Text);
                toolStripStatusLabel2.Text = "Готово к работе";
            }
            else
            {
                toolStripStatusLabel2.Text = $"Ошибка подключения к базе данных!";
                MessageBox.Show("Ошибка подключения к базе данных!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleted_Click(object sender, EventArgs e)
        {
            if (Test() != true) return;
            if (LoginAdmin() != true) return;
            FormDeleted formDeleted = new FormDeleted();
            formDeleted.ShowDialog();
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
            FormLogin.Login = "guest";
            toolStripStatusLabel2.Text = "Выполнен вход под логином " + FormLogin.Login;
            this.Text = "Туристическая компания - Гость";
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormLogin.Login != null)
            {
                FormLogin.Login = null;
                toolStripStatusLabel2.Text = "Произведен выход из системы";
                this.Text = "Туристическая компания";
                return;
            }
            MessageBox.Show("Не выполнен вход в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);   
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) e.Cancel = true;
        }

        private void PanelLoad()
        {
            FormLoad formLoad = new FormLoad(null, null);
            formLoad.progressBar.Value = 0;
            formLoad.ShowDialog();
        }

        public bool LoginGuest()
        {
            if (FormLogin.Login != null)
            {
                if (FormLogin.Login == "guest") return true;
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
            catch (Exception)
            {
            }
        }

        private void buttonClearStr_Click(object sender, EventArgs e)
        {
            foreach (var con in this.Controls)
                if (con is Panel) foreach (var pan in (con as Panel).Controls)
                        if (pan is TextBox) (pan as TextBox).Clear();
        }
    }
}