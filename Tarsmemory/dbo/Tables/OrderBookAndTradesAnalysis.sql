CREATE TABLE [dbo].[OrderBookAndTradesAnalysis] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [Timestamp]   DATETIMEOFFSET (7) NOT NULL,
    [Volume]      FLOAT (53)         NOT NULL,
    [Open]        FLOAT (53)         NOT NULL,
    [Close]       FLOAT (53)         NOT NULL,
    [High]        FLOAT (53)         NOT NULL,
    [Low]         FLOAT (53)         NOT NULL,
    [Time_Window] INT                NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

