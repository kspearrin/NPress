CREATE TABLE [dbo].[Post]
(
    [Id] VARCHAR(30) NOT NULL,
    [UserId] VARCHAR(30) NOT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [Slug] NVARCHAR(100) NOT NULL,
    [Published] BIT NOT NULL,
    [PublishDateTime] DATETIME2 NOT NULL,
    [CreationDateTime] DATETIME2 NOT NULL,
    [RevisionDateTime] DATETIME2 NOT NULL,
    CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED ([Id] DESC)
)
