/****** Object:  Table [dbo].[UniversityBuildings]    Script Date: 08.11.2020 13:56:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UniversityBuildings](
	[UniversityBuildingPK] [int] IDENTITY(1,1) NOT NULL,
	[BuildingName] [varchar](100) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UniversityBuildingPK] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


