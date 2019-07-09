using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAcces
{
    public class CategoryData : Connect, ICategoryData
    {    
        public Category GetById(int id)
        {
            string sqlExpression = "SELECT Id, Name FROM Category Where Id = @id";

            Category category = new Category();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        category.Id = (int)reader["Id"];
                        category.Name = (string)reader["Name"];
                    }
                }
            }

            return category;
        }

        public IEnumerable<Category> List()
        {
            List<Category> categories = new List<Category>();

            string sqlExpression = String.Format("SELECT Id, Name FROM Category");

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
                            Category category = new Category
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"]
                            };

                            categories.Add(category);
                        }
                    }
                }
            }

            return categories;
        }

        public void Add(Category entity)
        {
            string sqlProcedure = "AddCategory";

            CallProcedure(entity, sqlProcedure);
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE Category WHERE Id = @id";

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
                catch
                {
                    return ;
                }
            }
        }

        public void Edit(Category entity)
        {
            string sqlProcedure = "UpdateCategory";

            CallProcedure(entity, sqlProcedure);
        }


        private bool CallProcedure(Category category, string sqlProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlProcedure, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = category.Id
                };
                command.Parameters.Add(idParam);

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = category.Name
                };
                command.Parameters.Add(nameParam);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return true;
        }
    }
}
