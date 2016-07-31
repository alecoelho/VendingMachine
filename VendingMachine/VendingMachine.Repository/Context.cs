using System;
using System.Data.SqlClient;
using System.Configuration;

namespace VendingMachine.Repository
{
    public class Context : IDisposable
    {
        private readonly SqlConnection myConn;

        public Context() {
            myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["VendingMachine"].ConnectionString);
            myConn.Open();    
        }

        public int ExecuteCommand(string strQuery)
        {
            var cmdCommand = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = System.Data.CommandType.Text,
                Connection = myConn
            };

            return cmdCommand.ExecuteNonQuery();
        }

        public SqlDataReader ExecuteCommandWithReturn(string strQuery)
        {
            var cmdCommand = new SqlCommand(strQuery, myConn);
            return cmdCommand.ExecuteReader();
        }

        public void Dispose()
        {
            if (myConn.State == System.Data.ConnectionState.Open)
                myConn.Close();
        }
    }
}
