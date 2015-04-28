using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MySql.Data.MySqlClient;

namespace FamBudg
{
    class ClientCommands
    {
        public static string connectionString = "Server=mysql87.1gb.ru;Database=gb_fam_budget;Uid=gb_fam_budget; Pwd=c122ac54nm;";
        public static bool sendRequest(string some_request = "") // послать запрос
        {
            /*donothing*/
            Thread.Sleep(1000);
            return false;
        }

        public static bool addСonsumption(double sum, int category_id, string comment, string some_request = "") // добавить расход
        {
            /* ВЫЗОВ ХРАНИМОЙ ПРОЦЕДУРЫ */
            //Initialize mysql connection

            MySqlConnection connection = new MySqlConnection(connectionString);

            /* REQUEST */
            string CommandText = "INSERT INTO costs (summ, category_id, comment) VALUES (@sum, @cat, @comment)";

            MySqlCommand insCommand = new MySqlCommand(CommandText, connection);
            connection.Open(); //Устанавливаем соединение с базой данных.
            insCommand.Parameters.AddWithValue("@sum", sum);
            insCommand.Parameters.AddWithValue("@cat", category_id);
            insCommand.Parameters.AddWithValue("@comment", comment);
            insCommand.ExecuteNonQuery();

            connection.Close(); //Обязательно закрываем соединение!

            /*donothing*/
            Thread.Sleep(1000);
            return false;
        }

        public static bool addIncome(double sum, int category_id, string comment, string some_request = "") // добавить расход
        {
            /* будет ВЫЗОВ ХРАНИМОЙ ПРОЦЕДУРЫ */
            /* а пока, выполнение запроса */
            //Initialize mysql connection

            MySqlConnection connection = new MySqlConnection(connectionString);

            /* REQUEST */
            string CommandText = "INSERT INTO income (summ, category_id, comment) VALUES (@sum, @cat, @comment)";

            MySqlCommand insCommand = new MySqlCommand(CommandText, connection);
            connection.Open(); //Устанавливаем соединение с базой данных.
            insCommand.Parameters.AddWithValue("@sum", sum);
            insCommand.Parameters.AddWithValue("@cat", category_id);
            insCommand.Parameters.AddWithValue("@comment", comment);
            insCommand.ExecuteNonQuery();

            connection.Close(); //Обязательно закрываем соединение!

            /*donothing*/
            Thread.Sleep(1000);
            return false;
        }

            void refreshCosts()
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                /* REQUEST */
                string CommandText = "SELECT * FROM costs";

                MySqlCommand selCommand = new MySqlCommand(CommandText, connection);
                connection.Open(); //Устанавливаем соединение с базой данных.
                selCommand.ExecuteReader();
                MySql.Data.MySqlClient.MySqlDataReader reader = selCommand.ExecuteReader();
                while (reader.Read())
                {
                    //string = reader[0];
                }
                connection.Close(); //Обязательно закрываем соединение!
            }

            void refreshIncomes()
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

            }
    }
}