using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsManagerDAL
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string _connectionString;

        public CountryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Country GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Countries WHERE CountryID = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Country
                                {
                                    Id = (int)reader["CountryID"],
                                    CountryName = (string)reader["CountryName"]
                                };

                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public DataTable GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Countries";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable countries = new DataTable();

                            if (reader.HasRows)
                            {
                                countries.Load(reader);
                            }
                            else
                            {
                                return null;
                            }

                            return countries;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public int Add(Country newCountry)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO 
                                 Countries (
                                            CountryName
                                          )

                                 VALUES   (
                                            @CountryName
                                          );
                                 SELECT SCOPE_IDENTITY();
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryName", newCountry.CountryName);

                    try
                    {
                        connection.Open();

                        var newId = command.ExecuteScalar();

                        return Convert.ToInt32(newId);
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
        }

        public bool Update(Country updatedCountry)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Countries
                                 SET
                                    CountryName = @CountryName
                                 WHERE CountryID = @CountryID;
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryName", updatedCountry.CountryName);
                    command.Parameters.AddWithValue("@ContactID", updatedCountry.Id);

                    try
                    {
                        connection.Open();

                        int rowAffected = command.ExecuteNonQuery();

                        return rowAffected > 0;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM Countries WHERE CountryID = @CountryID;
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", id);

                    try
                    {
                        connection.Open();

                        int rowAffected = command.ExecuteNonQuery();

                        return rowAffected > 0;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool IsThere(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT Found=1 FROM Countries WHERE CountryID = @CountryID;
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", id);

                    try
                    {
                        connection.Open();

                        var found = command.ExecuteScalar();

                        return Convert.ToInt16(found) == 1;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}
