﻿CREATE TABLE [dbo].[Symbols]
(
	[ID] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(7) NOT NULL, 
    [Exclusion] DATETIME NULL, 
    [Description] NVARCHAR(80) NOT NULL
)
