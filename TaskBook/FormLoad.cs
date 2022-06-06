using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskBook
{
    public partial class FormLoad : Form
    {
        private readonly Interaction.Services services = new Interaction.Services();

        private string sqlLoad;
        private string typeLoad;

        public FormLoad(string sql, string type)
        {
            InitializeComponent();

            new Thread(() =>
            {
                Action action = () =>
                {
                    this.label1.Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    sqlLoad = sql;
                    typeLoad = type;
                    if (typeLoad == "res")
                        this.label1.Text = "Восстановление...";
                    else if (typeLoad == "back")
                        this.label1.Text = "Создание резерной копии...";
                    else if (typeLoad == "update")
                        this.label1.Text = "Проверка версии...";
                };

                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
            }).Start();
        }

        private void FormLoad_Load(object sender, EventArgs e)
        {
            Selection();
            progressBar.Value += 50;
        }

        private async void Selection()
        {
            if (sqlLoad == null)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand($"USE {FormMain.connection.Database.ToString()}", FormMain.connection))
                        {
                            FormMain.connection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                        FormMain.SQLStat = true;
                    }
                    catch (Exception)
                    {
                        FormMain.SQLStat = false;
                    }
                    finally
                    {
                        Task.Delay(1000);
                        FormMain.connection.Close();
                    }
                });
                progressBar.Value += 50;
                await Task.Delay(500);
            }
            else if (typeLoad == "res")
            {
                await Task.Run(() =>
                {
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(sqlLoad, FormMain.connection))
                        {
                            FormMain.connection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                        Task.Delay(500);
                        Program.formMain.toolStripStatusLabel2.Text = "База данных успешно востановленна";
                    }
                    catch (Exception ex)
                    {
                        Program.formMain.toolStripStatusLabel2.Text = $"Ошибка востановления базы данных! {ex.Message}";
                    }
                    finally
                    {
                        FormMain.connection.Close();
                    }
                });
                progressBar.Value += 50;
                services.Reload(Program.formMain.comboBox.Text);
                await Task.Delay(500);
            }
            else if (typeLoad == "back")
            {
                await Task.Run(() =>
                {
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(sqlLoad, FormMain.connection))
                        {
                            FormMain.connection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                        Task.Delay(500);
                        Program.formMain.toolStripStatusLabel2.Text = "Резерная копия успешно создана и сохранена";
                    }
                    catch (Exception ex)
                    {
                        Program.formMain.toolStripStatusLabel2.Text = $"Ошибка сохранения резервной копии базы данных! {ex.Message}";
                    }
                    finally
                    {
                        FormMain.connection.Close();
                    }
                });
                progressBar.Value += 50;
                await Task.Delay(1000);
            }
            this.Close();
        }
    }
}