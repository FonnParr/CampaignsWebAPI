CREATE TABLE [dbo].[Species] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[CharacterClasses] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Players] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Campaigns] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Characters] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [CampaignId] INT           NOT NULL,
    [PlayerId]   INT           NOT NULL,
    [SpeciesId]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaigns] ([Id]),
    FOREIGN KEY ([SpeciesId]) REFERENCES [dbo].[Species] ([Id]),
    FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Players] ([Id])
);

CREATE TABLE [dbo].[ClassLevels] (
    [ClassId] INT NOT NULL,
    [PcId]    INT NOT NULL,
    [Level]   INT NOT NULL,
    CONSTRAINT [PK_PcClass] PRIMARY KEY CLUSTERED ([ClassId] ASC, [PcId] ASC),
    FOREIGN KEY ([ClassId]) REFERENCES [dbo].[CharacterClasses] ([Id]),
    FOREIGN KEY ([PcId]) REFERENCES [dbo].[Characters] ([Id])
);



