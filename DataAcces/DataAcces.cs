using Common;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace DataAcces
{
    public abstract class DataAcces
    {              
        protected string _connection = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        protected int _numOfConnection;        
                
        protected void TryOpenConnection(SqlConnection connection)
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

        protected List<ArticleReference> GetReferences(int articleId, SqlConnection connection)
        {
            List<ArticleReference> articles = new List<ArticleReference>();

            string sqlExpressionReference = "SELECT ReferenceId, Name FROM ReferenceToArticle WHERE ArticleId = @id";

            SqlCommand commandRef = new SqlCommand(sqlExpressionReference, connection);

            commandRef.Parameters.Add("@id", SqlDbType.Int);
            commandRef.Parameters["@id"].Value = articleId;

            using (SqlDataReader readerRef = commandRef.ExecuteReader())
            {
                if (readerRef.HasRows)
                {
                    while (readerRef.Read())
                    {
                        ArticleReference articleRef = new ArticleReference();

                        articleRef.Id = (int)readerRef["ReferenceId"];
                        articleRef.Name = (string)readerRef["Name"];

                        articles.Add(articleRef);
                    }
                }
            }

            return articles;
        }
    }
}
