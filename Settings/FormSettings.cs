using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Settings
{
    public partial class FormSettings : MaterialForm
    {
        private readonly OpenFileDialog openFileDialog = new OpenFileDialog();
        private string pathDBmdf;
        private string pathDBldf;

        public FormSettings()
        {
            InitializeComponent();

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);

            foreach (var Label in this.Controls)
                if (Label is Label) (Label as Label).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            foreach (var Button in this.Controls)
                if (Button is Button) (Button as Button).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }

        private void textBoxSourceDBmdf_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "mdf file(*.mdf)|*.mdf";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            pathDBmdf = openFileDialog.FileName;
            textBoxSourceDBmdf.Text = pathDBmdf;
            textBoxNameDB.Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
            pathDBldf = Path.GetDirectoryName(openFileDialog.FileName).ToString() + "\\" + Path.GetFileNameWithoutExtension(openFileDialog.FileName).ToString() + "_log.ldf";
            textBoxSourceDBldf.Text = pathDBldf;
        }

        private void textBoxSourceDBldf_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "ldf file(*.ldf)|*.ldf";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            pathDBldf = openFileDialog.FileName;
            textBoxSourceDBldf.Text = pathDBldf;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (textBoxNameServer.Text == null || textBoxNameServer.Text.Length == 0) return;
            string connString = "Data Source=" + textBoxNameServer.Text + ";Integrated Security=True;Connect Timeout=1";
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                string sql = "USE [master] CREATE DATABASE[" + textBoxNameDB.Text + "] ON (FILENAME = N'" + pathDBmdf + "'), (FILENAME = N'" + pathDBldf + "' ) FOR ATTACH";
                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                MessageBox.Show("База данных " + textBoxNameDB.Text + " успешно добавленна!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void buttonDetech_Click(object sender, EventArgs e)
        {
            if (textBoxNameServer.Text == null || textBoxNameServer.Text.Length == 0) return;
            string connString = "Data Source=" + textBoxNameServer.Text + ";Integrated Security=True;Connect Timeout=1";
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                string sql = "USE [master] EXEC master.dbo.sp_detach_db @dbname = N'" + textBoxNameDB.Text + "'";
                using (SqlCommand sqlCommand = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                MessageBox.Show("База данных " + textBoxNameDB.Text + " успешно отсоеденина!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("SQLServerManager15.msc");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка зауска службы!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
