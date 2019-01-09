using System;
using MySql.Data;
using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using dapper_sandbox.Examples;

namespace dapper_sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appconfig.json")
                .Build();
            
            string connectionString = $"Persist Security Info=False;database={config["database"]};" +
                                      $"server={config["server"]};" +
                                      $"user id={config["user"]};" +
                                      $"Password={config["password"]}";


            MySqlConnection connection = null;
            try {
                connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                connection.Open();

                foreach (var runExample in ExampleList.GetList()) {
                    runExample(connection);
                }

            } catch (Exception ex) {
                connection?.Dispose();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
