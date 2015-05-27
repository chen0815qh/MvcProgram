USE [MessageBoardSys]
GO

/****** Object:  Table [dbo].[LeaveMessage]    Script Date: 05/27/2015 17:22:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LeaveMessage](
	[MsgId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[MsgContent] [nvarchar](max) NOT NULL,
	[AddTime] [datetime] NULL,
	[IsReply] [bit] NOT NULL,
	[IP] [varchar](128) NULL,
 CONSTRAINT [PK_LeaveMessage] PRIMARY KEY CLUSTERED 
(
	[MsgId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[LeaveMessage] ADD  CONSTRAINT [DF_Table_1_LeaveTime]  DEFAULT (getdate()) FOR [AddTime]
GO


