using CRUD_OPs.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace CRUD_OPs.Repo
{
    public class ClientRepository
    {
        private readonly string sqlKey = "Data Source=localhost;Initial Catalog=CRUD_Ops DB.dbo;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public List<Client> GetClients()
        {
            List<Client> Clients = [];

            try
            {
                using (SqlConnection connection = new(sqlKey))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM clients ORDER BY id ASC";

                    using (SqlCommand command = new(sqlQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Client client = new()
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2), 
                                Email = reader.GetString(3),
                                Phone = reader.GetString(4),
                                Address = reader.GetString(5),
                                CreatedAt = reader.GetDateTime(6).ToString("yyyy-MM-dd HH:mm:ss")
                            };

                            Clients.Add(client);
                            Console.WriteLine("Client found: " + client.FirstName);
                        }
                    }
                }

                MessageBox.Show("Total clients fetched: " + Clients.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex);
            }

            return Clients;
        }

        public Client? GetClient(int id)
        {
            try
            {
                using (SqlConnection connection = new(sqlKey))
                {
                    connection.Open();
                    string sqlQuery = "SELECT * FROM clients WHERE id=@id;";

                    using (SqlCommand command = new(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Client client = new();
                                client.Id = Convert.ToInt32(reader["id"]);
                                client.FirstName = reader["FirstName"] as string ?? "";
                                client.LastName = reader["LastName"] as string ?? "";
                                client.Email = reader["Email"] as string ?? "";
                                client.Phone = reader["Phone"] as string ?? "";
                                client.Address = reader["Address"] as string ?? "";
                                client.CreatedAt = reader["Created_At"].ToString() ?? "";

                                return client;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex.ToString());
            }

            return null;
        }

        public void CreateClient(Client client)
        {
            try
            {
                using (SqlConnection connection = new(sqlKey))
                {
                    connection.Open();
                    string sqlqQuery = "INSERT INTO clients (FirstName, LastName, Email, Phone, Address)" +
                        "VALUES (@FirstName, @LastName, @Email, @Phone, @Address)";
                    using (SqlCommand command = new(sqlqQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", client.FirstName);
                        command.Parameters.AddWithValue("@LastName", client.LastName);
                        command.Parameters.AddWithValue("@Email", client.Email);
                        command.Parameters.AddWithValue("@Phone", client.Phone);
                        command.Parameters.AddWithValue("@Address", client.Address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("CreateUser_Exception: " + ex.Message);
            }
        }

        public void UpdateClient(Client client)
        {
            try
            {
                using (SqlConnection connection = new(sqlKey))
                {
                    connection.Open();
                    string sqlQuery = "UPDATE clients SET FirstName = @FirstName, LastName = @LastName, " +
                        "Email = @Email, Address = @Address, Phone = @Phone WHERE id=@id";
                    using (SqlCommand command = new(sqlQuery , connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", client.FirstName);
                        command.Parameters.AddWithValue("@LastName", client.LastName);
                        command.Parameters.AddWithValue("@Email", client.Email);
                        command.Parameters.AddWithValue("@Phone", client.Phone);
                        command.Parameters.AddWithValue("@Address", client.Address);
                        command.Parameters.AddWithValue("@id", client.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Edit_Exception: " + ex.Message);
            }
        }

        public void DeleteClient(int id)
        {
            try
            {
                using (SqlConnection connection = new(sqlKey))
                {
                    connection.Open();
                    string sqlQuery = "DELETE FROM clients WHERE id=@id";
                    using (SqlCommand command = new(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CreateUser_Exception: " + ex.Message);
            }
        }
        
        public bool ConxStatus()
        {
            using (SqlConnection connection = new(sqlKey)) 
            {
                connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
            }
        }



    }
}
