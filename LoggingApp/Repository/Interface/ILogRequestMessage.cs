using LoggingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingApp.Repository
{
    public interface ILogRequestMessage
    {
        void SaveLogDataRequest(LogData logData);
        void SaveLogExceptionDataRequest(ExceptionData logExceptionData);
        void SaveLogDataInCSV(LogData logData);
        void SaveExceptionDataInCSV(ExceptionData logExceptionData);
    }
}
