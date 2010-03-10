using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AddressMatch.DataBase
{
    public class Database
    {
        #region [Members]

        /// <summary>
        /// connection string
        /// </summary>
        private string connectionString;

        /// <summary>
        /// database connection
        /// </summary>
        public SqlConnection Connection { get; set; }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="astrConnectionString">Connection String</param>
        public Database(string astrConnectionString)
        {
            connectionString = astrConnectionString;
        }

        #region [Public Methods]

        /// <summary>
        /// Set the connection string.
        /// </summary>
        public void SetConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Get the connection object.
        /// </summary>
        /// <returns>SQL Connection</returns>
        public SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Connection String is not set.");

            Connection = new SqlConnection(connectionString);

            return Connection;
        }

        /// <summary>
        /// Execute query and return the result as a DataSet.
        /// </summary>
        /// <param name="astrQuery">Select Query</param>
        /// <returns></returns>
        public DataSet ExecuteSelect(string astrQuery)
        {
            DataSet ldtbResult = null;

            if (Connection == null)
                GetConnection();

            Connection.Open();

            SqlCommand lcmdSQL = new SqlCommand(astrQuery, Connection);
            lcmdSQL.ExecuteNonQuery();
        
            ldtbResult = new DataSet();
            SqlDataAdapter ldaSelect = new SqlDataAdapter(lcmdSQL);
            ldaSelect.Fill(ldtbResult);
            
            Connection.Close();

            return ldtbResult;
        }

        /// <summary>
        /// Execute insert query.
        /// </summary>
        /// <param name="astrQuery">query</param>
        /// <returns>Return Code</returns>
        public int ExecuteInsert(string astrQuery)
        {
            int lintReturnCode = 0;

            if (Connection == null)
                GetConnection();

            Connection.Open();

            SqlCommand lcmdSQL = new SqlCommand(astrQuery, Connection);            
            lintReturnCode = lcmdSQL.ExecuteNonQuery();

            Connection.Close();

            return lintReturnCode;
        }

        #endregion
    }
}
