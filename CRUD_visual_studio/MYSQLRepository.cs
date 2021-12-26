using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace CRUD_visual_studio
{
    public class MYSQLRepository
    {
        private string ConnectionString { get; set; }

        public MYSQLRepository()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            ConnectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public void Create(string name, int age)
        {
            using MySqlConnection connection = new(ConnectionString);
            try
            {
                connection.Open();
                MySqlTransaction mySqlTransaction = connection.BeginTransaction();

                string SQL = "INSERT INTO person VALUES (null, @name, @age)";
                MySqlCommand mySqlCommand = new(SQL, connection);

                mySqlCommand.Parameters.AddWithValue("@name", name);
                mySqlCommand.Parameters.AddWithValue("@age", age);
                mySqlCommand.ExecuteNonQuery();
                mySqlTransaction.Commit();
                mySqlTransaction.Dispose();
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public List<PersonModel> Read()
        {
            List<PersonModel> personModels = new();

            using (MySqlConnection connection = new(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string SQL = "SELECT * FROM person";
                    MySqlCommand mySqlCommand = new(SQL, connection);

                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            personModels.Add(new PersonModel()
                            {
                                Name = reader["name"].ToString(),
                                Age = Convert.ToInt32(reader["age"]),
                                PersonId = Convert.ToInt32(reader["personId"])
                            });
                        }

                    }

                    mySqlCommand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return personModels;
        }

        public void Update(string name, int age, int personId)
        {
            using MySqlConnection connection = new(ConnectionString);
            try
            {
                connection.Open();
                MySqlTransaction mySqlTransaction = connection.BeginTransaction();

                string SQL = "UPDATE person SET name=@name, age=@age WHERE personId=@personId";
                MySqlCommand mySqlCommand = new(SQL, connection);

                mySqlCommand.Parameters.AddWithValue("@personId", personId);
                mySqlCommand.Parameters.AddWithValue("@name", name);
                mySqlCommand.Parameters.AddWithValue("@age", age);
                mySqlCommand.ExecuteNonQuery();
                mySqlTransaction.Commit();
                mySqlTransaction.Dispose();
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void Delete(int personId)
        {
            using MySqlConnection connection = new(ConnectionString);
            try
            {
                connection.Open();
                MySqlTransaction mySqlTransaction = connection.BeginTransaction();

                string SQL = "DELETE FROM person WHERE personId = @personId";
                MySqlCommand mySqlCommand = new(SQL, connection);

                mySqlCommand.Parameters.AddWithValue("@personId", personId);
                mySqlCommand.ExecuteNonQuery();
                mySqlTransaction.Commit();
                mySqlTransaction.Dispose();
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
