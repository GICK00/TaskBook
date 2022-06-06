using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TaskBook.Interaction;

namespace TaskBook
{
    public partial class FormRequest : MaterialForm
    {
        private readonly Services services = new Services();

        public FormRequest()
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

                    this.toolStripButtonOpenSQL.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    foreach (var button in this.Controls)
                        if (button is Button) (button as Button).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    foreach (var statusItem in statusStrip1.Items)
                        if (statusItem is ToolStripStatusLabel) (statusItem as ToolStripStatusLabel).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                };

                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }).Start();
        }

        private void toolStripButtonOpenSQL_Click(object sender, EventArgs e)
        {
            if (Services.openFileDialogSQL.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                string filename = Services.openFileDialogSQL.FileName;
                string sql = System.IO.File.ReadAllText(filename, Encoding.GetEncoding(1251));
                textBoxSQLReader.Text = sql;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Запрос не выполнен! Ошибка {ex.Message}";
                Program.formMain.toolStripStatusLabel2.Text = $"Запрос не выполнен! Ошибка {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(textBoxSQLReader.Text, FormMain.connection))
                {
                    FormMain.connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                toolStripStatusLabel2.Text = "Запрос выполнен";
                Program.formMain.toolStripStatusLabel2.Text = "Запрос выполнен";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                services.Reload(Program.formMain.comboBox.Text);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e) => this.Close();

        private void FormRequest_Load(object sender, EventArgs e) => toolStripStatusLabel2.Text = "";
    }
}