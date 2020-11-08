/****** Object:  Table [dbo].[OperatingSystems]    Script Date: 08.11.2020 12:56:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OperatingSystems](
	[OperatingSystemPK] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[CompanyName] [varchar](200) NOT NULL,
	[ProgrammingLanguage] [varchar](100) NOT NULL,
	[LatestVersion] [varchar](30) NOT NULL,
	[SupportedPlatforms] [varchar](200) NOT NULL,
	[MarketShare] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OperatingSystemPK] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


