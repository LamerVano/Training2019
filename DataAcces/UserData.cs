using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAcces
{
    public class UserData : DataAcces, IUsersData
    {
        public bool AddUser(User user)
        {
            string sqlExpression = "AddUser";

            return CallProcedure(user, sqlExpression);
        }

        public bool UpdateUser(User user)
        {
            string sqlExpression = "UpdateUser";

            return CallProcedure(user, sqlExpression);
        }

        public bool DelUser(int userId)
        {
            string sqlExpression = "DELETE Users WHERE Id = @id";

            Category category = new Category();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = userId;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public User GetUser(int userId)
        {
            string sqlExpression = String.Format("SELECT Id, FName, LName, EMail, Phone, Role FROM Users WHERE Id = '{0}'", userId);

            User user = new User();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.UserId = (int)reader["Id"];
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

        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();

            string sqlExpression = String.Format("SELECT Id, FName, LName, EMail, Phone, Role FROM Users");

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User user = new User();

                            user.UserId = (int)reader["Id"];
                            user.FirstName = (string)reader["FName"];
                            user.LastName = (string)reader["LName"];
                            user.EMail = (string)reader["EMail"];
                            user.Phone = (string)reader["Phone"];
                            user.Role = (string)reader["Role"];

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }


        private bool CallProcedure(User user, string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = user.UserId
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
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
