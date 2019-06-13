using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAcces
{
    public class ArticleData : DataAcces, IArticleData
    {
        public bool AddArticle(Article article)
        {
            string sqlExpression = "AddArticle";

            return CallProcedure(article, sqlExpression) && ProcedureReference(article, "AddReference");
        }

        public bool UpdateArticle(Article article)
        {
            string sqlExpression = "UpdateArticle";

            return CallProcedure(article, sqlExpression) && DelReference(article.Id) && ProcedureReference(article, "AddReference");
        }

        public bool DelArticle(int articleId)
        {
            string sqlExpression = "DELETE Category WHERE Id = @id";

            Category category = new Category();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = articleId;

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

        private bool DelReference(int articleId)
        {
            string sqlExpression = "Delete ArticlesRef where ArticlesRef.IdArticle = @id";

            Category category = new Category();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = articleId;

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

        public Article GetArticle(int articleId)
        {
            string sqlExpression = "SELECT Id, Name, Date, Language, PictureRef, VideoRef FROM Articles Where Id = @id";

            Article article = new Article();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = articleId;

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

                        article.Reference = GetReferences(article.Id, connection);
                    }
                }
            }

            return article;
        }

        public IEnumerable<Article> GetArticles()
        {
            List<Article> articles = new List<Article>();

            string sqlExpression = "SELECT Id, Name, Date, Language, PictureRef, VideoRef FROM Articles";
                        
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
                            Article article = new Article();

                            article.Id = (int)reader["Id"];
                            article.Name = (string)reader["Name"];
                            article.Date = (DateTime)reader["Date"];
                            article.Language = (string)reader["Language"];
                            article.Picture = (string)reader["PictureRef"];
                            article.Video = (string)reader["VideoRef"];

                            article.Reference = GetReferences(article.Id, connection);                            

                            articles.Add(article);
                        }
                    }
                }
            }

            return articles;
        }


        private bool CallProcedure(Article article, string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;

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

        private bool ProcedureReference(Article article, string procedureName)
        {
            foreach (ArticleReference reference in article.Reference)
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    TryOpenConnection(connection);

                    SqlCommand command = new SqlCommand(procedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@articleId",
                        Value = article.Id
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
