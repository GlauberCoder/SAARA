CREATE TABLE [dbo].[BitmexTradingRates] (
    [ID]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Timestamp_Open]  DATETIME       NULL,
    [Timestamp_Close] DATETIME       NULL,
    [RowKey]          NVARCHAR (MAX) NULL,
    [RowKey_Min]      BIGINT         NULL,
    [RowKey_Max]      BIGINT         NULL,
    [CurrencyPair]    NVARCHAR (MAX) NULL,
    [Price_Open]      FLOAT (53)     NULL,
    [Price_Close]     FLOAT (53)     NULL,
    [Price_Low]       FLOAT (53)     NULL,
    [Price_High]      FLOAT (53)     NULL,
    [Price_Low_Sell]  FLOAT (53)     NULL,
    [Price_Low_Buy]   FLOAT (53)     NULL,
    [Price_High_Sell] FLOAT (53)     NULL,
    [Price_High_Buy]  FLOAT (53)     NULL,
    [Price_Var]       FLOAT (53)     NULL,
    [Price_Avg]       FLOAT (53)     NULL,
    [Price_Stdev]     FLOAT (53)     NULL,
    [Amount]          FLOAT (53)     NULL,
    [Amount_Sell]     FLOAT (53)     NULL,
    [Amount_Buy]      FLOAT (53)     NULL,
    [Amount_Avg]      FLOAT (53)     NULL,
    [Amount_Var]      FLOAT (53)     NULL,
    [Amount_Stdev]    FLOAT (53)     NULL,
    [Time_Window]     INT            NULL,
    [Price_Sum]       FLOAT (53)     NULL,
    [Trades_Count]    FLOAT (53)     NULL,
    CONSTRAINT [PK_BitmexTradingRates] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [nci_wi_BitmexTradingRates_6CDD7BA137664C9E1C4EEB65D96735E5]
    ON [dbo].[BitmexTradingRates]([Time_Window] ASC)
    INCLUDE([Amount], [Price_Close], [Price_High], [Price_Low], [Price_Open], [Timestamp_Open]);


GO
CREATE NONCLUSTERED INDEX [nci_wi_BitmexTradingRates_9D1830286B9310F93A81C86265F19694]
    ON [dbo].[BitmexTradingRates]([Time_Window] ASC)
    INCLUDE([Amount], [CurrencyPair], [Price_Close], [Price_High], [Price_Low], [Price_Open], [Timestamp_Open]);

