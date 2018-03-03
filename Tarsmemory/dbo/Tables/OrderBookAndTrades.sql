CREATE TABLE [dbo].[OrderBookAndTrades] (
    [Id]           INT                IDENTITY (1, 1) NOT NULL,
    [PartitionKey] VARCHAR (MAX)      NOT NULL,
    [RowKey]       VARCHAR (MAX)      NOT NULL,
    [Timestamp]    DATETIMEOFFSET (7) NOT NULL,
    [Amount]       FLOAT (53)         NOT NULL,
    [Date]         DATETIMEOFFSET (7) NOT NULL,
    [Exchange]     VARCHAR (MAX)      NOT NULL,
    [Rate]         FLOAT (53)         NOT NULL,
    [Tota]         FLOAT (53)         NOT NULL,
    [TradeID]      INT                NOT NULL,
    [Type]         VARCHAR (MAX)      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [Date_Index]
    ON [dbo].[OrderBookAndTrades]([Date] ASC);

