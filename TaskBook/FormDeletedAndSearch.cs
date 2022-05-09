using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskBook
{
    public partial class FormDeletedAndSearch : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
        private readonly string type;
        public FormDeletedAndSearch(string t)
        {
            type = t;
            InitializeComponent();

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey300, Primary.Grey900, Primary.Grey200, Accent.LightBlue200, TextShade.BLACK);

            foreach (var label in this.Controls)
                if (label is Label) (label as Label).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            foreach (var panel in this.Controls)
                if (panel is Panel) foreach (var label in (panel as Panel).Controls)
                        if (label is Label) (label as Label).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            foreach (var button in this.Controls)
                if (button is Button) (button as Button).Font = new System.Drawing.Font(Program.RobotoRegular, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            switch (type)
            {
                case "del":
                    this.Text = "Удаление";
                    label1.Text = "Укажите номер в таблицы для удаление данных.";
                    break;
                case "sea":
                    this.Text = "Поиск";
                    label1.Text = "Укажите данные для поиска.";
                    break;
            }
        }

        public void buttonOk_Click(object sender, EventArgs e)
        {
            switch (type)
            {
                case "del":
                    Deleted();
                    break;
                case "sea":
                    Search();
                    break;
            }
        }

        private void Deleted()
        {
            try
            {
                FormMain.connection.Open();
                string sql = "DELETE FROM " + Program.formMain.comboBox.Text + " WHERE " + Program.formMain.comboBox.Text.ToUpper() + "_ID = @ID";
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                    sqlCommand.Parameters["@ID"].Value = Convert.ToInt32(this.textBox1.Text);

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные удалены из таблицы " + Program.formMain.comboBox.Text;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        private void Search()
        {
            string sql = null;
            switch (Program.formMain.comboBox.Text)
            {
                case "Employee":
                    sql = "SELECT * FROM Employee WHERE EMPLOYEE_SURNAME = @EMPLOYEE_SURNAME AND EMPLOYEE_NAME = @EMPLOYEE_NAME AND EMPLOYEE_PATRONYMIC = @EMPLOYEE_PATRONYMIC";
                    break;
                case "Programmer":
                    sql = "SELECT * FROM Programmer WHERE PROGRAMMER_SURNAME = @PROGRAMMER_SURNAME AND PROGRAMMER_NAME = @PROGRAMMER_NAME AND EMPLOYEE_PATRONYMIC = @EMPLOYEE_PATRONYMIC";
                    break;
                case "Task":
                    sql = "SELECT * FROM Task WHERE TASK_NAME = @Name";
                    break;
                case "Departament":
                    sql = "SELECT * FROM Departament WHERE DEPARTAMENT_NAME = @Name";
                    break;
                case "TaskDescription":
                    sql = "SELECT * FROM TaskDescription WHERE TASKDESCRIPTION_ID = @ID";
                    break;
                case "Employee_Task":
                    sql = "SELECT * FROM Employee_Task WHERE EMPLOYEE_TASK_ID = @ID";
                    break;
                case "Task_Programmer":
                    sql = "SELECT * FROM Task_Programmer WHERE TASK_PROGRAMMER_ID = @ID";
                    break;
                case "Autorization":
                    sql = "SELECT * FROM Autorization WHERE LOGIN = @Name";
                    break;
            }
            Interaction.InteractionData interactionData = new Interaction.InteractionData();
            interactionData.Search(sql, textBox1, out bool res);
            if (res == true) this.Close();
        }
    }
}
