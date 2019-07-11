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
    public class CategoryRefsData : Connect, ICategoryRefsData
    {
        public void Add(CategoryReferences entity)
        {
            if (entity.Refs != null)
            {
                string sqlProcedure = "AddCategoryRef";

                CallProcedure(entity, sqlProcedure);
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "Delete CategoryRefs where CategoryRefs.IdArticle = @id";

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
                    return;
                }
            }
        }

        public void Edit(CategoryReferences entity)
        {
            Delete(entity.Id);
            Add(entity);
        }

        public CategoryReferences GetById(int id)
        {
            string sqlExpressionReference = "SELECT CategoryId, CategoryName FROM ArticlesOfCategory WHERE ArticleId = @id";

            CategoryReferences categoryRefs = new CategoryReferences()
            {
                Id = id,
                Refs = new List<Category>()
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
                            Category reference = new Category
                            {
                                Id = (int)readerRef["CategoryId"],
                                Name = (string)readerRef["CategoryName"]
                            };

                            categoryRefs.Refs.Add(reference);
                        }
                    }
                }
            }

            return categoryRefs;
        }

        public IEnumerable<CategoryReferences> List()
        {
            List<CategoryReferences> categoriesRefsList = new List<CategoryReferences>();

            string sqlExpression = "SELECT ArticleId, CategoryId, CategoryName FROM ArticlesOfCategory";

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
                                Id = (int)reader["ArticleId"]
                            };

                            CategoryReferences article = categoriesRefsList.FirstOrDefault(art => art.Id == categoryRefs.Id);

                            if (article != null)
                            {
                                article.Refs.Add(new Category
                                {
                                    Id = (int)reader["CategoryId"],
                                    Name = (string)reader["CategoryName"]
                                });
                            }
                            else
                            {
                                categoriesRefsList.Add(categoryRefs);
                            }
                        }
                    }
                }
            }

            return categoriesRefsList;
        }

        private void CallProcedure(CategoryReferences categoryRefs, string sqlProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (Category reference in categoryRefs.Refs)
                {
                    TryOpenConnection(connection);

                    SqlCommand command = new SqlCommand(sqlProcedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@articleId",
                        Value = categoryRefs.Id
                    };
                    command.Parameters.Add(idParam);

                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@categoryId",
                        Value = reference.Id
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
            }
        }
    }
}
