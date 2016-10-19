USE [RadioFrequencyCenter]
GO

/****** Object:  Table [dbo].[LinkBroadcastStationsToResdbRes]    Script Date: 2016-10-20 03:24:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LinkBroadcastStationsToResdbRes](
	[ID_RES] [bigint] NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LinkBroadcastStationsToResdbRes_IdRes] PRIMARY KEY CLUSTERED 
(
	[ID_RES] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LinkBroadcastStationsToResdbRes]  WITH CHECK ADD  CONSTRAINT [FK_LinkBroadcastStationsToResdbRes_BroadcastStations] FOREIGN KEY([ID_RES])
REFERENCES [dbo].[BroadcastStations] ([ID_RES])
GO

ALTER TABLE [dbo].[LinkBroadcastStationsToResdbRes] CHECK CONSTRAINT [FK_LinkBroadcastStationsToResdbRes_BroadcastStations]
GO

USE [RadioFrequencyCenter]
GO

/****** Object:  Table [dbo].[LinkStationFrequenciesToResdbFrq]    Script Date: 2016-10-20 03:25:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LinkStationFrequenciesToResdbFrq](
	[ID_F] [bigint] NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_LinkStationFrequenciesToResdbFrq] PRIMARY KEY CLUSTERED 
(
	[ID_F] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LinkStationFrequenciesToResdbFrq]  WITH CHECK ADD  CONSTRAINT [FK_LinkStationFrequenciesToResdbFrq_StationFrequencies] FOREIGN KEY([ID_F])
REFERENCES [dbo].[StationFrequencies] ([ID_F])
GO

ALTER TABLE [dbo].[LinkStationFrequenciesToResdbFrq] CHECK CONSTRAINT [FK_LinkStationFrequenciesToResdbFrq_StationFrequencies]
GO


USE [RadioFrequencyCenter]
GO

/****** Object:  View [dbo].[V_ResdbResUpdatedate]    Script Date: 2016-10-20 03:25:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_ResdbResUpdatedate]
AS
SELECT LR.GUID AS ResGUID, BS.ID_RES, BS.UPDATE_DATE, LF.GUID AS FrqGUID, SF.ID_F
FROM     dbo.BroadcastStations AS BS INNER JOIN
                  dbo.LinkBroadcastStationsToResdbRes AS LR ON BS.ID_RES = LR.ID_RES INNER JOIN
                  dbo.StationFrequencies AS SF ON BS.ID_RES = SF.RES INNER JOIN
                  dbo.LinkStationFrequenciesToResdbFrq AS LF ON SF.ID_F = LF.ID_F

GO