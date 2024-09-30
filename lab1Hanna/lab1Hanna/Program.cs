using System;
using System.Data;
using System.Data.SqlClient;

namespace CoffeeShop
{
    class Program
    {
        // Оновлений рядок з'єднання, включаючи ім'я сервера та бази даних
        static string connectionString = "Server=localhost;Database=CoffeeShop;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            DisplayClients();
            AddClient("Марія", "Іваненко", "111222333");
            DisplayDrinks();
            DisplayOrders();
            DisplayOrdersWithJoin();
            DisplayAveragePrice();
        }

        static void DisplayClients()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Client", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, Phone: {reader["Phone"]}");
                }
            }
        }

        static void AddClient(string firstName, string lastName, string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Client (FirstName, LastName, Phone) VALUES (@FirstName, @LastName, @Phone)", connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Phone", phone);
                command.ExecuteNonQuery();
            }
        }

        static void DisplayDrinks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Drink", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["Name"]}, Price: {reader["Price"]}");
                }
            }
        }

        static void DisplayOrders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [Order]", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}, Date: {reader["Date"]}, ClientID: {reader["ClientID"]}, EmployeeID: {reader["EmployeeID"]}");
                }
            }
        }

        static void DisplayOrdersWithJoin()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT o.ID, o.Date, c.FirstName, c.LastName FROM [Order] o JOIN Client c ON o.ClientID = c.ID", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Order ID: {reader["ID"]}, Date: {reader["Date"]}, Client: {reader["FirstName"]} {reader["LastName"]}");
                }
            }
        }

        static void DisplayAveragePrice()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT AVG(Price) AS AveragePrice FROM Drink", connection);
                var averagePrice = command.ExecuteScalar();
                Console.WriteLine($"Average Drink Price: {averagePrice}");
            }
        }
    }
}
