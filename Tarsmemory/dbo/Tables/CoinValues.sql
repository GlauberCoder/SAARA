CREATE TABLE [dbo].[CoinValues] (
    [ID]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Date]       DATETIME       NOT NULL,
    [PriceOpen]  FLOAT (53)     NOT NULL,
    [PriceClose] FLOAT (53)     NOT NULL,
    [Amount]     FLOAT (53)     NOT NULL,
    [Action]     INT            NULL,
    [Exchanger]  NVARCHAR (MAX) NULL
);

