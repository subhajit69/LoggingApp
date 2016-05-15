using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingApp.Repository.Interface
{
    public interface IDBConnectionProvider
    {
        SqlConnection GetConnection();
        SqlCommand GetSqlCommand(SqlConnection sqlcon, string sqlcommand);
    }
}
