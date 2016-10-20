SELECT
	    S.[factoryNumber] AS ZAV_NUM
      , S.[certificateNumber] AS NUM_SVID
      , S.certificateIssueDate AS DATE_SVID
      , S.[certificateValidDate] AS [SROK_SVID]
      , S.[SP_REGION_GAI] AS [REGION]
      , S.[locationLattitude] AS [LAT]
      , S.[locationLongitude] AS [LONG]
      , ' LAC : '+ S.[LAC]+' CI : '+S.[CI]+' BSID : '+S.[BSID] AS  [IDS]
      , S.[Mac] AS [MAC]
      , S.[DelDate] AS [DEL_DATE]
      , S.[updateDate] AS [UPDATE_DATE]
FROM
[RESDB].[dbo].[RES] S

INSERT INTO [RadioFrequencyCenter].[dbo].[BroadcastStations]
SELECT
	  S.[factoryNumber] AS ZAV_NUM
      ,S.[certificateNumber] AS NUM_SVID
      ,S.certificateIssueDate AS DATE_SVID
      ,S.[certificateValidDate] AS [SROK_SVID]
      ,S.[SP_REGION_GAI] AS [REGION]
      ,S.[locationLattitude] AS [LAT]
      ,S.[locationLongitude] AS [LONG]
      ,' LAC : '+ S.[LAC]+' ; CI : '+S.[CI]+' ; BSID : '+S.[BSID] + ' ; ' AS  [IDS]
      ,S.[Mac] AS [MAC]
      ,S.[DelDate] AS [DEL_DATE]
      ,S.[updateDate] AS [UPDATE_DATE]
FROM
[RESDB].[dbo].[RES] S

TRUNCATE TABLE [RadioFrequencyCenter].[dbo].[BroadcastFrequencies];
DELETE FROM [RadioFrequencyCenter].[dbo].[BroadcastStations];

select COUNT(*) AS BroadcastStations FROM [RadioFrequencyCenter].[dbo].[BroadcastStations];
select COUNT(*) AS LinkBroadcastStationsToResdbRes FROM [RadioFrequencyCenter].[dbo].[LinkBroadcastStationsToResdbRes]
select COUNT(*) AS StationFrequencies FROM [RadioFrequencyCenter].[dbo].[StationFrequencies]
select COUNT(*) AS LinkStationFrequenciesToResdbFrq FROM [RadioFrequencyCenter].[dbo].[LinkStationFrequenciesToResdbFrq]

SELECT
	SF.*
FROM 
	[RESDB].[dbo].[RES] SS
	JOIN [RESDB].[dbo].[FRQ] SF ON SS.GUID=SF.RES
	JOIN [RadioFrequencyCenter].[dbo].[BroadcastStations] TS 
	ON TS.[ZAV_NUM] = SS.[factoryNumber]
	AND TS.[REGION] = SS.[SP_REGION_GAI]
	AND TS.[LAT] = SS.locationLattitude
	AND TS.[LONG] = SS.locationLongitude
	AND TS.MAC= SS.Mac
;

select COUNT(*) FROM [RESDB].[dbo].[FRQ];

INSERT INTO [RadioFrequencyCenter].[dbo].[StationFrequencies]
SELECT
	TS.ID_RES AS [RES],
	SF.[TN] AS [TN],
	SF.[RN] AS [RN]
FROM 
	[RESDB].[dbo].[RES] SS
	JOIN [RESDB].[dbo].[FRQ] SF ON SS.GUID=SF.RES
	JOIN [RadioFrequencyCenter].[dbo].[BroadcastStations] TS 
	ON TS.[ZAV_NUM] = SS.[factoryNumber]
	AND TS.[REGION] = SS.[SP_REGION_GAI]
	AND TS.[LAT] = SS.locationLattitude
	AND TS.[LONG] = SS.locationLongitude
	AND TS.MAC= SS.Mac
;

SELECT ( SELECT COUNT(*) FROM [RadioFrequencyCenter].[dbo].[StationFrequencies] BF WHERE BF.RES = BS.ID_RES) AS BF_COUNT ,BS.* FROM [RadioFrequencyCenter].[dbo].[BroadcastStations] BS;
SELECT COUNT(*) AS COUNT , BF.RES FROM [RadioFrequencyCenter].[dbo].[StationFrequencies] BF GROUP BY BF.RES;

  select CONVERT ( datetimeoffset , '2016-10-18 14:18:00 +05:00'   , 0  );

SELECT
	*
FROM
	[RESDB].[dbo].[RES]
;

SELECT 
      [GUID] AS Guid
    , [factoryNumber] AS FactoryNumber
    , [certificateNumber] AS CertificateNumber
    , [certificateIssueDate] AS CertificateIssueDate
    , [certificateValidDate] AS CertificateValidDate
    , [SP_REGION_GAI] AS SpRegionGai
    , [locationLattitude] AS LocationLattitude
    , [locationLongitude] AS LocationLongitude
    , [LAC] AS Lac
    , [CI] AS Ci
    , [BSID] AS Bsid
    , [Mac] AS Mac
    , [isDeleted] AS IsDeleted
    , [DelDate] AS DelDate
    , [updateDate] AS UpdateDate
FROM
    [RESDB].[dbo].[RES]

WHERE
  updateDate > CONVERT ( datetimeoffset , '2012-04-12 13:32:00 +05:00'   , 0  ) 
  AND  
  certificateNumber LIKE '%74%' 
  AND  factoryNumber = 10321574;

SELECT * FROM [RadioFrequencyCenter].[dbo].[BroadcastStations] BS WHERE BS.ID_RES= 888;
SELECT * FROM [RESDB].[dbo].[RES] RS WHERE RS.certificateNumber= '74 12 ¹ 01959';

SELECT * FROM [RESDB].[dbo].[RES] RS WHERE RS.updateDate > '2012-04-12 13:33:00.000';

SELECT * FROM [RadioFrequencyCenter].[dbo].[BroadcastStations] BS ;
