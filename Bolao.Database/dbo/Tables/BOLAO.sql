﻿CREATE TABLE [dbo].[BOLAO] (
    [COD_BOLAO]   INT           IDENTITY (1, 1) NOT NULL,
    [DSC_BOLAO]   VARCHAR (100) NOT NULL,
    [IND_STATUS]  CHAR (1)      NOT NULL DEFAULT 'N'
		CONSTRAINT FK_BOLAO_IND_STATUS CHECK (IND_STATUS IN ('N', 'A', 'F')),
    [DAT_RANKING] DATETIME      NULL,
    [VLR_BOLAO]   FLOAT (53)    NOT NULL,
    CONSTRAINT [PK_BOLAO] PRIMARY KEY CLUSTERED ([COD_BOLAO] ASC)
);

