using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace test.Controllers
{
    [LoggingExceptionProcessorAttribute(ApplicationName = "test")]
    [LoggingMessageProcessorAttribute(ApplicationName = "test")]
    public class HomeController : Controller
    {
        LoggingMessageProcessorAttribute ins = new LoggingMessageProcessorAttribute();
        public ActionResult Index()
        {
            
            try
            {
                ins.ApplicationName = "LoggingApp";
                ins.ApplicationName = "LoggingApp";
                ins.ApplicationName = "LoggingApp";
                 ins.ApplicationName = "LoggingApp";
                 int j = 0;
                 int i = GetRes();
                 ins.RequestLogging(this.ControllerContext);
                 return View();
            }
            catch (Exception ex)
            {
                //return View("Error");
                throw ex;
            }
        }


        public static int GetRes()
        {
            int j = 0;
            int i = 6 / j;
            return i;
        }
    }
}
