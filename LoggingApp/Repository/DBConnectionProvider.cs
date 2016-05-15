using LoggingApp.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LoggingApp.Repository.Interface;

namespace LoggingApp.Repository
{
    public class DBConnectionProvider : IDBConnectionProvider
    {
        /// <summary>
        /// Creationg SqlConnection object from the connecton string provided in the web.config file
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public SqlConnection GetConnection()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings[Constant.DBConnection].ConnectionString;
                SqlConnection sqlcon = new SqlConnection(con);
                return sqlcon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creating SqlCommand object with the provided SqlConnection object and sql query data
        /// </summary>
        /// <param name="sqlcon">SqlConnection object</param>
        /// <param name="sqlcommand">sql query string</param>
        /// <returns>SqlCommand object</returns>
        public SqlCommand GetSqlCommand(SqlConnection sqlcon, string sqlcommand)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlcommand;
                cmd.Connection = sqlcon;
                cmd.CommandType = CommandType.Text;
                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}