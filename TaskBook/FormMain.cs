using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using TaskBook.Interaction;

namespace TaskBook
{
    public partial class FormMain : MaterialForm
    {
        public static SqlConnection connection;

        public static readonly MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
        private readonly InteractionData interactionData = new InteractionData();
        private readonly InteractionTool interactionTool = new InteractionTool();
        private readonly Services services = new Services();

        private readonly FormRequest formRequest = new FormRequest();
        private readonly FormInfo formInfo = new FormInfo();
        private readonly FormLogin formLogin = new FormLogin();
        private readonly FormSettings formSettings = new FormSettings();

        public static bool SQLStat = false;
        public static string ver = "Ver. Alpha 0.3.0 T_B";

        public static bool flagUpdate = false;
        public static int n = 0;

        public FormMain()
        {
            Program.formMain = this;
            InitializeComponent();

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
                };
                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }).Start();

            Services.saveFileDialogBack.FileName = "TaskBook";
            Services.saveFileDialogBack.DefaultExt = ".bak";
            Services.saveFileDialogBack.Filter = "Bak files(*bak)|*bak";

            Services.openFileDialogSQL.Filter = "Sql files(*.sql)|*.sql| Text files(*.txt)|*.txt| All files(*.*)|*.*";
            Services.openFileDialogRes.Filter = "Bak files(*bak)|*bak";

            services.UpdateApp();
            services.Visibl();
            dataGridView1.Enabled = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (services.CheckConfig(null, null, null, null) != true)
                return;
            Interaction.Services.PanelLoad();
            if (services.Test() != true)
                return;
            toolStripStatusLabel2.Text = "Готово к работе";
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (services.Test() != true)
                return;
            if (services.LoginGuest() != true)
                return;

            services.ClearStr();
            services.Reload(comboBox.Text);
            services.comboBoxFilter(comboBox.Text);
            services.Visibl();
        }

        private void buttonAdd_Click(object sender, EventArgs e) => interactionData.AddAndUpdate("Add");

        private void buttonUpdate_Click(object sender, EventArgs e) => interactionData.AddAndUpdate("Update");

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (services.Test() != true)
                return;
            if (services.LoginGuest() != true)
                return;
            FormDeletedAndSearch formDeletedAndSearch = new FormDeletedAndSearch("sea");
            formDeletedAndSearch.ShowDialog();
        }

        private void buttonDeleted_Click(object sender, EventArgs e)
        {
            if (services.Test() != true)
                return;
            if (services.LoginAdmin() != true)
                return;
            FormDeletedAndSearch formDeletedAndSearch = new FormDeletedAndSearch("del");
            formDeletedAndSearch.ShowDialog();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (services.Test() != true)
                return;
            if (services.LoginGuest() != true)
                return;
            services.Reload(comboBox.Text);
        }

        private void buttonReconnection_Click(object sender, EventArgs e)
        {
            if (services.CheckConfig(null, null, null, null) != true)
                return;
            Interaction.Services.PanelLoad();
            if (SQLStat != false)
            {
                MessageBox.Show("Подключение к базе данных установленно", "Проверка подключения", MessageBoxButtons.OK);
                toolStripStatusLabel2.Text = "Готово к работе";
                if (FormLogin.Login != null)
                {
                    services.DataTable();
                    services.Visibl();
                    services.Reload(comboBox.Text);
                }
            }
            else
            {
                toolStripStatusLabel2.Text = $"Ошибка подключения к базе данных!";
                MessageBox.Show("Ошибка подключения к базе данных!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonInfo_Click(object sender, EventArgs e) => formInfo.ShowDialog();

        private void ButtonUpdateApp_Click(object sender, EventArgs e) => services.UpdateApp();

        private void выполнитьЗапросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (services.Test() != true)
                return;
            if (services.LoginAdmin() != true)
                return;
            flagUpdate = false;
            n = 0;
            dataGridView1.ClearSelection();
            formRequest.ShowDialog();
        }

        private void очиститьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            n = 0;
            dataGridView1.ClearSelection();
            interactionTool.очиститьБазуДанныхToolStripMenuItem();
        }

        private void создатьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.создатьРезервнуюКопиюToolStripMenuItem();

        private void восстановитьБазуДанныхToolStripMenuItem_Click(object sender, EventArgs e) => interactionTool.восстановитьБазуДанныхToolStripMenuItem();

        private void войтиКакАдминистраторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (services.Test() != true)
                return;
            formLogin.ShowDialog();
        }

        private void войтиКакГостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (services.Test() != true)
                return;
            if (FormLogin.Login != null)
            {
                MessageBox.Show("Вы уже вошли в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FormLogin.Login = "user";
            services.DataTable();
            services.Visibl();
            dataGridView1.Enabled = true;
            services.Reload(comboBox.Text);
            toolStripStatusLabel2.Text = $"Выполнен вход под логином {FormLogin.Login}";
            this.Text = $"Учёт задач штатного программиста - {FormLogin.Login}";
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
                comboBoxFilter.DataSource = null;
                services.Visibl();
                dataGridView1.DataSource = null;
                flagUpdate = false;
                dataGridView1.ClearSelection();
                dataGridView1.Enabled = false;
                toolStripStatusLabel2.Text = "Произведен выход из системы";
                return;
            }
            MessageBox.Show("Не выполнен вход в систему!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e) => formSettings.ShowDialog();

        private void dataGridView1_MouseDoubleClick(object sender, EventArgs e)
        {
            services.TextViewTextBox(services.ArrayUpdate());
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            flagUpdate = true;

            toolStripStatusLabel2.Text = $"Выбрана строка № {(dataGridView1.CurrentRow.Index + 1)}";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            n = dataGridView1.CurrentRow.Index;
        }

        public void buttonClearStr_Click(object sender, EventArgs e) => services.ClearStr();

        private void buttonFilter_Click(object sender, EventArgs e) => services.Filter(comboBox.Text, comboBoxFilter.Text);

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}