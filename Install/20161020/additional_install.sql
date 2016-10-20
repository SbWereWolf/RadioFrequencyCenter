
USE [RadioFrequencyCenter]
GO

/****** Object:  View [dbo].[V_ResdbResUpdatedate]    Script Date: 2016-10-20 03:25:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.V_ResdbResUpdatedate', 'U') IS NOT NULL 
  DROP TABLE dbo.V_ResdbResUpdatedate;

CREATE VIEW [dbo].[V_ResdbResUpdatedate]
AS
SELECT LR.GUID AS ResGUID, BS.ID_RES, BS.UPDATE_DATE, LF.GUID AS FrqGUID, SF.ID_F
FROM     dbo.BroadcastStations AS BS INNER JOIN
                  dbo.LinkBroadcastStationsToResdbRes AS LR ON BS.ID_RES = LR.ID_RES INNER JOIN
                  dbo.StationFrequencies AS SF ON BS.ID_RES = SF.RES INNER JOIN
                  dbo.LinkStationFrequenciesToResdbFrq AS LF ON SF.ID_F = LF.ID_F

GO