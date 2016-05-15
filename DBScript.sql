
/****** Object:  Table [dbo].[tblExceptionLog]    Script Date: 05/15/2016 5:13:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblExceptionLog](
	[exceptionid] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[ApplicationName] [varchar](50) NULL,
	[ExceptionClassName] [varchar](50) NULL,
	[ExceptionMethodName] [varchar](50) NULL,
	[ExceptionMessage] [varchar](50) NULL,
	[ExceptionLineNumber] [varchar](50) NULL,
	[ServerName] [varchar](50) NULL,
	[Url] [varchar](50) NULL,
	[ExceptionLoggingTime] [datetime] NULL,
 CONSTRAINT [PK_tblExceptionLog] PRIMARY KEY CLUSTERED 
(
	[exceptionid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[tblMessageLog](
	[logid] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[ApplicationName] [varchar](50) NULL,
	[MachineName] [varchar](50) NULL,
	[ClassName] [varchar](50) NULL,
	[MethodName] [varchar](50) NULL,
	[Message] [varchar](100) NULL,
	[ServerName] [varchar](100) NULL,
	[Url] [varchar](100) NULL,
	[LoggingTime] [datetime] NULL,
 CONSTRAINT [PK_tblMessageLog] PRIMARY KEY CLUSTERED 
(
	[logid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF
GO


