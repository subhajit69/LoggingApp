using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ClientApp.Controllers
{
    [LoggingExceptionProcessorAttribute(ApplicationName = "ClientApp")]
    [LoggingMessageProcessorAttribute(ApplicationName = "ClientApp")]
    public class HomeController : Controller
    {
        LoggingMessageProcessorAttribute ins = new LoggingMessageProcessorAttribute();
        public ActionResult Index()
        {
            
            try
            {
                 ins.ApplicationName = "ClientApp";
                 int j = 0;
                 int i = GetRes();
                 ins.RequestLogging(this.ControllerContext);
                 return View();
            }
            catch (Exception ex)
            {
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
