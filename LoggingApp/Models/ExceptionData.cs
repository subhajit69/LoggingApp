﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggingApp.Models
{
    public class ExceptionData
    {
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string ExceptionClassName { get; set; }
        public string ExceptionMethodName { get; set; }
        public string ExceptionMessage { get; set; }
        public int ExceptionLineNumber { get; set; }
        public string ServerName { get; set; }
        public string Url { get; set; }
        public DateTime ExceptionLoggingTime { get; set; }
    }
}