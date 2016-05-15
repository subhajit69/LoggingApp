using LoggingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using LoggingApp.Constants;
using LoggingApp.Repository.Interface;

namespace LoggingApp.Repository
{
    public class LogRequestMessage : ILogRequestMessage
    {
        IDBConnectionProvider dbProvider = new DBConnectionProvider();

        /// <summary>
        /// Saving Exception Data into tblMessageLog table in the configured DB 
        /// </summary>
        /// <param name="logData">LogData custom object</param>
        public void SaveLogDataRequest(LogData logData)
        {
            try
            {
                var con = dbProvider.GetConnection();
                string sql = "INSERT INTO tblMessageLog(UserName, ApplicationName, MachineName, ClassName, MethodName, Message, ServerName, Url,LoggingTime) " +
                            "VALUES(@UserName,@ApplicationName,@MachineName,@ClassName,@MethodName,@Message,@ServerName,@Url,@LoggingTime)";
                var cmd = dbProvider.GetSqlCommand(con, sql);
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = logData.UserName;
                cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 50).Value = logData.ApplicationName;
                cmd.Parameters.Add("@MachineName", SqlDbType.VarChar, 50).Value = logData.MachineName;
                cmd.Parameters.Add("@ClassName", SqlDbType.VarChar, 50).Value = logData.ClassName;
                cmd.Parameters.Add("@MethodName", SqlDbType.VarChar, 50).Value = logData.MethodName;
                cmd.Parameters.Add("@Message", SqlDbType.VarChar, 100).Value = logData.Message;
                cmd.Parameters.Add("@ServerName", SqlDbType.VarChar, 50).Value = logData.MachineName;
                cmd.Parameters.Add("@Url", SqlDbType.VarChar, 100).Value = logData.Url;
                cmd.Parameters.Add("@LoggingTime", SqlDbType.DateTime).Value = logData.LoggingTime;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Saving Exception Data into tblExceptionLog table in the configured DB 
        /// </summary>
        /// <param name="logExceptionData">ExceptionData custom object</param>
        public void SaveLogExceptionDataRequest(ExceptionData logExceptionData)
        {
            try
            {
                var con = dbProvider.GetConnection();
                string sql = "INSERT INTO tblExceptionLog(UserName, ApplicationName, ExceptionClassName, ExceptionMethodName, ExceptionMessage, ExceptionLineNumber, ServerName, Url, ExceptionLoggingTime) " +
                              "VALUES(@UserName,@ApplicationName,@ExceptionClassName,@ExceptionMethodName,@ExceptionMessage,@ExceptionLineNumber,@ServerName,@Url,@ExceptionLoggingTime)";
                var cmd = dbProvider.GetSqlCommand(con, sql);
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = logExceptionData.UserName;
                cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 50).Value = logExceptionData.ApplicationName;
                cmd.Parameters.Add("@ExceptionClassName", SqlDbType.VarChar, 50).Value = logExceptionData.ExceptionClassName;
                cmd.Parameters.Add("@ExceptionMethodName", SqlDbType.VarChar, 50).Value = logExceptionData.ExceptionMethodName;
                cmd.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar, 50).Value = logExceptionData.ExceptionMessage;
                cmd.Parameters.Add("@ExceptionLineNumber", SqlDbType.VarChar, 100).Value = logExceptionData.ExceptionLineNumber;
                cmd.Parameters.Add("@ServerName", SqlDbType.VarChar, 50).Value = logExceptionData.ServerName;
                cmd.Parameters.Add("@Url", SqlDbType.VarChar, 100).Value = logExceptionData.Url;
                cmd.Parameters.Add("@ExceptionLoggingTime", SqlDbType.DateTime).Value = logExceptionData.ExceptionLoggingTime;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Saving LogData in CSV format file 
        /// </summary>
        /// <param name="logData">LogData custom object</param>
        public void SaveLogDataInCSV(LogData logData)
        {
            try
            {
                string path = ConfigurationManager.AppSettings[Constant.MsgLoggingFIlePath];
                path = path + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + Constant.Underscore + Constant.LogDataFileName + Constant.CSV;
                string csvHeaderRow = "UserName" + "," + "ApplicationName" + "," + "MachineName" + "," + "ClassName" + "," + "MethodName" + "," + "Message" + "," + "ServerName" + "," + "Url" + "," + "LoggingTime";
                string csvDataRow = logData.UserName + "," + logData.ApplicationName + "," + logData.MachineName + "," + logData.ClassName + "," + logData.MethodName + "," + logData.Message + "," + logData.MachineName + "," + logData.Url + "," + logData.LoggingTime;
                if (File.Exists(path))
                {
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(csvDataRow);
                        writer.Flush();
                        writer.Close();
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(csvHeaderRow);
                        writer.WriteLine(csvDataRow);
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Saving Exception Data in CSV format file 
        /// </summary>
        /// <param name="logExceptionData">ExceptionData custom object</param>
        public void SaveExceptionDataInCSV(ExceptionData logExceptionData)
        {
            try
            {
                string path = ConfigurationManager.AppSettings[Constant.MsgLoggingFIlePath];
                path = path + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + Constant.Underscore + Constant.LogExceptionFileName + Constant.CSV;
                string csvHeaderRow = "UserName" + "," + "ApplicationName" + "," + "ExceptionClassName" + "," + "ExceptionMethodName" + "," + "ExceptionMessage" + "," + "ExceptionLineNumber" + "," + "ServerName" + "," + "Url" + "," + "ExceptionLoggingTime";
                string csvDataRow = logExceptionData.UserName + "," + logExceptionData.ApplicationName + "," + logExceptionData.ExceptionClassName + "," + logExceptionData.ExceptionMethodName + "," + logExceptionData.ExceptionMessage + "," + logExceptionData.ExceptionLineNumber + "," + logExceptionData.ServerName + "," + logExceptionData.Url + "," + logExceptionData.ExceptionLoggingTime;
                if (File.Exists(path))
                {
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(csvDataRow);
                        writer.Flush();
                        writer.Close();
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(csvHeaderRow);
                        writer.WriteLine(csvDataRow);
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}