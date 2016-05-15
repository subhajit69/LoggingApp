# ### Introduction
The objectives of this Web API application to create a easy configurable and reusable application for logging and exception handling in the database or csv file or both according to your business needs.  

### Environment  
* .Net Framework : 4.5  
* Web API Version : 4.0.30506.0  
* MVC Version : 4.0.30506.0  
* Database Used : SQL Server 2012

### Configuring LoggingApp
**Step 1:**  
  Download and unzip the project files.  

**Step 2:**  
  change `MsgLoggingFIlePath` and `dbLogConnection` values to your desired file logging path and database connection string.
  
**Step 3:**  
  Run the script file `DBScript.sql` to your database for `[dbo].[tblExceptionLog]` and  `[dbo].[tblMessageLog]` table creation. 
 
**Step 4:**  
  Host `LoggingApp` to the IIS server and note down the hosting url.  

**Step 5:**  
  Copy and Paste `LoggingProcessor.cs` file from ClientApp folder to your project folder for which you need logging message and exception. Change the namespace `ClientApp` accordingly.  

**Step 6:**  
  Copy and paste `Constant.cs` file to your project folder and change the namespace `ClientApp` accordingly.  

**Step 7:**  
 * Add the following appSettings lines to your projects app setting folder. Change `http://localhost:18235/` part of `MessageLoggingApiUrl` and `ExceptionLoggingApiUrl` value to your LoggingApp hosting url.  
 * LoggingTypeValue value can be either of DB/File/All for logging in database or csv file or both place  
    `<add key="MessageLoggingApiUrl" value="http://localhost:18235/api/LogData/LoggingData"/>
    <add key="ExceptionLoggingApiUrl" value="http://localhost:18235/api/LogData/LoggingExceptionData"/>
    <add key="loggingTypeValue" value="All"/>`   
 
**Step 8:**  
  * In the `HomeController.cs` the sample code is provided for logging message and exception through `LoggingExceptionProcessor` and `LoggingMessageProcessor` attribute.  
  * `RequestLogging` and `RequestExceptionLogging` method can also be called for logging message and exception from required places creating instance `LoggingMessageProcessorAttribute` and `LoggingExceptionProcessorAttribute` class respectively.  

### Authors and Contributors
Find my profile in GitHub Subhajit Ray, (@subhajit69)

### Support or Contact
Having trouble with configuration? Drop a mail to subhajit.ray69@gmail.com
