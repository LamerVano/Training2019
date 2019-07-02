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
    public class CategoryRefData : Connect, ICategoryRefData
    {
        public void Add(CategoryReferences entity)
        {
            string sqlProcedure = "AddReference";

            CallProcedure(entity, sqlProcedure);
        }

        public void Delete(CategoryReferences entity)
        {
            string sqlExpression = "Delete CategoryRefs where CategoryRefs.CategoryId = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = entity.Id;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {
                    return;
                }
            }
        }

        public void Edit(CategoryReferences entity)
        {
            Delete(entity);
            Add(entity);
        }

        public CategoryReferences GetById(int id)
        {
            string sqlExpressionReference = "SELECT ArticleId, Name FROM ArticlesOfCategory WHERE CategoryId = @id";

            CategoryReferences categoryRefs = new CategoryReferences()
            {
                Id = id
            };

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand commandRef = new SqlCommand(sqlExpressionReference, connection);

                commandRef.Parameters.Add("@id", SqlDbType.Int);
                commandRef.Parameters["@id"].Value = id;

                using (SqlDataReader readerRef = commandRef.ExecuteReader())
                {
                    if (readerRef.HasRows)
                    {
                        while (readerRef.Read())
                        {
                            ArticleReference articleRef = new ArticleReference
                            {
                                Id = (int)readerRef["ArticleId"],
                                Name = (string)readerRef["Name"]
                            };

                            categoryRefs.Refs.Add(articleRef);
                        }
                    }
                }
            }

            return categoryRefs;
        }

        public IEnumerable<CategoryReferences> List()
        {
            List<CategoryReferences> categoriesRefsList = new List<CategoryReferences>();

            string sqlExpression = "SELECT CategoryId, ArticleId, Name FROM ArticlesOfCategory";

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
                            CategoryReferences categoryRefs = new CategoryReferences
                            {
                                Id = (int)reader["CategoryId"]
                            };

                            categoriesRefsList.Add(categoryRefs);
                        }
                    }
                }
            }

            return categoriesRefsList;
        }

        private bool CallProcedure(CategoryReferences categoriesRefs, string sqlProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (ArticleReference reference in categoriesRefs.Refs)
                {
                    TryOpenConnection(connection);

                    SqlCommand command = new SqlCommand(sqlProcedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@categoryId",
                        Value = categoriesRefs.Id
                    };
                    command.Parameters.Add(idParam);

                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@articleId",
                        Value = reference.Id
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
            }
            return true;
        }
    }
}
