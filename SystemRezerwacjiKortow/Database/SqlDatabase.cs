﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public static class SqlDatabase
    {
        public static int UserRoleId;   // numer roli użytkownik
        public static int CustomerAtr;  // atrapa customera

        public static SqlConnection connection = Initialize();

        #region Konfiguracja bazy
        public static SqlConnection Initialize()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.Pooling = false;
            stringBuilder.DataSource = ConfigurationManager.AppSettings["BazaDataSource"];
            stringBuilder.UserID = ConfigurationManager.AppSettings["BazaUserID"];
            stringBuilder.InitialCatalog = ConfigurationManager.AppSettings["BazaInitialCatalog"];
            stringBuilder.Password = ConfigurationManager.AppSettings["BazaPassword"];
            Console.WriteLine(stringBuilder.ToString());
            return new SqlConnection(stringBuilder.ToString());
        }

        public static bool OpenConnection()
        {
            if (connection == null)
                Initialize();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Number);
                Console.WriteLine(ex.Message);
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server. Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        public static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Number);
                Console.WriteLine(ex.Message);
                return false;
            }
        }       
        #endregion

        #region Inicjalizacja zmiennych początkowych
        public static void init()
        {
            SqlDatabase.UserRoleId = SqlDatabase.GetUserRoleId();
            SqlDatabase.CustomerAtr = SqlDatabase.GetCustomerAtr();
        }
        public static int GetUserRoleId()
        {
            int result = -1;
            if (OpenConnection())
            {
                var command = new SqlCommand("select RoleID from dbo.Role where RoleName='user'", connection);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = (int)reader["RoleID"];
                }
                else
                {
                    throw new System.ArgumentException("W tabeli dbo.Role brakuje roli o nazwie user");
                }

                CloseConnection();                
            }
            return result;
        }

        public static int GetCustomerAtr()
        {
            int result = -1;
            if (OpenConnection())
            {
                var command = new SqlCommand("select CustomerID from dbo.Customer where CompanyName='Atrapa' and City='Atrapa'", connection);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = (int)reader["CustomerID"];
                }
                else
                {
                    throw new System.ArgumentException("W tabeli dbo.Customer brakuje klienta o nazwie firmy Atrapa i mieście Atrapa");
                }

                CloseConnection();
            }
            return result;
        }
        #endregion

        // zwraca listę danych klientów
        public static List<Customer> GetCustomers()
        {
            var list = new List<Customer>();
            if (OpenConnection())
            {
                var command = new SqlCommand("select * from dbo.Customer", connection);
                //command.Parameters.AddWithValue("@number", number);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Customer()
                    {
                        CustomerID = (int)reader["CustomerID"],
                        CompanyName = (string)reader["CompanyName"],
                        City = (string)reader["City"],
                        Street = (string)reader["Street"],
                        ZipCode = (string)reader["ZipCode"],
                        DiscountValue = (decimal)reader["DiscountValue"],
                    });
                }
                CloseConnection();
            }
            return list;
        }
    }
}
