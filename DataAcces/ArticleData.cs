using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAcces
{
    public class ArticleData : Connect, IArticleData
    {        
        private void CallProcedure(Article article, string sqProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = article.Id,
                    SqlDbType = SqlDbType.Int
                };
                command.Parameters.Add(idParam);

                SqlParameter userIdParam = new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = article.UserId,
                    SqlDbType = SqlDbType.Int
                };
                command.Parameters.Add(userIdParam);

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = article.Name,
                    SqlDbType = SqlDbType.NVarChar
                };
                command.Parameters.Add(nameParam);

                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = article.Date,
                    SqlDbType = SqlDbType.Date
                };
                command.Parameters.Add(dateParam);

                SqlParameter langParam = new SqlParameter
                {
                    ParameterName = "@language",
                    Value = article.Language,
                    SqlDbType = SqlDbType.NVarChar
                };
                command.Parameters.Add(langParam);

                SqlParameter pictureParam = new SqlParameter
                {
                    ParameterName = "@pictureRef",
                    Value = article.Picture,
                    SqlDbType = SqlDbType.NVarChar
                };
                command.Parameters.Add(pictureParam);

                SqlParameter videoParam = new SqlParameter
                {
                    ParameterName = "@videoRef",
                    Value = article.Video,
                    SqlDbType = SqlDbType.NVarChar
                };
                command.Parameters.Add(videoParam);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<Article> GetByCategoryId(int categoryId)
        {
            List<Article> articles = new List<Article>();

            string sqlExpressionArticles = "SELECT ArticleId, UserId, ArticleName, Date, Language, PictureRef, VideoRef FROM ArticlesOfCategory WHERE CategoryId = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpressionArticles, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = categoryId;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Article article = new Article
                            {
                                Id = (int)reader["ArticleId"],
                                UserId = (int)reader["UserId"],
                                Name = (string)reader["ArticleName"],
                                Date = (DateTime)reader["Date"],
                                Language = (string)reader["Language"],
                                Picture = (string)reader["PictureRef"],
                                Video = (string)reader["VideoRef"]
                            };

                            articles.Add(article);
                        }
                    }
                }
            }

            return articles;
        }

        public Article GetById(int id)
        {
            string sqlExpression = "SELECT Id, Name, Date, Language, PictureRef, VideoRef FROM Articles Where Id = @id";

            Article article = new Article();

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
                        article.Id = (int)reader["Id"];
                        article.Name = (string)reader["Name"];
                        article.Date = (DateTime)reader["Date"];
                        article.Language = (string)reader["Language"];
                        article.Picture = (string)reader["PictureRef"];
                        article.Video = (string)reader["VideoRef"];                        
                    }
                }
            }

            return article;
        }

        public IEnumerable<Article> List()
        {
            List<Article> articles = new List<Article>();

            string sqlExpression = "SELECT Id, Name, Date, Language, PictureRef, VideoRef FROM Articles";

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
                            Article article = new Article
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Date = (DateTime)reader["Date"],
                                Language = (string)reader["Language"],
                                Picture = (string)reader["PictureRef"],
                                Video = (string)reader["VideoRef"]
                            };
                            
                            articles.Add(article);
                        }
                    }
                }
            }

            return articles;
        }

        public void Add(Article entity)
        {
            string sqlProcedure = "AddArticle";

            CallProcedure(entity, sqlProcedure);
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE Articles WHERE Id = @id";

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

        public void Edit(Article entity)
        {
            string sqlExpression = "UpdateArticle";
                        
            CallProcedure(entity, sqlExpression);
        }

        public int GetLastIndex()
        {
            string sqlExpression = "SELECT Id FROM Articles ORDER BY Id DESC";

            int id = 1;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        id = (int)reader["Id"];
                    }
                }
            }

            return id;
        }
    }
}
