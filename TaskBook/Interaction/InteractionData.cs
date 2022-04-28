using System;
using System.Data;
using System.Data.SqlClient;

namespace TaskBook.Interaction
{
    class InteractionData
    {
        public void buttonAddTourist()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            try
            {
                FormMain.connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("TouristAdd", FormMain.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    string[] FIO = Program.formMain.textBox1.Text.Split();

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_SURNAEM", SqlDbType.NChar, 30));
                    sqlCommand.Parameters["@TOURIST_SURNAEM"].Value = FIO[0];

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_NAME", SqlDbType.NChar, 30));
                    sqlCommand.Parameters["@TOURIST_NAME"].Value = FIO[1];

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_PATRONYMIC", SqlDbType.NChar, 30));
                    sqlCommand.Parameters["@TOURIST_PATRONYMIC"].Value = FIO[2];

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_SEX", SqlDbType.NVarChar, 1));
                    sqlCommand.Parameters["@TOURIST_SEX"].Value = Program.formMain.textBox2.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_COUNTRY", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TOURIST_COUNTRY"].Value = Program.formMain.textBox4.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_REGION", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TOURIST_REGION"].Value = Program.formMain.textBox3.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_INDEX", SqlDbType.NVarChar, 6));
                    sqlCommand.Parameters["@TOURIST_INDEX"].Value = Program.formMain.textBox5.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_CITY", SqlDbType.NChar, 20));
                    sqlCommand.Parameters["@TOURIST_CITY"].Value = Program.formMain.textBox6.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_STREET", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TOURIST_STREET"].Value = Program.formMain.textBox19.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_HOME", SqlDbType.NChar, 20));
                    sqlCommand.Parameters["@TOURIST_HOME"].Value = Program.formMain.textBox20.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_TELEPHONE", SqlDbType.NVarChar, 12));
                    sqlCommand.Parameters["@TOURIST_TELEPHONE"].Value = Program.formMain.textBox18.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_EXCURSION_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@TRAVELAGENCY_EXCURSION_ID"].Value = Convert.ToInt32(Program.formMain.textBox23.Text);

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_DISCONT", SqlDbType.Int));
                    sqlCommand.Parameters["@TOURIST_DISCONT"].Value = Program.formMain.textBox45.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_PASPORT_DATA", SqlDbType.NChar, 150));
                    sqlCommand.Parameters["@TOURIST_PASPORT_DATA"].Value = Program.formMain.textBox21.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_VISA", SqlDbType.NChar, 150));
                    sqlCommand.Parameters["@TOURIST_VISA"].Value = Program.formMain.textBox22.Text;

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        public void buttonAddExcursion()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            try
            {
                FormMain.connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("ExcursionAdd", FormMain.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_NAME", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@EXCURSION_NAME"].Value = Program.formMain.textBox8.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_COUNTRY", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@EXCURSION_COUNTRY"].Value = Program.formMain.textBox9.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_REGION", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@EXCURSION_REGION"].Value = Program.formMain.textBox10.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_CITY ", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@EXCURSION_CITY "].Value = Program.formMain.textBox11.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_DATA", SqlDbType.Date));
                    sqlCommand.Parameters["@EXCURSION_DATA"].Value = Program.formMain.textBox12.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_DURATION", SqlDbType.NChar, 12));
                    sqlCommand.Parameters["@EXCURSION_DURATION"].Value = Program.formMain.textBox13.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_SERVISE", SqlDbType.NChar, 25));
                    sqlCommand.Parameters["@EXCURSION_SERVISE"].Value = Program.formMain.textBox14.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_NUMBER_PLACES", SqlDbType.Int));
                    sqlCommand.Parameters["@EXCURSION_NUMBER_PLACES"].Value = Program.formMain.textBox15.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_MOVING", SqlDbType.NChar, 25));
                    sqlCommand.Parameters["@EXCURSION_MOVING"].Value = Program.formMain.textBox16.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_PRICE", SqlDbType.Money));
                    sqlCommand.Parameters["@EXCURSION_PRICE"].Value = Program.formMain.textBox17.Text;

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        public void buttonAddTravelAgency()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            try
            {
                FormMain.connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("TravelAgencyAdd", FormMain.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_NAME", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TRAVELAGENCY_NAME"].Value = Program.formMain.textBox33.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_COUNTRY", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TRAVELAGENCY_COUNTRY"].Value = Program.formMain.textBox32.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_REGION", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TRAVELAGENCY_REGION"].Value = Program.formMain.textBox31.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_INDEX", SqlDbType.NVarChar, 6));
                    sqlCommand.Parameters["@TRAVELAGENCY_INDEX"].Value = Program.formMain.textBox30.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_CITY", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TRAVELAGENCY_CITY"].Value = Program.formMain.textBox25.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_STREET", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TRAVELAGENCY_STREET"].Value = Program.formMain.textBox24.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_HOME", SqlDbType.NChar, 15));
                    sqlCommand.Parameters["@TRAVELAGENCY_HOME"].Value = Program.formMain.textBox28.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_OFFICE", SqlDbType.NChar, 15));
                    sqlCommand.Parameters["@TRAVELAGENCY_OFFICE"].Value = Program.formMain.textBox27.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_TELEPHONE", SqlDbType.NVarChar, 12));
                    sqlCommand.Parameters["@TRAVELAGENCY_TELEPHONE"].Value = Program.formMain.textBox29.Text;

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        public void buttonAddlTourOperator()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            try
            {
                FormMain.connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("TourOperatorAdd", FormMain.connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_NAME", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TOUROPERATOR_NAME"].Value = Program.formMain.textBox41.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_COUNTRY", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TOUROPERATOR_COUNTRY"].Value = Program.formMain.textBox40.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_REGION", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TOUROPERATOR_REGION"].Value = Program.formMain.textBox39.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_INDEX", SqlDbType.NVarChar, 6));
                    sqlCommand.Parameters["@TOUROPERATOR_INDEX"].Value = Program.formMain.textBox38.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_CITY", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TOUROPERATOR_CITY"].Value = Program.formMain.textBox34.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_STREET", SqlDbType.NChar, 50));
                    sqlCommand.Parameters["@TOUROPERATOR_STREET"].Value = Program.formMain.textBox26.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_HOME", SqlDbType.NVarChar, 15));
                    sqlCommand.Parameters["@TOUROPERATOR_HOME"].Value = Program.formMain.textBox36.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_OFFICE", SqlDbType.NChar, 15));
                    sqlCommand.Parameters["@TOUROPERATOR_OFFICE"].Value = Program.formMain.textBox35.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_TELEPHONE", SqlDbType.NVarChar, 12));
                    sqlCommand.Parameters["@TOUROPERATOR_TELEPHONE"].Value = Program.formMain.textBox37.Text;

                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_PERCENT", SqlDbType.Int));
                    sqlCommand.Parameters["@TOUROPERATOR_PERCENT"].Value = Convert.ToInt32(Program.formMain.textBox42.Text);

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        public void buttonAddService()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            try
            {
                FormMain.connection.Open();
                const string sql = "INSERT INTO Service (TRAVELAGENCY_ID, EXCURSION_ID, SERVICE_EXCURSION_SOLD) VALUES (@TRAVELAGENCY_ID, @EXCURSION_ID, @SERVICE_EXCURSION_SOLD) SELECT SCOPE_IDENTITY()";
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@TRAVELAGENCY_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@TRAVELAGENCY_ID"].Value = Convert.ToInt32(Program.formMain.textBox48.Text);

                    sqlCommand.Parameters.Add(new SqlParameter("@EXCURSION_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@EXCURSION_ID"].Value = Convert.ToInt32(Program.formMain.textBox52.Text);

                    sqlCommand.Parameters.Add(new SqlParameter("@SERVICE_EXCURSION_SOLD", SqlDbType.Int));
                    sqlCommand.Parameters["@SERVICE_EXCURSION_SOLD"].Value = Convert.ToInt32(Program.formMain.textBox43.Text);

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        public void buttonAddProvides()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            try
            {
                FormMain.connection.Open();
                const string sql = "INSERT INTO Provides (TOUROPERATOR_ID, SERVICE_ID) VALUES (@TOUROPERATOR_ID, @SERVICE_ID) SELECT SCOPE_IDENTITY()";
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@TOUROPERATOR_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@TOUROPERATOR_ID"].Value = Convert.ToInt32(Program.formMain.textBox49.Text);

                    sqlCommand.Parameters.Add(new SqlParameter("@SERVICE_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@SERVICE_ID"].Value = Convert.ToInt32(Program.formMain.textBox50.Text);

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        public void buttonAddPromotes()
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginAdmin() != true) return;
            try
            {
                FormMain.connection.Open();
                const string sql = "INSERT INTO Promotes (SERVICE_ID, TOURIST_ID) VALUES (@SERVICE_ID, @TOURIST_ID) SELECT SCOPE_IDENTITY()";
                using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@SERVICE_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@SERVICE_ID"].Value = Convert.ToInt32(Program.formMain.textBox53.Text);

                    sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@TOURIST_ID"].Value = Convert.ToInt32(Program.formMain.textBox51.Text);

                    sqlCommand.ExecuteNonQuery();
                }
                Program.formMain.toolStripStatusLabel2.Text = "Данные добавлены";
            }
            catch (Exception ex)
            {
                Program.formMain.toolStripStatusLabel2.Text = $"Ошибка! {ex.Message}";
            }
            finally
            {
                FormMain.connection.Close();
                Program.formMain.Reload(Program.formMain.comboBox.Text);
            }
        }

        public void Search(string sql)
        {
            if (Program.formMain.Test() != true) return;
            if (Program.formMain.LoginGuest() != true) return;
            using (SqlCommand sqlCommand = new SqlCommand(sql, FormMain.connection))
            {
                try
                {
                    switch (Program.formMain.comboBox.Text)
                    {
                        case "Tourist":
                            string[] FIO = Program.formMain.textBox1.Text.Split();

                            sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_SURNAEM", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@TOURIST_SURNAEM"].Value = FIO[0];

                            sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_NAME", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@TOURIST_NAME"].Value = FIO[1];

                            sqlCommand.Parameters.Add(new SqlParameter("@TOURIST_PATRONYMIC", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@TOURIST_PATRONYMIC"].Value = FIO[2];
                            break;
                        case "TourOperator":
                            sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@Name"].Value = Program.formMain.textBox41.Text;
                            break;
                        case "TravelAgency":
                            sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@Name"].Value = Program.formMain.textBox33.Text;
                            break;
                        case "Excursion":
                            sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NChar, 50));
                            sqlCommand.Parameters["@Name"].Value = Program.formMain.textBox8.Text;
                            break;
                        case "Provides":
                            sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                            sqlCommand.Parameters["@ID"].Value = Convert.ToInt32(Program.formMain.textBox46.Text);
                            break;
                        case "Service":
                            sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                            sqlCommand.Parameters["@ID"].Value = Convert.ToInt32(Program.formMain.textBox44.Text);
                            break;
                        case "Promotes":
                            sqlCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                            sqlCommand.Parameters["@ID"].Value = Convert.ToInt32(Program.formMain.textBox47.Text);
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
        }
    }
}