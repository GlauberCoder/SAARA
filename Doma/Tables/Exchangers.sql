﻿CREATE TABLE [dbo].[Exchangers]
(
	[ID] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(80) NOT NULL, 
    [Exclusion] DATETIME NULL
)