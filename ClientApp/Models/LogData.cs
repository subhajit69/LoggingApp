using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientApp.Models
{
    public class LogData
    {
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string MachineName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Message { get; set; }
        public string ServerName { get; set; }
        public string Url { get; set; }
        public DateTime LoggingTime { get; set; }
    }

    
}
