﻿CREATE TABLE [dbo].[Page]
(
    [Id] VARCHAR(30) NOT NULL , 
    [Title] NVARCHAR(255) NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_Page] PRIMARY KEY ([Id] ASC)
)
