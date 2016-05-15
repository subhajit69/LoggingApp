using LoggingApp.Constants;
using LoggingApp.Models;
using LoggingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LoggingApp.Controllers
{
    public class LogDataController : ApiController
    {
        ILogRequestMessage logReqMsg = new LogRequestMessage();

        /// <summary>
        /// Logging LogData object into database or csv formated file or both place based on LoggingType header value
        /// </summary>
        /// <param name="logData">LogData custom object</param>
        /// <returns>HttpResponseMessage object</returns>
        public HttpResponseMessage LoggingData([FromBody]LogData logData)
        {
            try
            {
                var LoggingType = Request.Headers.GetValues(Constant.LoggingType);
                if (LoggingType.FirstOrDefault() == Constant.DB)
                {
                    logReqMsg.SaveLogDataRequest(logData);
                }
                else if (LoggingType.FirstOrDefault() == Constant.File)
                {
                    logReqMsg.SaveLogDataInCSV(logData);
                }
                else if (LoggingType.FirstOrDefault() == Constant.All)
                {   
                    logReqMsg.SaveLogDataRequest(logData);
                    logReqMsg.SaveLogDataInCSV(logData);
                }
                HttpResponseMessage respMsg = new HttpResponseMessage();
                respMsg.StatusCode = HttpStatusCode.OK;
                respMsg.Content = new StringContent(Constant.MsgLogged);
                return respMsg;
            }
            catch (Exception ex)
            {
                HttpResponseMessage expResp = new HttpResponseMessage();
                expResp.StatusCode = HttpStatusCode.ExpectationFailed;
                expResp.Content = new StringContent(ex.Message);
                return expResp;
            }
        }

        /// <summary>
        /// Logging ExceptionData object into database or csv formated file or both place based on LoggingType header value
        /// </summary>
        /// <param name="logExceptionData">ExceptionData custom object</param>
        /// <returns>HttpResponseMessage object</returns>
        public HttpResponseMessage LoggingExceptionData([FromBody]ExceptionData logExceptionData)
        {
            try
            {
                var LoggingType = Request.Headers.GetValues(Constant.LoggingType);
                if (LoggingType.FirstOrDefault() == Constant.DB)
                {
                    logReqMsg.SaveLogExceptionDataRequest(logExceptionData);
                }
                else if (LoggingType.FirstOrDefault() == Constant.File)
                {
                    logReqMsg.SaveExceptionDataInCSV(logExceptionData);
                }
                else if (LoggingType.FirstOrDefault() == Constant.All)
                {
                    logReqMsg.SaveLogExceptionDataRequest(logExceptionData);
                    logReqMsg.SaveExceptionDataInCSV(logExceptionData);
                }
                HttpResponseMessage respMsg = new HttpResponseMessage();
                respMsg.StatusCode = HttpStatusCode.OK;
                respMsg.Content = new StringContent(Constant.ExceptionLogged);
                return respMsg;
            }
            catch (Exception ex)
            {
                HttpResponseMessage expResp = new HttpResponseMessage();
                expResp.StatusCode = HttpStatusCode.ExpectationFailed;
                expResp.Content = new StringContent(ex.Message);
                return expResp;
            }
        }
        
    }
}