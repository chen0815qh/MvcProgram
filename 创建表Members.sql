USE [MessageBoardSys]
GO

/****** Object:  Table [dbo].[Members]    Script Date: 05/27/2015 17:22:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Members](
	[MemberId] [int] IDENTITY(1,1) NOT NULL,
	[MemberAccount] [varchar](1024) NOT NULL,
	[Password] [varchar](512) NOT NULL,
	[Portrait] [nvarchar](1024) NULL,
	[NickName] [nvarchar](1024) NULL,
	[Gender] [nvarchar](16) NULL,
	[Age] [int] NULL,
	[QQ] [varchar](32) NULL,
	[Email] [nvarchar](128) NULL,
	[RegisterTime] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[LastLoginIP] [varchar](128) NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [DF_Members_RegisterTime]  DEFAULT (getdate()) FOR [RegisterTime]
GO


