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
    public class ArticleRefData : Connect, IArticleRefData
    {
        public void Add(ArticleReferences entity)
        {
            string sqlProcedure = "AddReference";

            CallProcedure(entity, sqlProcedure);
        }

        public void Delete(int id)
        {
            string sqlExpression = "Delete ArticlesRef where ArticlesRef.IdArticle = @id";
            
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

        public void Edit(ArticleReferences entity)
        {
            Delete(entity.Id);
            Add(entity);
        }

        public ArticleReferences GetById(int id)
        {
            string sqlExpressionReference = "SELECT ReferenceId, Name FROM ReferenceToArticle WHERE ArticleId = @id";

            ArticleReferences articlesRefs = new ArticleReferences()
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
                                Id = (int)readerRef["ReferenceId"],
                                Name = (string)readerRef["Name"]
                            };

                            articlesRefs.Refs.Add(articleRef);
                        }
                    }
                }                
            }

            return articlesRefs;            
        }

        public IEnumerable<ArticleReferences> List()
        {
            List<ArticleReferences> articlesRefsList = new List<ArticleReferences>();

            string sqlExpression = "SELECT ArticleId, ReferenceId, Name FROM ReferenceToArticle";

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
                            ArticleReferences articleRefs = new ArticleReferences
                            {
                                Id = (int)reader["Id"]                                
                            };

                            articlesRefsList.Add(articleRefs);
                        }
                    }
                }
            }

            return articlesRefsList;
        }

        public IEnumerable<ArticleReference> ListShortArticle()
        {
            List<ArticleReference> articles = new List<ArticleReference>();

            string sqlExpression = "SELECT Id, Name FROM Articles";

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
                            ArticleReference article = new ArticleReference
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"]
                            };

                            articles.Add(article);
                        }
                    }
                }
            }

            return articles;
        }

        private void CallProcedure(ArticleReferences articleRefs, string sqlProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                foreach (ArticleReference reference in articleRefs.Refs)
                {
                    TryOpenConnection(connection);

                    SqlCommand command = new SqlCommand(sqlProcedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@articleId",
                        Value = articleRefs.Id
                    };
                    command.Parameters.Add(idParam);

                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@referenceId",
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
