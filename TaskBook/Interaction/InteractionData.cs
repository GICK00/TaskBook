using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskBook.Interaction
{
    class InteractionData
    {
        // Добавления и изменение данных в выбранную таблицу
        public void AddAndUpdate(string action)
        {
            string[] array = null;
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            if (action == "Update" && FormMain.flag == true)
                array = Program.formMain.ArrayUpdate();
            else if (action == "Update")
            {
                MessageBox.Show("Выберите обьект изменения", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                FormMain.connection.Open();
                string sql = Program.formMain.comboBox.Text + action;
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    switch (Program.formMain.comboBox.Text)
                    {
                        case "Employee":
                            string[] EmployeeFIO = Program.formMain.textBox1.Text.Trim().Split();
                            if (EmployeeFIO.Length < 2 || Program.formMain.textBox4.Text.Length == 0 || Program.formMain.textBox5.Text.Length == 0 || Program.formMain.textBox6.Text.Length == 0 || Program.formMain.textBox7.Text.Length == 0)
                            {
                                MessageBox.Show("Ошибка введенных данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка введенных данных!";
                                return;
                            }
                            else
                            {
                                if (action == "Update")
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                                    sqlCommand.Parameters["@ID"].Value = array[0];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_SURNAME", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@EMPLOYEE_SURNAME"].Value = EmployeeFIO[0];

                                sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_NAME", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@EMPLOYEE_NAME"].Value = EmployeeFIO[1];

                                if (EmployeeFIO.Length > 2)
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_PATRONYMIC", SqlDbType.NChar, 25));
                                    sqlCommand.Parameters["@EMPLOYEE_PATRONYMIC"].Value = EmployeeFIO[2];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_POSITION", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@EMPLOYEE_POSITION"].Value = Program.formMain.textBox4.Text;

                                sqlCommand.Parameters.Add(new SqlParameter("@DEPARTAMENT_ID", SqlDbType.Int));
                                sqlCommand.Parameters["@DEPARTAMENT_ID"].Value = Convert.ToInt32(Program.formMain.textBox5.Text);

                                sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_WORK_TELEPHONE", SqlDbType.NVarChar, 12));
                                sqlCommand.Parameters["@EMPLOYEE_WORK_TELEPHONE"].Value = Program.formMain.textBox6.Text;

                                sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_WORK_TIME", SqlDbType.NChar, 15));
                                sqlCommand.Parameters["@EMPLOYEE_WORK_TIME"].Value = Program.formMain.textBox7.Text;
                            }
                            break;
                        case "Programmer":
                            string[] ProgrammerFIO = Program.formMain.textBox13.Text.Trim().Split();

                            if (ProgrammerFIO.Length < 2 || Program.formMain.textBox11.Text.Length == 0 || Program.formMain.textBox10.Text.Length == 0 || Program.formMain.textBox2.Text.Length == 0)
                            {
                                MessageBox.Show("Ошибка введенных данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка введенных данных!";
                                return;
                            }
                            else
                            {
                                if (action == "Update")
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                                    sqlCommand.Parameters["@ID"].Value = array[0];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_SURNAME", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@PROGRAMMER_SURNAME"].Value = ProgrammerFIO[0];

                                sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_NAME", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@PROGRAMMER_NAME"].Value = ProgrammerFIO[1];

                                if (ProgrammerFIO.Length > 2)
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_PATRONYMIC", SqlDbType.NChar, 25));
                                    sqlCommand.Parameters["@PROGRAMMER_PATRONYMIC"].Value = ProgrammerFIO[2];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@DEPARTAMENT_ID", SqlDbType.Int));
                                sqlCommand.Parameters["@DEPARTAMENT_ID"].Value = Convert.ToInt32(Program.formMain.textBox11.Text);

                                sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_WORK_TELEPHONE", SqlDbType.NVarChar, 12));
                                sqlCommand.Parameters["@PROGRAMMER_WORK_TELEPHONE"].Value = Program.formMain.textBox10.Text;

                                sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_WORK_TIME", SqlDbType.NChar, 15));
                                sqlCommand.Parameters["@PROGRAMMER_WORK_TIME"].Value = Program.formMain.textBox2.Text;
                            }
                            break;
                        case "Task":
                            if (Program.formMain.textBox15.Text.Length == 0 || Program.formMain.textBox14.Text.Length == 0 || Program.formMain.textBox17.Text.Length == 0)
                            {
                                MessageBox.Show("Ошибка введенных данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка введенных данных!";
                                return;
                            }
                            else
                            {
                                if (action == "Update")
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                                    sqlCommand.Parameters["@ID"].Value = array[0];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@TASK_NAME", SqlDbType.NChar, 30));
                                sqlCommand.Parameters["@TASK_NAME"].Value = Program.formMain.textBox15.Text;

                                DateTime data = DateTime.Parse(Program.formMain.textBox14.Text);
                                sqlCommand.Parameters.Add(new SqlParameter("@TASK_DATA", SqlDbType.DateTime));
                                sqlCommand.Parameters["@TASK_DATA"].Value = data.ToString("dd-MM-yyyy HH:mm:ss");

                                if (Program.formMain.textBox12.Text.Length != 0)
                                {
                                    DateTime dataCompletion = DateTime.Parse(Program.formMain.textBox12.Text);
                                    sqlCommand.Parameters.Add(new SqlParameter("@TASK_COMPLETION_DATA", SqlDbType.DateTime));
                                    sqlCommand.Parameters["@TASK_COMPLETION_DATA"].Value = dataCompletion.ToString("dd-MM-yyyy HH:mm:ss");
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@TASK_DESCRIPTION", SqlDbType.NChar, 200));
                                sqlCommand.Parameters["@TASK_DESCRIPTION"].Value = Program.formMain.textBox17.Text;

                                if (Program.formMain.textBox16.Text.Length != 0)
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@TASK_STATUS", SqlDbType.NChar, 15));
                                    sqlCommand.Parameters["@TASK_STATUS"].Value = Program.formMain.textBox16.Text;
                                }
                            }
                            break;
                        case "Departament":
                            if (Program.formMain.textBox9.Text.Length == 0 || Program.formMain.textBox8.Text.Length == 0 || Program.formMain.textBox3.Text.Length == 0)
                            {
                                MessageBox.Show("Ошибка введенных данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка введенных данных!";
                                return;
                            }
                            else
                            {
                                if (action == "Update")
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                                    sqlCommand.Parameters["@ID"].Value = array[0];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@DEPARTAMENT_NAME", SqlDbType.NChar, 35));
                                sqlCommand.Parameters["@DEPARTAMENT_NAME"].Value = Program.formMain.textBox9.Text;

                                sqlCommand.Parameters.Add(new SqlParameter("@DEPARTAMENT_FLOOR", SqlDbType.NChar, 10));
                                sqlCommand.Parameters["@DEPARTAMENT_FLOOR"].Value = Program.formMain.textBox8.Text;

                                sqlCommand.Parameters.Add(new SqlParameter("@DEPARTAMENT_CABINET", SqlDbType.NChar, 10));
                                sqlCommand.Parameters["@DEPARTAMENT_CABINET"].Value = Program.formMain.textBox3.Text;
                            }
                            break;
                        case "Employee_Task":
                            if (Program.formMain.textBox25.Text.Length == 0 || Program.formMain.textBox22.Text.Length == 0)
                            {
                                MessageBox.Show("Ошибка введенных данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка введенных данных!";
                                return;
                            }
                            else
                            {
                                if (action == "Update")
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                                    sqlCommand.Parameters["@ID"].Value = array[0];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_ID", SqlDbType.Int));
                                sqlCommand.Parameters["@EMPLOYEE_ID"].Value = Convert.ToInt32(Program.formMain.textBox25.Text);

                                sqlCommand.Parameters.Add(new SqlParameter("@TASK_ID", SqlDbType.Int));
                                sqlCommand.Parameters["@TASK_ID"].Value = Convert.ToInt32(Program.formMain.textBox22.Text);
                            }
                            break;
                        case "Task_Programmer":
                            if (Program.formMain.textBox20.Text.Length == 0 || Program.formMain.textBox19.Text.Length == 0)
                            {
                                MessageBox.Show("Ошибка введенных данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка введенных данных!";
                                return;
                            }
                            else
                            {
                                if (action == "Update")
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                                    sqlCommand.Parameters["@ID"].Value = array[0];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@TASK_ID", SqlDbType.Int));
                                sqlCommand.Parameters["@TASK_ID"].Value = Convert.ToInt32(Program.formMain.textBox20.Text);

                                sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_ID", SqlDbType.Int));
                                sqlCommand.Parameters["@PROGRAMMER_ID"].Value = Convert.ToInt32(Program.formMain.textBox19.Text);
                            }
                            break;
                        case "Autorization":
                            if (Program.formMain.textBox23.Text.Length == 0 || Program.formMain.textBox21.Text.Length == 0)
                            {
                                MessageBox.Show("Ошибка введенных данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка введенных данных!";
                                return;
                            }
                            else
                            {
                                if (action == "Update")
                                {
                                    sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                                    sqlCommand.Parameters["@id"].Value = array[0];
                                }

                                sqlCommand.Parameters.Add(new SqlParameter("@login", SqlDbType.NVarChar, 16));
                                sqlCommand.Parameters["@login"].Value = Program.formMain.textBox23.Text;

                                sqlCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 16));
                                sqlCommand.Parameters["@password"].Value = Program.formMain.textBox21.Text;

                                sqlCommand.Parameters.Add(new SqlParameter("@position", SqlDbType.NChar, 10));
                                sqlCommand.Parameters["@position"].Value = Program.formMain.comboBox1.Text;
                            }
                            break;
                    }
                    sqlCommand.ExecuteNonQuery();
                    if (action == "Update")
                    {
                        Program.formMain.toolStripStatusLabel2.Text = "Данные изменены";
                        FormMain.flag = false;
                    }
                    else
                        Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
                    FormMain.connection.Close();
                    Program.formMain.Reload(Program.formMain.comboBox.Text);
                }
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
                FormMain.connection.Close();
            }
        }

        // Поиск и вывод данных из выбранной таблицы
        public void Search(string sql, TextBox textBox, out bool res)
        {
            res = false;
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginGuest() != true) return;
            using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
            {
                try
                {
                    switch (Program.formMain.comboBox.Text)
                    {
                        case "Employee":
                            string[] EmployeeFIO= textBox.Text.Trim().Split();

                            sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_SURNAME", SqlDbType.NChar, 25));
                            sqlCommand.Parameters["@EMPLOYEE_SURNAME"].Value = EmployeeFIO[0];

                            sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_NAME", SqlDbType.NChar, 25));
                            sqlCommand.Parameters["@EMPLOYEE_NAME"].Value = EmployeeFIO[1];

                            if (EmployeeFIO.Length > 2)
                            {
                                sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_PATRONYMIC", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@EMPLOYEE_PATRONYMIC"].Value = EmployeeFIO[2];
                            }
                            else
                            {
                                sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_PATRONYMIC", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@EMPLOYEE_PATRONYMIC"].Value = DBNull.Value;
                            }
                            break;
                        case "Programmer":
                            string[] ProgrammerFIO = textBox.Text.Trim().Split();

                            sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_SURNAME", SqlDbType.NChar, 25));
                            sqlCommand.Parameters["@PROGRAMMER_SURNAME"].Value = ProgrammerFIO[0];

                            sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_NAME", SqlDbType.NChar, 25));
                            sqlCommand.Parameters["@PROGRAMMER_NAME"].Value = ProgrammerFIO[1];

                            if (ProgrammerFIO.Length > 2)
                            {
                                sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_PATRONYMIC", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@PROGRAMMER_PATRONYMIC"].Value = ProgrammerFIO[2];
                            }
                            else
                            {
                                sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_PATRONYMIC", SqlDbType.NChar, 25));
                                sqlCommand.Parameters["@PROGRAMMER_PATRONYMIC"].Value = DBNull.Value;
                            }
                            break;
                        case "Task":
                            sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@Name"].Value = textBox.Text;
                            break;
                        case "Departament":
                            sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@Name"].Value = textBox.Text;
                            break;
                        case "TaskDescription":
                            sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                            sqlCommand.Parameters["@ID"].Value = Convert.ToInt32(textBox.Text);
                            break;
                        case "Employee_Task":
                            sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                            sqlCommand.Parameters["@ID"].Value = Convert.ToInt32(textBox.Text);
                            break;
                        case "Task_Programmer":
                            sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                            sqlCommand.Parameters["@ID"].Value = Convert.ToInt32(textBox.Text);
                            break;
                        case "Autorization":
                            sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@Name"].Value = textBox.Text;
                            break;
                    }
                    FormMain.connection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        Program.formMain.dataGridView1.DataSource = dataTable;
                        dataReader.Close();
                    }
                    Program.formMain.toolStripStatusLabel2.Text = "Поиск выполнен";
                    res = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
                }
                finally
                {
                    FormMain.connection.Close();
                }
            }
        }
    }
}