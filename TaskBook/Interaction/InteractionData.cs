                                                                                                                                                                                                         using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaskBook.Interaction
{
    class InteractionData
    {
        public void Add(string tabl)
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            try
            {
                FormMain.connection.Open();
                string sql = tabl + "Add";
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    switch (tabl)
                    {
                        case "Employee":
                            string[] EmployeeFIO = Program.formMain.textBox1.Text.Trim().Split();

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
                            break;
                        case "Programmer":
                            string[] ProgrammerFIO = Program.formMain.textBox13.Text.Trim().Split();

                            sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_SURNAME", SqlDbType.NChar, 25));
                            sqlCommand.Parameters["@PROGRAMMER_SURNAME"].Value = ProgrammerFIO[0];

                            sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_NAME", SqlDbType.NChar, 25));
                            sqlCommand.Parameters["@PROGRAMMER_NAME"].Value = ProgrammerFIO[1];
                            
                            if(ProgrammerFIO.Length > 2)
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
                            break;
                        case "Task":

                            break;
                        case "Departament":

                            break;
                        case "TaskDescription":

                            break;
                        case "Employee_Task":

                            break;
                        case "Task_Programmer":

                            break;
                        case "Autorization":
                            sqlCommand.Parameters.Add(new SqlParameter("@login", SqlDbType.NVarChar, 16));
                            sqlCommand.Parameters["@login"].Value = Program.formMain.textBox23.Text;

                            sqlCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 16));
                            sqlCommand.Parameters["@password"].Value = Program.formMain.textBox21.Text;

                            sqlCommand.Parameters.Add(new SqlParameter("@position", SqlDbType.NChar, 10));
                            sqlCommand.Parameters["@position"].Value = Program.formMain.comboBox1.Text;
                            break;
                    }
                    sqlCommand.ExecuteNonQuery();
                    Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
                    Program.formMain.Reload(Program.formMain.comboBox.Text);
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
                            string[] Employee = textBox.Text.Trim().Split();

                            sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_SURNAME", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@EMPLOYEE_SURNAME"].Value = Employee[0];

                            sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_NAME", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@EMPLOYEE_NAME"].Value = Employee[1];

                            sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_PATRONYMIC", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@EMPLOYEE_PATRONYMIC"].Value = Employee[2];
                            break;
                        case "Programmer":
                            string[] Programmer = textBox.Text.Trim().Split();

                            sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_SURNAME", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@PROGRAMMER_SURNAME"].Value = Programmer[0];

                            sqlCommand.Parameters.Add(new SqlParameter("@PROGRAMMER_NAME", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@PROGRAMMER_NAME"].Value = Programmer[1];

                            sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEE_PATRONYMIC", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@PROGRAMMER_PATRONYMIC"].Value = Programmer[2];
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