using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAcces
{
    public class UserData : Connect, IUserData
    {       
        public User GetById(int id)
        {
            string sqlExpression = String.Format("SELECT Id, FName, LName, EMail, Phone, Role FROM Users WHERE Id = '{0}'", id);

            User user = new User();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.Id = (int)reader["Id"];
                        user.FirstName = (string)reader["FName"];
                        user.LastName = (string)reader["LName"];
                        user.EMail = (string)reader["EMail"];
                        user.Phone = (string)reader["Phone"];
                        user.Role = (string)reader["Role"];
                    }
                }
            }

            return user;
        }

        public IEnumerable<User> List()
        {
            List<User> users = new List<User>();

            string sqlExpression = String.Format("SELECT Id, FName, LName, EMail, Phone, Role FROM Users");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = (int)reader["Id"],
                                FirstName = (string)reader["FName"],
                                LastName = (string)reader["LName"],
                                EMail = (string)reader["EMail"],
                                Phone = (string)reader["Phone"],
                                Role = (string)reader["Role"]
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public void Add(User entity)
        {
            string sqlProcedure = "AddUser";

            CallProcedure(entity, sqlProcedure);
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE Users WHERE Id = @id";

            Category category = new Category();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }            
        }

        public void Edit(User entity)
        {
            string sqlProcedure = "UpdateUser";

            CallProcedure(entity, sqlProcedure);
        }


        private void CallProcedure(User user, string sqlProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = user.Id
                };
                command.Parameters.Add(idParam);

                SqlParameter fnameParam = new SqlParameter
                {
                    ParameterName = "@fname",
                    Value = user.FirstName
                };
                command.Parameters.Add(fnameParam);

                SqlParameter lnameParam = new SqlParameter
                {
                    ParameterName = "@lname",
                    Value = user.LastName
                };
                command.Parameters.Add(lnameParam);

                SqlParameter emailParam = new SqlParameter
                {
                    ParameterName = "@email",
                    Value = user.EMail
                };
                command.Parameters.Add(emailParam);

                SqlParameter passwParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = user.Password
                };
                command.Parameters.Add(passwParam);

                SqlParameter phoneParam = new SqlParameter
                {
                    ParameterName = "@phone",
                    Value = user.Phone
                };
                command.Parameters.Add(phoneParam);

                SqlParameter roleParam = new SqlParameter
                {
                    ParameterName = "@role",
                    Value = user.Role
                };
                command.Parameters.Add(roleParam);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

    }
}
