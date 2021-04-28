using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSqLite
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Data Source=SQLite.sqlite;";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                const string sql = @"SELECT * FROM Orders";
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqliteDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                string columnNameCustomerID = dataReader.GetName(0);
                                string columnNameOrderID = dataReader.GetName(1);
                                string columnNameOrderDate = dataReader.GetName(2);
                                string columnNameFilledDate = dataReader.GetName(3);
                                string columnNameStatus = dataReader.GetName(4);
                                string columnNameAmount = dataReader.GetName(5);

                                Console.WriteLine($"{columnNameCustomerID}\t{columnNameOrderID} \t{columnNameOrderDate}\t{columnNameFilledDate}\t{columnNameStatus}\t{columnNameAmount}");

                                while (dataReader.Read())
                                {
                                    var CustomerID = dataReader.GetValue(0);
                                    var OrderID = dataReader.GetValue(1);
                                    var OrderDate = dataReader.GetValue(2);
                                    var FilledDate = dataReader.GetValue(3);
                                    var Status = dataReader.GetValue(4);
                                    var Amount = dataReader.GetValue(5);

                                    Console.WriteLine($"{CustomerID}\t\t{OrderID}\t\t{OrderDate}\t{FilledDate}\t{Status}\t{Amount}");
                                }
                            }
                        }
                    }
                    catch (SqliteException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        // Закрываем соединение.
                        connection.Close();
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
