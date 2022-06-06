using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskBook.Interaction
{
    class InteractionTool
    {
        private readonly Services services = new Services();

        // Очищает все таблицы БД от данных кроме таблицы Autorization (она необходима для авторизации в приложении). 
        public void очиститьБазуДанныхToolStripMenuItem()
        {
            if (services.Test() != true)
                return;
            if (services.LoginAdmin() != true)
                return;
            DialogResult result = MessageBox.Show("Вы уверены, что хотите очистить базу данных?", "Удаление данных.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            using (SqlCommand sqlCommand = new SqlCommand("DeletedAll", FormMain.connection))
            {
                FormMain.connection.Open();
                sqlCommand.ExecuteNonQuery();
                FormMain.connection.Close();
            }
            services.Reload(Program.formMain.comboBox.Text);
            services.ClearStr();
            Program.formMain.toolStripStatusLabel2.Text = "База данных очищенна";
        }

        // Создает полную резерную копию всей БД.
        public void создатьРезервнуюКопиюToolStripMenuItem()
        {
            if (services.Test() != true)
                return;
            if (services.LoginAdmin() != true)
                return;
            if (Services.saveFileDialogBack.ShowDialog() == DialogResult.Cancel)
                return;

            if (services.GetBDSettings() == null)
                return;
            List<string> settings = services.GetBDSettings();

            string path = Services.saveFileDialogBack.FileName;
            string sql = $@"BACKUP DATABASE[{settings[1]}] TO DISK = N'{path}' WITH NOFORMAT, NOINIT, NAME = N'{settings[1]}-Полная База данных Резервное копирование', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            FormLoad formLoad = new FormLoad(sql, "back");
            formLoad.ShowDialog();
        }

        // Восстанавливает БД из выбранной резервной копии.
        public void восстановитьБазуДанныхToolStripMenuItem()
        {
            if (services.Test() != true)
                return;
            if (services.LoginAdmin() != true)
                return;
            DialogResult result = MessageBox.Show("Вы уверены, что хотите востановить базу данных?", "Восстановление базы данных.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            if (Services.openFileDialogRes.ShowDialog() == DialogResult.Cancel)
                return;

            if (services.GetBDSettings() == null)
                return;
            List<string> settings = services.GetBDSettings();

            string path = Services.openFileDialogRes.FileName;
            string sql = $@"USE master RESTORE DATABASE [{settings[1]}] FROM  DISK = N'{path}' WITH REPLACE, FILE = 1,  NOUNLOAD,  STATS = 5";
            FormLoad formLoad = new FormLoad(sql, "res");
            formLoad.ShowDialog();
        }
    }
}
