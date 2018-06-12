﻿CREATE TABLE [dbo].[APOSTA] (
    [COD_USUARIO]  INT      NOT NULL,
    [COD_JOGO]     INT      NOT NULL,
    [QTD_GOL_A]    INT      NULL,
    [COD_CRITERIO] INT      NULL,
    [QTD_GOL_B]    INT      NULL,
    [COD_TIME_A]   INT      NULL,
    [COD_TIME_B]   INT      NULL,
    [DAT_APOSTA]   DATETIME NULL,
    [IND_APURADA]  CHAR (1) NULL DEFAULT 'N'
		CONSTRAINT CK_APOSTA_IND_APURADA CHECK (IND_APURADA IN ('S', 'N')),
	CONSTRAINT PK_APOSTA PRIMARY KEY (COD_USUARIO, COD_JOGO)
);
GO

ALTER TABLE APOSTA ADD CONSTRAINT FK_USUARIO_APOSTA_01 FOREIGN KEY (COD_USUARIO) REFERENCES USUARIO (COD_USUARIO)
GO

ALTER TABLE APOSTA ADD CONSTRAINT FK_JOGO_APOSTA_01 FOREIGN KEY (COD_JOGO) REFERENCES JOGO (COD_JOGO)
GO

ALTER TABLE APOSTA ADD CONSTRAINT FK_CRITERIO_APOSTA_01 FOREIGN KEY (COD_CRITERIO) REFERENCES CRITERIO (COD_CRITERIO)
GO

CREATE INDEX IDX_APOSTA_001 ON APOSTA (COD_JOGO)
GO

CREATE INDEX IDX_APOSTA_002 ON APOSTA (COD_CRITERIO)
GO

CREATE TRIGGER [dbo].[TGR_APOSTA_01] ON [dbo].[APOSTA] AFTER INSERT, UPDATE, DELETE AS
BEGIN
	SET NOCOUNT ON

	DECLARE 
		@strParam VARCHAR(255),
		@codUsuario INT = NULL,
		@dscIp VARCHAR(255),
		@nomPagina VARCHAR(255),
		@nomTabela NVARCHAR(255),
		@countInserted INT = 0,
		@countDeleted INT = 0,
		@tipo CHAR(1),
		@dscAntes XML = NULL,
		@dscDepois XML = NULL,
		@dscAtributo XML = NULL

	-- RECUPERA AS INFORMAÇÕES DO CONTEXTO DA CONEXÃO
	SELECT @strParam = CAST(CONTEXT_INFO() AS VARCHAR(255)) FROM master.dbo.sysprocesses WHERE SPID = @@SPID
	
	IF @strParam IS NOT NULL BEGIN
		SELECT @codUsuario = CONVERT(INT, @strParam)
	END

	-- RECUPERA O OWNER E A TABELA CORRENTE
	SELECT 
		@nomTabela = OBJECT_NAME(PARENT_ID)
	FROM sys.TRIGGERS
	WHERE OBJECT_ID = @@PROCID

	-- RECUPERA A QUANTIDADE LINHAS INSERIDAS E DELETADAS
	SELECT @countInserted = COUNT(*) FROM INSERTED
	SELECT @countDeleted = COUNT(*) FROM DELETED

	-- DEFINE O TIPO DA OPERAÇÃO
	SET @tipo = CASE 
		WHEN @countInserted > 0 AND @countDeleted > 0 THEN 'A'
		WHEN @countInserted > 0 AND @countDeleted = 0 THEN 'I'
		WHEN @countInserted = 0 AND @countDeleted > 0 THEN 'D'
		ELSE 'X' -- Não há linhas inseridas, alteradas ou excluídas
	END

	-- RECUPERA OS DADOS ANTIGOS E OS NOVOS CONFORME O TIPO DE OPERAÇÃO
	IF @tipo = 'A' BEGIN
		SET @dscAntes = (SELECT * FROM DELETED TABELA FOR XML AUTO)
		SET @dscDepois = (SELECT * FROM INSERTED TABELA FOR XML AUTO)
	END
	ELSE IF @tipo = 'I' BEGIN
		SET @dscDepois = (SELECT * FROM INSERTED TABELA FOR XML AUTO)
	END
	ELSE IF @tipo = 'D' BEGIN
		SET @dscAntes = (SELECT * FROM DELETED TABELA FOR XML AUTO)
	END

	--INSERE O LOG PARA OS REGISTROS INSERIDOS E/OU ATUALIZADOS
	IF @tipo <> 'X' BEGIN
		INSERT INTO LOG_REGISTRO (DAT_LOG_REGISTRO, COD_USUARIO, NOM_TABELA, IND_TIPO_LOG_REGISTRO, DSC_DADOS_ANTES, DSC_DADOS_DEPOIS, DSC_ATRIBUTO)
		SELECT GETDATE(), @codUsuario, @nomTabela, @tipo, @dscAntes, @dscDepois, @dscAtributo
	END
END
