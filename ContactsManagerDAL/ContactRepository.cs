using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsManagerDAL
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString;

        public ContactRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Contact GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contacts WHERE ContactID = @id";

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
                                return new Contact
                                {
                                    Id = (int)reader["ContactID"],
                                    CountryID = (int)reader["CountryID"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Email = (string)reader["Email"],
                                    Phone = (string)reader["Phone"],
                                    Address = (string)reader["Address"],
                                    BirthDate = (DateTime)reader["DateOfBirth"],
                                    ImagePath = reader["ImagePath"] == DBNull.Value ? string.Empty : (string)reader["ImagePath"]
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

        public List<Contact> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contacts";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Contact> contacts = new List<Contact>();

                            while (reader.Read())
                            {
                                Contact contact = new Contact
                                {
                                    Id = (int)reader["ContactID"],
                                    CountryID = (int)reader["CountryID"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Email = (string)reader["Email"],
                                    Phone = (string)reader["Phone"],
                                    Address = (string)reader["Address"],
                                    BirthDate = (DateTime)reader["DateOfBirth"],
                                    ImagePath = reader["ImagePath"] == DBNull.Value ? string.Empty : (string)reader["ImagePath"]
                                };

                                contacts.Add(contact);
                            }

                            return contacts;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return new List<Contact>();
                    }
                }
            }
        }

        public int Add(Contact newContact)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO 
                                 Contacts (
                                            FirstName, 
                                            LastName,
                                            Email,
                                            Phone,
                                            Address,
                                            DateOfBirth,
                                            ImagePath,
                                            CountryID
                                          )

                                 VALUES   (
                                            @FirstName, 
                                            @LastName,
                                            @Email,
                                            @Phone,
                                            @Address,
                                            @DateOfBirth,
                                            @ImagePath,
                                            @CountryID
                                          );
                                 SELECT SCOPE_IDENTITY();
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", newContact.FirstName);
                    command.Parameters.AddWithValue("@LastName", newContact.LastName);
                    command.Parameters.AddWithValue("@Email", newContact.Email);
                    command.Parameters.AddWithValue("@Phone", newContact.Phone);
                    command.Parameters.AddWithValue("@Address", newContact.Address);
                    command.Parameters.AddWithValue("@DateOfBirth", newContact.BirthDate);
                    command.Parameters.AddWithValue("@ImagePath", newContact.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CountryID", newContact.CountryID);

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

        public bool Update(Contact updatedContact)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Contacts
                                 SET
                                    FirstName = @FirstName,
                                    LastName = @LastName,
                                    Email = @Email,
                                    Phone = @Phone,
                                    Address = @Address,
                                    DateOfBirth = @DateOfBirth,
                                    ImagePath = @ImagePath,
                                    CountryID = @CountryID

                                 WHERE ContactID = @ContactID;
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", updatedContact.FirstName);
                    command.Parameters.AddWithValue("@LastName", updatedContact.LastName);
                    command.Parameters.AddWithValue("@Email", updatedContact.Email);
                    command.Parameters.AddWithValue("@Phone", updatedContact.Phone);
                    command.Parameters.AddWithValue("@Address", updatedContact.Address);
                    command.Parameters.AddWithValue("@DateOfBirth", updatedContact.BirthDate);
                    object updatedImagePathParam = string.IsNullOrEmpty(updatedContact.ImagePath) ? (object)DBNull.Value : updatedContact.ImagePath;
                    command.Parameters.AddWithValue("@ImagePath", updatedImagePathParam);
                    command.Parameters.AddWithValue("@CountryID", updatedContact.CountryID);
                    command.Parameters.AddWithValue("@ContactID", updatedContact.Id);

                    try
                    {
                        connection.Open();

                        int rowAffected = command.ExecuteNonQuery();

                        return rowAffected > 0;
                    }
                    catch (Exception ex)
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
                string query = @"DELETE FROM Contacts WHERE ContactID = @ContactID;
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContactID", id);

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
    }
}