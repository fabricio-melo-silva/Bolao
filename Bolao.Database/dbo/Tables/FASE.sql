﻿CREATE TABLE [dbo].[FASE] (
    [COD_FASE]      INT          IDENTITY (1, 1) NOT NULL,
    [DSC_FASE]      VARCHAR (50) NOT NULL,
    [COD_BOLAO]     INT          NOT NULL,
    [IND_TIPO_FASE] CHAR (1)     NOT NULL
		CONSTRAINT CK_FASE_IND_TIPO_FASE CHECK (IND_TIPO_FASE IN ('G', 'E')),
    [IND_STATUS]    CHAR (1)     NOT NULL DEFAULT 'N'
		CONSTRAINT CK_FASE_IND_STATUS CHECK (IND_STATUS IN ('N', 'A', 'F')),
    CONSTRAINT [PK_FASE] PRIMARY KEY CLUSTERED ([COD_FASE] ASC),
    CONSTRAINT [FK_BOLAO_FASE_01] FOREIGN KEY ([COD_BOLAO]) REFERENCES [dbo].[BOLAO] ([COD_BOLAO])
);


GO
CREATE NONCLUSTERED INDEX [IDX_FASE_001]
    ON [dbo].[FASE]([COD_BOLAO] ASC);

