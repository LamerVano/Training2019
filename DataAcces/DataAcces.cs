using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Unity;

namespace DataAcces
{
    public class DataAcces : IDataAcces
    {              
        string _connection = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        int _numOfConnection;
        

        public IArticle Article { get; set; }
        public ICategory Category { get; set; }
        public IUser User { get; set; }


        public DataAcces(IArticle article, ICategory category, IUser user)
        {
            Article = article;
            Category = category;
            User = user;
        }



        public IEnumerable<IArticle> GetArticles(int categoryId)
        {
            throw new NotImplementedException();
        }

        
        public IEnumerable<IArticle> GetArticles()
        {
            List<IArticle> articles = new List<IArticle>();

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

                            Article.Id = (int)reader["Id"];
                            Article.Name = (string)reader["Name"];
                            Article.Date = (DateTime)reader["Date"];
                            Article.Language = (string)reader["Language"];
                            Article.Picture = (string)reader["PictureRef"];
                            Article.Video = (string)reader["VideoRef"];

                            articles.Add(Article);
                        }
                    }
                }
            }

            return articles;
        }

        public IEnumerable<ICategory> GetCategories()
        {
            List<ICategory> categories = new List<ICategory>();

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
                            Category.Id = (int)reader["Id"];
                            Category.Name = (string)reader["Name"];

                            categories.Add(Category);
                        }
                    }
                }
            }

            return categories;
        }

        public IEnumerable<IUser> GetUsers()
        {
            List<IUser> users = new List<IUser>();

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
                            User.UserId = (int)reader["Id"];
                            User.FirstName = (string)reader["FName"];
                            User.LastName = (string)reader["LName"];
                            User.EMail = (string)reader["EMail"];
                            User.Phone = (string)reader["Phone"];
                            User.Role = (string)reader["Role"];

                            users.Add(User);
                        }
                    }
                }
            }

            return users;
        }



        public IArticle GetArticle(int articleId)
        {
            string sqlExpression = String.Format("SELECT Id, Name, Date, Language, PictureRef, VideoRef FROM Articles Where Id = '{0}'", articleId);

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Article.Id = (int)reader["Id"];
                        Article.Name = (string)reader["Name"];
                        Article.Date = (DateTime)reader["Date"];
                        Article.Language = (string)reader["Language"];
                        Article.Picture = (string)reader["PictureRef"];
                        Article.Video = (string)reader["VideoRef"];
                    }
                }
            }

            return Article;
        }

        public ICategory GetCategory(int categoryId)
        {
            string sqlExpression = String.Format("SELECT Id, Name FROM Category Where Id = '{0}'", categoryId);

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Category.Id = (int)reader["Id"];
                        Category.Name = (string)reader["Name"];
                    }
                }
            }

            return Category;
        }

        public IUser GetUser(int userId)
        {
            string sqlExpression = String.Format("SELECT Id, FName, LName, EMail, Phone, Role FROM Users WHERE Id = '{0}'", userId);

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        User.UserId = (int)reader["Id"];
                        User.FirstName = (string)reader["FName"];
                        User.LastName = (string)reader["LName"];
                        User.EMail = (string)reader["EMail"];
                        User.Phone = (string)reader["Phone"];
                        User.Role = (string)reader["Role"];
                    }
                }
            }

            return User;
        }


        public bool AddArticle(IArticle article)
        {
            string sqlExpression = String.Format("INSERT INTO Article (Name, Date, Language, PictureRef, VideoRef) VALUES ('{0}', '{1}','{2}', '{3}','{4}')", article.Name, article.Date, article.Language, article.Picture, article.Video);

            return OneCommand(sqlExpression);
        }

        public bool AddCategory(ICategory category)
        {
            string sqlExpression = String.Format("INSERT INTO Category (Name) VALUES ('{0}')", category.Name);

            return OneCommand(sqlExpression);
        }

        public bool AddUser(IUser user)
        {
            string sqlExpression;

            if (user.Role != "" & user.Role != null)
            {
                sqlExpression = String.Format("INSERT INTO Users (FName, LName, EMail, Password, Phone, Role) VALUES ('{0}', '{1}','{2}', '{3}','{4}','{5}')", user.FirstName, user.LastName, user.EMail, user.Password, user.Phone, user.Role);
            }
            else
            {
                sqlExpression = String.Format("INSERT INTO Users (FName, LName, EMail, Password, Phone) VALUES ('{0}', '{1}','{2}', '{3}','{4}')", user.FirstName, user.LastName, user.EMail, user.Password, user.Phone);
            }

            return OneCommand(sqlExpression);
        }



        public bool ChangeCategory(ICategory category)
        {
            string sqlExpression = String.Format("UPDATE Category SET Name = '{1}' FROM Category WHERE Id={0}", category.Id, category.Name);

            return OneCommand(sqlExpression);
        }

        public bool ChangeArticle(IArticle article)
        {
            string sqlExpression = String.Format("UPDATE Article SET Name = '{1}', Date = '{2}', Language = '{3}', PictureRef = '{4}', VideoRef = '{5}' FROM Article WHERE Id={0}", article.Id, article.Name, article.Date, article.Language, article.Picture, article.Video);

            return OneCommand(sqlExpression);
        }

        public bool ChangeUser(IUser user)
        {
            string sqlExpression = String.Format("UPDATE Users SET FName = '{1}', LName = '{2}', EMail = '{3}', Password = '{4}', Phone = '{5}', Role = '{6}' FROM Users WHERE Id={0}", user.UserId, user.FirstName, user.LastName, user.EMail, user.Password, user.Phone, user.Role);

            return OneCommand(sqlExpression);
        }



        public bool DelArticle(int articleId)
        {
            string sqlExpression = String.Format("DELETE Article WHERE Id={0}", articleId);

            return OneCommand(sqlExpression);
        }

        public bool DelCategory(int categoryId)
        {
            string sqlExpression = String.Format("DELETE Category WHERE Id={0}", categoryId);

            return OneCommand(sqlExpression);
        }

        public bool DelUser(int userId)
        {
            string sqlExpression = String.Format("DELETE Users WHERE Id={0}", userId);

            return OneCommand(sqlExpression);
        }


        private void TryOpenConnection(SqlConnection connection)
        {
            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {
                _numOfConnection++;
                connection.Close();
                Thread.Sleep(1000);
                if (_numOfConnection > 10)
                {
                    _numOfConnection = 0;
                    throw ex;

                }
                TryOpenConnection(connection);
            }
        }

        private bool OneCommand(string sqlExpression)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    TryOpenConnection(connection);

                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        
    }
}
