﻿CREATE TABLE [dbo].[LOG_ACESSO] (
    [COD_ACESSO]   BIGINT   IDENTITY (1, 1) NOT NULL,
    [COD_USUARIO]  INT      NOT NULL,
    [DAT_ACESSO]   DATETIME NOT NULL,
    [DSC_ATRIBUTO] XML      NULL,
    CONSTRAINT [PK_LOGACESSO] PRIMARY KEY CLUSTERED ([COD_ACESSO] ASC),
	CONSTRAINT FK_USUARIO_LOGACESSO_01 FOREIGN KEY (COD_USUARIO) REFERENCES USUARIO (COD_USUARIO)
);
GO

CREATE INDEX IDX_LOGACESSO_001 ON LOG_ACESSO (COD_USUARIO)
GO


