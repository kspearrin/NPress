CREATE TABLE [dbo].[User]
(
    [Id] VARCHAR(30) NOT NULL,
    [Username] NVARCHAR(20) NOT NULL,
    [Password] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NULL,
    [DisplayName] NVARCHAR(20) NOT NULL,
    [CreationDateTime] DATETIME2 NOT NULL,
    [RevisionDateTime] DATETIME2 NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] DESC)
)
