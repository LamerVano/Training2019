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
    public class ArticleData : DataAcces, IArticleData
    {
        public bool AddArticle(Article article)
        {
            string sqlExpression = "AddArticle";

            return CallProcedure(article, sqlExpression);
        }

        public bool UpdateArticle(Article article)
        {
            string sqlExpression = "UpdateArticle";

            return CallProcedure(article, sqlExpression);
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

        public Article GetArticle(int articleId)
        {
            string sqlExpression = String.Format("SELECT Id, Name, Date, Language, PictureRef, VideoRef FROM Articles Where Id = '{0}'", articleId);

            Article article = new Article();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

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

        public IEnumerable<Article> GetArticles()
        {
            List<Article> articles = new List<Article>();

            string sqlExpression = String.Format("SELECT Id, Name, Date, Language, PictureRef, VideoRef FROM Articles");

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
    }
}
