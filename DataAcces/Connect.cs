using Common;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace DataAcces
{
    public abstract class Connect
    {              
        protected string _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
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
            catch(System.InvalidOperationException)
            {
                SqlConnection.ClearPool(connection);

                TryOpenConnection(connection);
            }
        }
                
    }
}
