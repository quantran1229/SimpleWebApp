using System;
using System.IO;
using System.Web.Http;
using Mono.Data.Sqlite;

namespace SimpleWeb
{
    public static class WebApiConfig
    {
        private const string DB_NAME = "SimpleDatabase.sqlite";
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.Insert(0, new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            Newtonsoft.Json.JsonConvert.DefaultSettings = () => new Newtonsoft.Json.JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };

            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            //check if database exist? if not then create one
            createNewDatabase();
        }

        //create SimpleWeb database
        private static void createNewDatabase()
        {
            try
            {
                if (!File.Exists(DB_NAME))
                {
                    Console.WriteLine("Create new database");
                    SqliteConnection.CreateFile(DB_NAME);
                    if (File.Exists(DB_NAME))
                    {
                        createTable();
                        Console.WriteLine("Finishing created new database");
                    }
                }
                else
                {
                    Console.WriteLine("database exist!!!");
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //create table for database
        private static void createTable()
        {
            try
            {
                using (SqliteConnection db_conn = new SqliteConnection("Data Source=" + DB_NAME + ";Version=3;"))
                {

                    db_conn.Open();
                    string sql = "create table Users (username varchar(50) NOT NULL primary key,password varchar(50) NOT NULL,firstname varchar(100),lastname varchar(100),email varchar(50) NOT NULL)";
                    SqliteCommand command = new SqliteCommand(sql, db_conn);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Created Users table");
                    db_conn.Close();
                }
            }
            catch (SqliteException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
