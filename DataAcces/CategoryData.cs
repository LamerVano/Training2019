using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAcces
{
    public class CategoryData : DataAcces, ICategoryData
    {
        public bool AddCategory(Category category)
        {
            string sqlExpression = "AddCategory";

            return CallProcedure(category, sqlExpression);
        }

        public bool UpdateCategory(Category category)
        {
            string sqlExpression = "UpdateCategory";

            return CallProcedure(category, sqlExpression);
        }

        public bool DelCategory(int categoryId)
        {
            string sqlExpression = "DELETE Category WHERE Id = @id";

            Category category = new Category();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = categoryId;

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

        public IEnumerable<Article> GetArticles(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            string sqlExpression = String.Format("SELECT Id, Name FROM Category");

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
                            Category category = new Category();

                            category.Id = (int)reader["Id"];
                            category.Name = (string)reader["Name"];

                            categories.Add(category);
                        }
                    }
                }
            }

            return categories;
        }

        public Category GetCategory(int categoryId)
        {
            string sqlExpression = "SELECT Id, Name FROM Category Where Id = @id";

            Category category = new Category();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = categoryId;

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


        private bool CallProcedure(Category category, string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);
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
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
