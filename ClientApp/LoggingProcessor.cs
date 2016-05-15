using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClientApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class LoggingMessageProcessorAttribute : FilterAttribute, IActionFilter
    {
        public string ApplicationName { get; set; }

        /// <summary>
        /// Request to LoggingAPI to log requested data
        /// </summary>
        /// <param name="context">ControllerContext object</param>
        public void RequestLogging(ControllerContext context)
        {
            LogData logData = CreateLogData(context);
            HttpClient htpclient = new HttpClient();
            htpclient.DefaultRequestHeaders.Add(Constant.LoggingType, ConfigurationManager.AppSettings[Constant.loggingTypeValue]);
            Uri requli = new Uri(ConfigurationManager.AppSettings[Constant.MessageLoggingApiUrl]);
            var resp = htpclient.PostAsJsonAsync<LogData>(requli.ToString(), logData).Result;
        }


       /// <summary>
       /// Creates LogData custom object from ControllerContext and server variables
       /// </summary>
       /// <param name="context"></param>
       /// <returns>LogData Custom object</returns>
        public LogData CreateLogData(ControllerContext context)
        {
            var logData = new LogData {
            UserName = context.HttpContext.Profile.UserName == null ? Environment.UserName : context.HttpContext.Profile.UserName,
            MachineName = context.HttpContext.Server.MachineName,
            ClassName = context.RouteData.Values[Constant.Controller].ToString(),
            MethodName = context.RouteData.Values[Constant.Action].ToString(),
            Url = context.HttpContext.Request.Url.AbsoluteUri,
            ApplicationName = ApplicationName,
            LoggingTime = DateTime.Now,
            Message = Constant.REQUEST_METHOD + Constant.Colon + context.HttpContext.Request.ServerVariables[Constant.REQUEST_METHOD] + Constant.SemiColon +
                      Constant.CONTENT_TYPE + Constant.Colon + context.HttpContext.Request.ServerVariables[Constant.CONTENT_TYPE] + Constant.SemiColon +
                      Constant.LOCAL_ADDR + Constant.Colon + context.HttpContext.Request.ServerVariables[Constant.LOCAL_ADDR] + Constant.SemiColon +
                      Constant.REMOTE_USER + Constant.Colon + context.HttpContext.Request.ServerVariables[Constant.REMOTE_USER] + Constant.SemiColon +
                      Constant.QUERY_STRING + Constant.Colon + context.HttpContext.Request.ServerVariables[Constant.QUERY_STRING]
                     
        };
            return logData;
         
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RequestLogging(filterContext.Controller.ControllerContext);
        }

        
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class LoggingExceptionProcessorAttribute : FilterAttribute,IExceptionFilter
    {
        public string ApplicationName { get; set; }

        /// <summary>
        /// Request to LoggingAPI to log Exception Data 
        /// </summary>
        /// <param name="context">HttpContextBase object</param>
        /// <param name="ex">Exception object</param>
        public void RequestExceptionLogging(HttpContextBase context, Exception ex)
        {
            ExceptionData logExceptionData = CreateExceptionData(context, ex);
            HttpClient htpclient = new HttpClient();
            htpclient.DefaultRequestHeaders.Add(Constant.LoggingType, ConfigurationManager.AppSettings[Constant.loggingTypeValue]);
            Uri requli = new Uri(ConfigurationManager.AppSettings[Constant.ExceptionLoggingApiUrl]);
            var resp = htpclient.PostAsJsonAsync<ExceptionData>(requli.ToString(), logExceptionData).Result;
        }

        /// <summary>
        /// Create ExceptionData object from HttpContextBase and Exception object
        /// </summary>
        /// <param name="context">HttpContextBase object</param>
        /// <param name="ex"> Exception object</param>
        /// <returns>ExceptionData Custom object</returns>
        public ExceptionData CreateExceptionData(HttpContextBase context, Exception ex)
        {
            var exceptionData = new ExceptionData
            {
                UserName = context.Profile.UserName == null ? Environment.UserName : context.Profile.UserName,
                ServerName = context.Server.MachineName,
                ExceptionClassName = new StackTrace(ex).GetFrame(0).GetMethod().DeclaringType.Name.ToString(),
                ExceptionLineNumber = (ex.StackTrace.Split(new string[] { ":line " }, StringSplitOptions.None)[1]).Split(new char[]{' '})[0].Replace("\r\n"," "),//new StackTrace(ex).GetFrame(0).GetFileLineNumber(),
                ExceptionLoggingTime = DateTime.Now,
                ExceptionMessage = ex.Message,
                ExceptionMethodName = new StackTrace(ex).GetFrame(0).GetMethod().Name,
                Url = context.Request.Url.AbsoluteUri,
                ApplicationName = ApplicationName,

            };
            return exceptionData;
        }

       
        public void OnException(ExceptionContext filterContext)
        {
            RequestExceptionLogging(filterContext.HttpContext, filterContext.Exception);
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult() { ViewName = Constant.ErrorViewName };
        }
    }

}
