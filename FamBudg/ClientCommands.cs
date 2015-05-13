using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace FamBudg
{
    class ClientCommands
    {
        public static string connectionString = "Server=mysql87.1gb.ru;Database=gb_fam_budget;Uid=gb_fam_budget; Pwd=c122ac54nm;Convert Zero Datetime=True;CharSet=utf8;";
        public static bool sendRequest(string some_request = "") // послать запрос
        {
            /*donothing*/
            //Thread.Sleep(50);
            return false;
        }

        public static bool addСonsumption(double sum, string category, string comment, string some_request = "") // добавить расход
        {
            /* ВЫЗОВ ХРАНИМОЙ ПРОЦЕДУРЫ */
            //Initialize mysql connection

            MySqlConnection connection = new MySqlConnection(connectionString);

            /* REQUEST */
            string CommandText = "INSERT INTO costs (summ, category_id, comment) VALUES (@sum, (SELECT id FROM categories WHERE name = @cat), @comment)";

            MySqlCommand insCommand = new MySqlCommand(CommandText, connection);
            connection.Open(); //Устанавливаем соединение с базой данных.
            insCommand.Parameters.AddWithValue("@sum", sum);
            insCommand.Parameters.AddWithValue("@cat", category);
            insCommand.Parameters.AddWithValue("@comment", comment);
            try
            {
                insCommand.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close(); //Обязательно закрываем соединение!
            }
            /*donothing*/
            Thread.Sleep(50);
            return false;
        }

        public static bool addIncome(double sum, string category, string comment, string some_request = "") // добавить расход
        {
            /* будет ВЫЗОВ ХРАНИМОЙ ПРОЦЕДУРЫ */
            /* а пока, выполнение запроса */
            //Initialize mysql connection

            MySqlConnection connection = new MySqlConnection(connectionString);

            /* REQUEST */
            string CommandText = "INSERT INTO income (summ, category_id, comment) VALUES (@sum, (SELECT id FROM categories WHERE name = @cat), @comment)";

            MySqlCommand insCommand = new MySqlCommand(CommandText, connection);
            connection.Open(); //Устанавливаем соединение с базой данных.
            insCommand.Parameters.AddWithValue("@sum", sum);
            insCommand.Parameters.AddWithValue("@cat", category);
            insCommand.Parameters.AddWithValue("@comment", comment);
            try
            {
                insCommand.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close(); //Обязательно закрываем соединение!
            }
            /*donothing*/
            Thread.Sleep(50);
            return false;
        }

        public static bool addCategory(string name, string descr, string some_request = "") // добавить расход
        {
            /* будет ВЫЗОВ ХРАНИМОЙ ПРОЦЕДУРЫ */
            /* а пока, выполнение запроса */
            //Initialize mysql connection

            MySqlConnection connection = new MySqlConnection(connectionString);

            /* REQUEST */
            string CommandText = "INSERT INTO categories (name, descr) VALUES (@name, @descr)";

            MySqlCommand insCommand = new MySqlCommand(CommandText, connection);
            connection.Open(); //Устанавливаем соединение с базой данных.
            insCommand.Parameters.AddWithValue("@name", name);
            insCommand.Parameters.AddWithValue("@descr", descr);
            try
            {
                insCommand.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Используйте уникальное имя категории."); // <- удобно
                /*throw new System.Exception(
                     "Используйте уникальное имя категории.");*/ // <- правильно
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close(); //Обязательно закрываем соединение!
                }
            }
            /*donothing*/
            Thread.Sleep(50);
            return false;
        }


            public static BindingSource refreshIncomes()
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                /* REQUEST */
                string CommandText = "SELECT i.id as Номер, i.date as Дата, i.summ as Сумма, ca.name as 'Категория', i.comment as Комментарий FROM income i, categories ca WHERE i.category_id = ca.id";

                MySqlCommand selCommand = new MySqlCommand(CommandText, connection);
                connection.Open(); //Устанавливаем соединение с базой данных.
                //MySql.Data.MySqlClient.MySqlDataReader reader = selCommand.ExecuteReader();
                System.Data.DataTable tab = new System.Data.DataTable();
                tab.Load(selCommand.ExecuteReader());
                BindingSource bs = new BindingSource();
                bs.DataSource = tab.DefaultView;

                connection.Close();
                return bs;
            }

            public static BindingSource refreshCosts()
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                /* REQUEST */
                string CommandText = "SELECT c.id as Номер, c.date as Дата, c.summ as Сумма, ca.name as 'Категория', c.comment as Комментарий FROM costs c, categories ca WHERE c.category_id = ca.id";

                MySqlCommand selCommand = new MySqlCommand(CommandText, connection);
                connection.Open(); //Устанавливаем соединение с базой данных.
                //MySql.Data.MySqlClient.MySqlDataReader reader = selCommand.ExecuteReader();
                System.Data.DataTable tab = new System.Data.DataTable(); // инициализируем пустую таблицу под извлекаемые из БД данные
                tab.Load(selCommand.ExecuteReader()); // выполняем запрос и загружаем результат в таблицу
                BindingSource bs = new BindingSource(); // инициализируем источник данных
                bs.DataSource = tab.DefaultView; // заполняем данными выборки

                connection.Close(); // закрываем соединение
                return bs; // возвращаем данные из метода
            }


        public static BindingSource refreshCategories(string name)
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                /* REQUEST */
                string CommandText = "SELECT id as Номер, name as '" + name + "', descr as Описание FROM categories";

                MySqlCommand selCommand = new MySqlCommand(CommandText, connection);
                connection.Open(); //Устанавливаем соединение с базой данных.
                //MySql.Data.MySqlClient.MySqlDataReader reader = selCommand.ExecuteReader();
                System.Data.DataTable tab = new System.Data.DataTable();
                tab.Load(selCommand.ExecuteReader());
                BindingSource bs = new BindingSource();
                bs.DataSource = tab.DefaultView;
                connection.Close();
                return bs;
            }
        
    }
}