﻿CREATE TABLE [dbo].[GRUPO] (
    [COD_GRUPO] INT          IDENTITY (1, 1) NOT NULL,
    [COD_FASE]  INT          NOT NULL,
    [COD_BOLAO] INT          NOT NULL,
    [SGL_GRUPO] VARCHAR (5)  NULL,
    [NOM_GRUPO] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_GRUPO] PRIMARY KEY CLUSTERED ([COD_GRUPO] ASC),
    CONSTRAINT [FK_BOLAO_GRUPO_01] FOREIGN KEY ([COD_BOLAO]) REFERENCES [dbo].[BOLAO] ([COD_BOLAO]),
    CONSTRAINT [FK_FASE_GRUPO_01] FOREIGN KEY ([COD_FASE]) REFERENCES [dbo].[FASE] ([COD_FASE])
);

GO

CREATE INDEX IDX_GRUPO_001 ON GRUPO (COD_FASE)
GO

CREATE INDEX IDX_GRUPO_002 ON GRUPO (COD_BOLAO)
GO