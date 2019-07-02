using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAcces
{
    public class ArticleData : Connect, IArticleData
    {        
        private bool CallProcedure(Article article, string sqProcedure)
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
                    Value = article.Id
                };
                command.Parameters.Add(idParam);

                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = article.Name
                };
                command.Parameters.Add(nameParam);

                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = article.Date
                };
                command.Parameters.Add(dateParam);

                SqlParameter langParam = new SqlParameter
                {
                    ParameterName = "@language",
                    Value = article.Language
                };
                command.Parameters.Add(langParam);

                SqlParameter pictureParam = new SqlParameter
                {
                    ParameterName = "@pictureRef",
                    Value = article.Picture
                };
                command.Parameters.Add(pictureParam);

                SqlParameter videoParam = new SqlParameter
                {
                    ParameterName = "@videoRef",
                    Value = article.Video
                };
                command.Parameters.Add(videoParam);

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

        public IEnumerable<Article> GetByCategoryId(int categoryId)
        {
            List<Article> articles = new List<Article>();

            string sqlExpressionArticles = "SELECT ArticleId, Name, Date, Language, PictureRef, VideoRef FROM ArticlesOfCategory WHERE CategoryId = @id";

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
                            Article article = new Article();

                            article.Id = (int)reader["ArticleId"];
                            article.Name = (string)reader["Name"];
                            article.Date = (DateTime)reader["Date"];
                            article.Language = (string)reader["Language"];
                            article.Picture = (string)reader["PictureRef"];
                            article.Video = (string)reader["VideoRef"];

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

        public void Delete(Article entity)
        {
            string sqlExpression = "DELETE Category WHERE Id = @id";

            Category category = new Category();

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
                    return ;
                }
            }
        }

        public void Edit(Article entity)
        {
            string sqlExpression = "UpdateArticle";
                        
            CallProcedure(entity, sqlExpression);
        }
    }
}
